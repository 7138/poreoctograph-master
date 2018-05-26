using TShockAPI;
using OTAPI;
using Terraria;
using System;
//Unneeded until later
namespace ICMG
{
    public class MGPlayer
    {
        public TSPlayer plr;
        public MinigameType currentMinigame;
        public int sessionID;
        public Group originalGroup;
        public string originalplayerName;
        public string accountName;
        public Item[] savedInventory;
        public int savedHealth;
        public int savedMana;
        public bool hasTrashItem;
        public Team forceTeam;

        public MGPlayer()
        {

        }

        public MGPlayer(TSPlayer plr)
        {

        }
        
        public Minigame GetCurrentMinigame()
        {
            for (int i = 0; i < ICMG.activeMinigames.Count; ++i)
            {
                if (ICMG.activeMinigames[i].minigameType == currentMinigame && ICMG.activeMinigames[i].sessionID == 1)
                {
                    return ICMG.activeMinigames[i];
                }
            }
            return null;
        }

        public Item[] GetInventory()
        {

        }

        public void LoadInventory()
        {

        }

        public void SaveInventory()
        {

        }

        public void SetStats(int hp, int mana)
        {

        }

        public void SetPVPMode(bool hostile)
        {

        }

        public string GetName()
        {

        }

        public void Heal(int healAmount)
        {

        }

        public void Heal()
        {

        }

        public void ToSpawn()
        {

        }
    }
}
