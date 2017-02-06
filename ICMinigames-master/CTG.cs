using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using TerrariaApi.Server;
using TShockAPI;

namespace ICMinigames
{
    public class CTG : Minigame
    {
        public Dictionary<MGPlayer, int> killCount;
        public Dictionary<MGPlayer, int> deathCount;
        public List<NetItem[]> inventoryPrefabs;
        public List<Tuple<int, int>> statsPrefabs;
        public List<string> inventoryNames;
        public int currentInventoryPrefab;
        public int matchLength;
        public int matchTimer;
        public int cooldownLength;
        public int cooldownTimer;
        public CTGArena currentArena;
        public List<MGPlayer> ghostPlayers;

        public CTG()
        {
            name = "CTG";
            information = "A game where you must capture the other team's gem.";
            minigameType = MinigameType.CTG;
            minigameGroup = TShock.Utils.GetGroup("ctg");
            maxPlayers = 10;
            minPlayers = 2;
            currentArena = null;
            ghostPlayers = new List<MGPlayer>();

            killCount = new Dictionary<MGPlayer, int>();
            deathCount = new Dictionary<MGPlayer, int>();

            //Inventory Prefabs
            inventoryPrefabs = new List<NetItem[]>();
            statsPrefabs = new List<Tuple<int, int>>();
            inventoryNames = new List<string>();
            currentInventoryPrefab = 0;
            SetupInventoryPrefabs();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void OnSecondTimerElapsed(object sender, ElapsedEventArgs e)
        {
            
        }

        public override void SetupInventoryPrefabs()
        {

        }

        public override void StartRound()
        {
            base.StartRound();


            //Reset scores
            for (int i = 0; i < currentPlayers.Count; ++i)
            {
                deathCount[currentPlayers[i]] = 0;
                killCount[currentPlayers[i]] = 0;
            }

            //Choose new inventory set
            currentInventoryPrefab = ICMinigames.GetRand().Next(0, inventoryPrefabs.Count);

            //Respawn the players and update inventory
            for (int i = 0; i < currentPlayers.Count; ++i)
            {
                RespawnPlayer(currentPlayers[i], currentArena.spawnPoints[i].X, currentArena.spawnPoints[i].Y);
                SendCombatText("Fight!");

                ICMinigames.UpdatePlayerInventory(currentPlayers[i], inventoryPrefabs[currentInventoryPrefab]);
                currentPlayers[i].SetStats(statsPrefabs[currentInventoryPrefab].Item1, statsPrefabs[currentInventoryPrefab].Item2);
            }

            //Announce new round
            SendMessage("Starting new round...");
            SendMessage("");
            SendMessage("Round Parameters:");
            SendMessage("    (Arena) " + currentArena.name + " created by " + currentArena.author + ".");
            SendMessage("    (Arena) " + currentArena.information);
            SendMessage("    (Stats) Inventory Set: " + inventoryNames[currentInventoryPrefab] + ", Health:" + statsPrefabs[currentInventoryPrefab] + ", Mana:" + statsPrefabs[currentInventoryPrefab]);
            SendMessage("    (Players) " + string.Join(", ", GetPlayerNames()) + ".");

            //Turn on PVP.
            forcePVPOn = true;
            forceTeam = true;

            //Manage timers
            cooldownTimer = -1;
            matchTimer = matchLength;
        }

        public override void EndRound()
        {
            base.EndRound();

            //Send All Players To Spawn
            for (int i = 0; i < currentPlayers.Count; ++i)
            {
                currentPlayers[i].ToSpawn();
                currentPlayers[i].Heal();
            }

            //Calculate Results
            SendMessage("The round is over...");
            SendMessage("");
            SendMessage("Results: ");

            //Turn off PVP.
            forcePVPOn = false;
            forceTeam = false;

            //Manage timers
            matchTimer = -1;
            cooldownTimer = cooldownLength;
            }
        }

        public override void StartMinigame()
        {
            
        }

        public override void EndMinigame()
        {
            
        }

        public override void JoinGame(MGPlayer player)
        {
            
        }

        public override void LeaveGame(MGPlayer player)
        {
            
        }

        public override void DisplayScores(MGPlayer player)
        {
            
        }

        public override void PlayerDeath(MGPlayer player, MGPlayer killer)
        {
            
        }

        public override void PlayerRespawn(MGPlayer player)
        {
            
        }

        public void RespawnPlayer(MGPlayer player, float X = 0, float Y = 0)
        {
            if (X == 0 && Y == 0)
            {
                Vector2 newSpawnPoint = (origin * 16) + (currentArena.spawnPoints[ICMinigames.GetRand().Next(0, currentArena.spawnPoints.Length)] * 16);
                player.plr.Teleport(newSpawnPoint.X, newSpawnPoint.Y - (4 * 16));
            }
            else
            {
                Vector2 newSpawnPoint = (origin * 16) + (new Vector2(X, Y) * 16);
                player.plr.Teleport(newSpawnPoint.X, newSpawnPoint.Y - (4 * 16));
            }

            ICMinigames.UpdatePlayerInventory(player, inventoryPrefabs[currentInventoryPrefab]);
            player.SetStats(statsPrefabs[currentInventoryPrefab].Item1, statsPrefabs[currentInventoryPrefab].Item2);
            player.Heal();
        }

        public override Arena GetCurrentArena()
        {
            
        }
    }
}
