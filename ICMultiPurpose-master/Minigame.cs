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
using static OTAPI.Hooks;

public enum MinigameType { None, CTG, Bomberman };

namespace ICMG
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

        public enum ItemSlot
        {
            InvRow1Slot1, InvRow1Slot2, InvRow1Slot3, InvRow1Slot4, InvRow1Slot5, InvRow1Slot6, InvRow1Slot7, InvRow1Slot8, InvRow1Slot9, InvRow1Slot10,
            InvRow2Slot1, InvRow2Slot2, InvRow2Slot3, InvRow2Slot4, InvRow2Slot5, InvRow2Slot6, InvRow2Slot7, InvRow2Slot8, InvRow2Slot9, InvRow2Slot10,
            InvRow3Slot1, InvRow3Slot2, InvRow3Slot3, InvRow3Slot4, InvRow3Slot5, InvRow3Slot6, InvRow3Slot7, InvRow3Slot8, InvRow3Slot9, InvRow3Slot10,
            InvRow4Slot1, InvRow4Slot2, InvRow4Slot3, InvRow4Slot4, InvRow4Slot5, InvRow4Slot6, InvRow4Slot7, InvRow4Slot8, InvRow4Slot9, InvRow4Slot10,
            InvRow5Slot1, InvRow5Slot2, InvRow5Slot3, InvRow5Slot4, InvRow5Slot5, InvRow5Slot6, InvRow5Slot7, InvRow5Slot8, InvRow5Slot9, InvRow5Slot10,
            CoinSlot1, CoinSlot2, CoinSlot3, CoinSlot4, AmmoSlot1, AmmoSlot2, AmmoSlot3, AmmoSlot4, HandSlot,
            ArmorHeadSlot, ArmorBodySlot, ArmorLeggingsSlot, AccessorySlot1, AccessorySlot2, AccessorySlot3, AccessorySlot4, AccessorySlot5, AccessorySlot6, UnknownSlot1,
            VanityHeadSlot, VanityBodySlot, VanityLeggingsSlot, SocialAccessorySlot1, SocialAccessorySlot2, SocialAccessorySlot3, SocialAccessorySlot4, SocialAccessorySlot5, SocialAccessorySlot6, UnknownSlot2,
            DyeHeadSlot, DyeBodySlot, DyeLeggingsSlot, DyeAccessorySlot1, DyeAccessorySlot2, DyeAccessorySlot3, DyeAccessorySlot4, DyeAccessorySlot5, DyeAccessorySlot6, Unknown3,
            EquipmentSlot1, EquipmentSlot2, EquipmentSlot3, EquipmentSlot4, EquipmentSlot5,
            DyeEquipmentSlot1, DyeEquipmentSlot2, DyeEquipmentSlot3, DyeEquipmentSlot4, DyeEquipmentSlot5
        }

        public enum ItemPrefix
        {
            None, Large, Massive, Dangerous, Savage, Sharp, Pointy, Tiny, Terrible, Small, Dull, Unhappy, Bulky, Shameful, Heavy, Light, Sighted,
            Rapid, Hasty, Intimidating, Deadly, Staunch, Awful, Lethargic, Arkward, Powerful, Mystic, Adept, Masterful, Inept, Ignorant, Deranged, Intense, Taboo,
            Celestial, Furious, Keen, Superior, Forceful, Broken, Damaged, Shoddy, Quick, Deadly2, Agile, Nimble, Murderous, Slow, Sluggish, Lazy, Annoying, Nasty,
            Manic, Hurtful, Strong, Unpleasant, Weak, Ruthless, Frenzying, Godly, Demonic, Zealous, Hard, Guarding, Armored, Warding, Arcane, Precise, Lucky, Jagged,
            Spiked, Angry, Menacing, Brisk, Fleeting, Hasty2, Quick2, Wild, Rash, Intrepid, Violent, Legendary, Unreal, Mythical
        }
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

        public Minigame(Main game)
        {
        }

        public void SetRegion(int width, int height)
        {
            TShock.Regions.DeleteRegion(regionName);
            TShock.Regions.AddRegion((int)origin.X, (int)origin.Y, width, height, regionName, "Server", Main.worldID.ToString());
            region = TShock.Regions.GetRegionByName(regionName);

            ICMG.SendMessage(ICMG.allPlayers[0], "Region, " + region.Name + ", created.");
        }

        public void ClearRegionArea()
        {
            ICMG.RunCommand("//region " + regionName);
            ICMG.RunCommand("//cut");
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

            ICMG.SendMessage(player, "Joined the minigame: " + name + ". Session ID: " + sessionID);

            if (currentPlayers.Count == minPlayers)
            {
                StartMinigame();
            }
            else if (currentPlayers.Count < minPlayers)
            {
                ICMG.SendMessage(player, "Waiting for" + (minPlayers - currentPlayers.Count) + " more player(s) to start...");
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
                ICMG.SendMessage(player, "Left the minigame: " + name + ". Session ID: " + sessionID);
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
