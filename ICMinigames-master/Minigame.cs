using System;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using TShockAPI.DB;
using System.Timers;
using TShockAPI;
using Terraria;
using TerrariaApi.Server;
using System.Collections.Generic;

public enum MinigameType { None, CTG, Bomberman };

namespace ICMinigames
{
    public class Minigame
    {
        public string name;
        public string information;
        public MinigameType minigameType;
        public Group minigameGroup;
        public int maxPlayers;
        public int minPlayers;
        public bool active;
        public int sessionID;
        public Vector2 origin;
        public List<MGPlayer> currentPlayers;
        public Timer secondTimer;
        public bool forcePVPOn;
        public bool forceTeam;
        public string regionName;
        public Region region;

        public Minigame()
        {
            currentPlayers = new List<MGPlayer>();
            active = false;
            sessionID = 0;
            origin = Vector2.Zero;
            secondTimer = new Timer();
            secondTimer.Interval = 1000;
            secondTimer.Enabled = false;
            secondTimer.AutoReset = true;
            secondTimer.Elapsed += OnSecondTimerElapsed;
            forcePVPOn = false;
            forceTeam = false;
            regionName = "";
            region = null;
        }

        public void SetRegion(int width, int height)
        {
            TShock.Regions.DeleteRegion(regionName);
            TShock.Regions.AddRegion((int)origin.X, (int)origin.Y, width, height, regionName, "Server", Main.worldID.ToString());
            region = TShock.Regions.GetRegionByName(regionName);

            ICMinigames.SendMessage(ICMinigames.allPlayers[0], "Region, " + region.Name + ", created.");
        }

        public void ClearRegionArea()
        {
            ICMinigames.RunCommand("//region " + regionName);
            ICMinigames.RunCommand("//cut");
        }

        public virtual void OnSecondTimerElapsed(object sender, ElapsedEventArgs e)
        {
            for (int i = 0; i < currentPlayers.Count; ++i)
            {
                if ((forcePVPOn && !currentPlayers[i].plr.TPlayer.hostile) || (!forcePVPOn && currentPlayers[i].plr.TPlayer.hostile));
                {
                    currentPlayers[i].SetPvPMode(forcePVPOn);
                }
                if (forceTeam && currentPlayers[i].plr.Team != (int)currentPlayers[i].forceTeam)
                {
                    currentPlayers[i].plr.SetTeam((int)currentPlayers[i].forceTeam);
                }
            }
        }

        public virtual void Update()
        {
            if (active)
            {

            }
        }

        public virtual void SetupInventoryPrefabs()
        {

        }

        public virtual void StartRound()
        {

        }

        public virtual void EndRound()
        {

        }

        public virtual void StartMinigame()
        {
            secondTimer.Enabled = true;
            active = true;
        }

        public virtual void EndMinigame()
        {
            secondTimer.Enabled = false;
            active = false;
        }

        public virtual void JoinGame(MGPlayer player)
        {
            SendMessage(player.GetName() + " joined the session.");

            currentPlayers.Add(player);
            player.sessionID = sessionID;
            player.currentMinigame = minigameType;
            player.plr.Group = minigameGroup;
            player.SetPVPMode(forcePVPOn);
            player.SaveInventory();
            player.plr.SetTeam((int)Team.None);
            player.forceTeam = Team.None;
            player.hasTrashItem = player.plr.TPlayer.trashItem != null;

            ICMinigames.SendMessage(player, "Joined the minigame: " + name + ". Session ID: " + sessionID);

            if (currentPlayers.Count == minPlayers)
            {
                StartMinigame();
            }
            else if (currentPlayers.Count < minPlayers)
            {
                ICMinigames.SendMessage(player, "Waiting for" + (minPlayers - currentPlayers.Count) + " more player(s) to start...");
            }
        }

        public virtual void LeaveGame(MGPlayer player)
        {
            currentPlayers.Remove(player);
            player.sessionID = 0;
            player.currentMinigame = MinigameType.None;
            if (player.plr != null)
            {
                player.plr.Group = player.originalGroup;
                player.SetPVPMode(false);
                player.ToSpawn();
                player.Heal();
                player.forceTeam = Team.None;
                player.plr.SetTeam((int)Team.None);
                player.LoadInventory();
            }

            if (player.plr != null)
            {
                ICMinigames.SendMessage(player, "Left the minigame: " + name + ". Session ID: " + sessionID);
            }
            SendMessage(player.GetName() + " left the session.");

            if (currentPlayers.Count < minPlayers)
            {
                EndMinigame();
            }
        }

        public virtual void DisplayScores(MGPlayer player)
        {

        }

        public virtual void PlayerDeath(MGPlayer player, MGPlayer killer)
        {

        }

        public virtual void PlayerRespawn(MGPlayer player)
        {

        }

        public virtual Arena GetCurrentArena()
        {
            return null;
        }

        public string[] GetPlayerNames(Team team = Team.None, MGPlayer excluding = null)
        {

        }

        public void SendMessage(string message, Color? colour = null)
        {

        }

        public void SendCombatText(string message, Color? colour = null)
        {

        }
    }
}
