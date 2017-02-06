using TShockAPI;
using OTAPI;
//Unneeded until later
namespace ICMinigames
{
    public class MGPlayer
    {
        public TSPlayer plr;
        public MinigameType currentMinigame;
        public int sessionID;
        public Group originalGroup;
        public string originalplayerName;
        public string accountName;
        public NetItem[] savedInventory;
        public int savedHealth;
        public int savedMana;
        public bool hasTrashItem;
        public Team forceTeam;

        public MGPlayer();

        public MGPlayer(TSPlayer plr)
        
        public ICMinigames GetCurrentMinigame()
        {
            for (int i = 0; i < ICMinigames.activeMinigames.Count; ++i)
            {
                if (ICMinigames.activeMinigames[i].minigameType == currentMinigame && ICMinigames.activeMinigames[i].sessionID == )
            }
        }


    }
}
