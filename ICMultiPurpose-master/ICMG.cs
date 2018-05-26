//'Import' necessary packages
using System;
using TShockAPI;
using Terraria;
using TerrariaApi.Server;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.IO;


public enum Team
{
    None,
    Red,
    Green,
    Blue,
    Yellow,
    Pink
}
public enum ItemPrefix
{
    None, Large, Massive, Dangerous, Savage, Sharp, Pointy, Tiny, Terrible, Small, Dull, Unhappy, Bulky, Shameful, Heavy, Light, Sighted,
    Rapid, Hasty, Intimidating, Deadly, Staunch, Awful, Lethargic, Arkward, Powerful, Mystic, Adept, Masterful, Inept, Ignorant, Deranged, Intense, Taboo,
    Celestial, Furious, Keen, Superior, Forceful, Broken, Damaged, Shoddy, Quick, Deadly2, Agile, Nimble, Murderous, Slow, Sluggish, Lazy, Annoying, Nasty,
    Manic, Hurtful, Strong, Unpleasant, Weak, Ruthless, Frenzying, Godly, Demonic, Zealous, Hard, Guarding, Armored, Warding, Arcane, Precise, Lucky, Jagged,
    Spiked, Angry, Menacing, Brisk, Fleeting, Hasty2, Quick2, Wild, Rash, Intrepid, Violent, Legendary, Unreal, Mythical
}
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
namespace ICMG
{
    //Version of TShock API
    [ApiVersion(2, 0)]

    public class ICMG : TerrariaPlugin
    {
        //Get version of plugin
        public override Version Version
        {
            get { return new Version(1, 0); }
        }

        //Name the plugin
        public override string Name
        {
            get { return "ICMinigames"; }
        }

        //Who created the plugin
        public override string Author
        {
            get { return "Slayr & Plad"; }
        }

        //Describe the plugin (not necessary)
        public override string Description
        {
            get { return "Minigames plugin for IC"; }
        }

        //Will be run first by TShock
        //-x will be run after TShock API loads
        //+x will be run before TShock API loads
        public ICMG(Main game)
            : base(game)
        {
            Order = +4;
        }

        public static List<MGPlayer> allPlayers;
        public static List<Minigame> activeMinigames;
        public static Color chatColour = Color.Teal;

        public static Vector2[] ctgPoints;
        public static Vector2[] bomberManPoints;
        public static List<CTGArena> ctgArenas;
        public static BomberManArena bomberManArena;

        public static int ctgArenaMaxWidth;
        public static int ctgArenaMaxHeight;

        public override void Initialize()
        {
            //Variables
            allPlayers = new List<MGPlayer>();
            activeMinigames = new List<Minigame>();

            //Server Player
            MGPlayer serverPlayer = new MGPlayer(TSPlayer.Server);
            allPlayers.Add(serverPlayer);

            //SS
            for (int i = 0; i < TShock.Groups.groups.Count; ++ i)
            {
                if (TShock.Groups.groups[i].HasPermission("tshock.admin.ignoressc"))
                {
                    TShock.Groups.groups[i].RemovePermission("tshock.admin.ignorescc");
                }

                //Minigame Arenas
                SetupCTGArenas();
                SetupBomberManArenas();

                //Minigame Points
                ctgPoints = new Vector2[] { new Vector2(0, 0) };
                bomberManPoints = new Vector2[] { new Vector2(0, 0) };

                //Minigame Sessions
                for (int i = 0; i < ctgPoints.Length; ++i)
                {
                    CTG ctg = new CTG();
                    ctg.sessionID = i + 1;
                    ctg.origin = ctgPoints[i];
                    ctg.regionName = "ctg" + (i + 1);
                    activeMinigames.Add(ctg);
                }
                for (int i = 0; i < bomberManPoints.Length; ++i)
                {
                    BomberMan bomberMan = new BomberMan();
                    bomberman.sessionID = i + 1;
                    bomberman.origin = bomberManPoints[i];
                    bomberman.regionName = "bomberman" + (i + 1);
                    activeMinigames.Add(bomberMan);
                }

                //Hooks
                ServerApi.Hooks.ServerJoin.Register(this, OnJoin);
                ServerApi.Hooks.ServerLeave.Register(this, OnLeave);
                ServerApi.Hooks.GameUpdate.Register(this, OnGameUpdate);
                TShockAPI.Hooks.PlayerHooks.PlayerPostLogin += OnPostLogin;
                TShockAPI.Hooks.RegionHooks.RegionEntered += OnRegionEntered;
                ServerApi.Hooks.NetGetData.Register(this, OnNetGetData);
                ServerApi.Hooks.GamePostInitialize.Register(this, OnGamePostInitialise);

                //Commands
                Commands.ChatCommands.Add(new Command("icmg", CallICMG, "icmg"));
                Commands.ChatCommands.Add(new Command("inv", CallInventory, "inv"));
            }
        }
        public void SetupCTGArenas()
        {
            ctgArenaMaxWidth = 160;
            ctgArenaMaxHeight = 80;

            ctgArenas = new List<CTGArena>();

            CTGArena ctg1 = new CTGArena();
            ctg1.name = "Something";
            ctg1.information = "something";
            ctg1.author = "someone";
            ctg1.schematicName = "ctg_something";
            ctg1.spawnPoints = new Vector2[] { new Vector2(0, 0), new Vector2(0, 0) };
            ctg1.width = 160;
            ctg1.height = 80;
            ctgArenas.Add(ctg1);
        }
        
        public void CallICMG(CommandArgs args)
        {

        }

        public void CallICMGPlay(CommandArgs args, MGPlayer player)
        {

        }

        public void CallICMGLeave(CommandArgs args, MGPlayer player)
        {

        }

        public void CallICMGMinigames(CommandArgs args, MGPlayer player)
        {

        }

        public void CallICMGMe(CommandArgs args, MGPlayer player)
        {

        }

        public void CallICMGScores(CommandArgs args, MGPlayer player)
        {

        }

        public void CallICMGAdmin(CommandArgs args, MGPlayer player)
        {

        }

        public void CallICMGHelp(CommandArgs args, MGPlayer player)
        {

        }

        public void CallInventory(CommandArgs args)
        {

        }

        public void CallInventoryMap(CommandArgs args, MGPlayer player)
        {

        }

        public void CallInventoryExport(CommandArgs args, MGPlayer player)
        {
            TSPlayer TSPlayer = args.Player;
            string exportedCode = "";
            string arrayName = "itemArray";

            if (args.Parameters.Count > 1)
            {
                arrayName = args.Parameters[1];
            }

            exportedCode += "Item[] " + arrayName + " = new Item[NetItem.MaxInventory];";
            for (int i = 0; i < NetItem.MaxInventory; ++i)
            {
                    Item item = GetItem(i, TSPlayer.TPlayer);
                
                    if (item == null || item.netID == 0)
                {
                    continue;
                }

                exportedCode += arrayName + "[" + i + "] = TShock.Utils.GetItemById(" + item.netID + ");";
                exportedCode += arrayName + "[" + i + "].prefix = " + item.prefix + ";";
                exportedCode += arrayName + "[" + i + "].stack = " + item.stack + ";";

                TSPlayer.Server.SendMessage(exportedCode, Color.Aqua);
            }
        }

        public Minigame GetMinigame(MinigameType minigameType, int sessionID)
        {
         
        }

        public Minigame CreateMinigame(MinigameType minigameType)
        {

        }

        public int GetNumberOfSessions(MinigameType minigameType)
        {

        }

        public void OnGameUpdate(EventArgs args)
        {
            for (int i = 0; i < activeMinigames.Count; ++i)
            {
                activeMinigames[i].Update();
            }
        }

        public void OnJoin(JoinEventArgs args)
        {
            allPlayers.Add(new MGPlayer(TShock.Players[args.Who]));
        }

        public void OnLeave(LeaveEventArgs args)
        {
            MGPlayer player = GetMGPlayer(args.Who);

            if (player.currentMinigame != MinigameType.None)
            {
                GetMinigame(player.currentMinigame, player.sessionID).LeaveGame(player);
            }

            allPlayers.Remove(player);
        }

        public void OnPostLogin(TShockAPI.Hooks.PlayerPostLoginEventArgs args)
        {
            GetMGPlayer(args.Player).accountName = args.Player.User.Name;
            GetMGPlayer(args.Player).originalGroup = args.Player.Group;
        }

        public void OnRegionEntered(TShockAPI.Hooks.RegionHooks.RegionEnteredEventArgs args)
        {
            MGPlayer player = GetMGPlayer(args.Player);
            TShockAPI.DB.Region region = args.Region;

            if (player.currentMinigame == MinigameType.None)
            {
                if (region == activeMinigames[i].region)
                {
                    player.ToSpawn();
                    SendMessage(player, "You are not part of the minigame. Do not interfere.");
                }
            }
        }

        public void OnNetGetData(GetDataEventArgs args)
        {
            using (var stream = new MemoryStream(args.Msg.readBuffer, args.Index, args.Length))
            {
                using (var reader = new BinaryReader(stream))
                {
                    if (args.MsgID == PacketTypes.PlayerSpawn)
                    {
                        byte playerID = reader.ReadByte();
                        MGPlayer player = GetMGPlayer(playerID);

                        if (player.currentMinigame != MinigameType.None)
                        {
                            GetMinigame(player.currentMinigame, player.sessionID).PlayerRespawn(player);
                        }
                    }
                    else if (args.MsgID == PacketTypes.PlayerKillMe)
                    {
                        byte playerID = reader.ReadByte();
                        byte hitDirection = reader.ReadByte();
                        int damage = reader.ReadInt16();
                        bool pvp = reader.ReadBoolean();
                        string deathText = reader.ReadString();

                        int killerID = -1;
                        for (int p = 0; p < allPlayers.Count; ++p)
                        {
                            if (allPlayers[p].plr.Index != playerID && deathText.Contains(allPlayers[p].plr.Name))
                            {
                                killerID = allPlayers[p].plr.Index;
                                break;
                            }

                            MGPlayer player = GetMGPlayer(playerID);
                            MGPlayer killer = killerID > -1 ? GetMGPlayer(killerID) : null;

                            if (player.currentMinigame != MinigameType.None)
                            {
                                GetMinigame(player.currentMinigame, player.sessionID).PlayerDeath(player, killer);
                            }
                        }
                    }
                }
            }
        }

        public void OnGamePostInitialise(EventArgs args)
        {
            for (int i = 0; i < activeMinigames.Count; ++i)
            {
                if (activeMinigames[i].minigameType == MinigameType.CTG)
                {
                    activeMinigames[i].SetRegion(ctgArenaMaxWidth, ctgArenaMaxHeight);
                    activeMinigames[i].ClearRegionArea();
                }
                else if (activeMinigames[i].minigameType == MinigameType.Bomberman)
                {
                    activeMinigames[i].SetRegion((int)bomberManArena.width, (int)bomberManArena.height);
                    activeMinigames[i].ClearRegionArea();
                }
            }
        }

        public MGPlayer GetMGPlayer(int TSPlayerIndex)
        {
            
        }

        public MGPlayer GetMGPlayer(TSPlayer TSPlayer)
        {
            
        }

        public static void SendMessage(MGPlayer plr, string message, Color? colour = null)
        {
            plr.plr.SendMessage(message, colour ?? chatColour);
        }

        public static void SendCombatText(MGPlayer plr, string message, Color? colour = null)
        {
            NetMessage.SendData((int)PacketTypes.CreateCombatText, -1, -1, message, (colour == null) ? (int)chatColour);
        }

        public static void UpdatePlayerInventory(MGPlayer player, Item[] inventory)
        {
            for (int i = 0; i < inventory.Length; ++i)
            {
                UpdatePlayerInventory(player, inventory[i], i);
            }
        }

        public static void UpdatePlayerInventory(MGPlayer player, Item item, int slot)
        {
            if (item == null)
            {
                return;
            }

            if (item.netID == 0)
            {
                item = new Item();
            }

            int index = 0;

            //Inventory slots
            if (slot < NetItem.InventorySlots)
            {
                index = slot;
                player.plr.TPlayer.inventory[index] = item;

                NetMessage.SendData((int)PacketTypes.PlayerSlot, -1, -1, player.plr.TPlayer.inventory[index].name, player.plr.Index, slot, player.plr.TPlayer.inventory[index].prefix);
            }

            //Armor & Accessory slots
            else if (slot < NetItem.InventorySlots + NetItem.ArmorSlots)
            {
                index = slot - NetItem.InventorySlots;
                player.plr.TPlayer.armor[index] = item;

                NetMessage.SendData((int)PacketTypes.PlayerSlot, -1, -1, player.plr.TPlayer.armor[index].name, player.plr.Index, slot, player.plr.TPlayer.armor[index].prefix);
            }

            //Dye slots
            else if (slot < NetItem.InventorySlots + NetItem.ArmorSlots + NetItem.DyeSlots)
            {
                index = slot - (NetItem.InventorySlots + NetItem.ArmorSlots);
                player.plr.TPlayer.dye[index] = item;

                NetMessage.SendData((int)PacketTypes.PlayerSlot, -1, -1, player.plr.TPlayer.dye[index].name, player.plr.Index, slot, player.plr.TPlayer.dye[index].prefix);
            }

            //Misc Equipment slots
            else if (slot < NetItem.InventorySlots + NetItem.ArmorSlots + NetItem.DyeSlots + NetItem.MiscEquipSlots)
            {
                index = slot - (NetItem.InventorySlots + NetItem.ArmorSlots + NetItem.DyeSlots);
                player.plr.TPlayer.miscEquips[index] = item;

                NetMessage.SendData((int)PacketTypes.PlayerSlot, -1, -1, player.plr.TPlayer.miscEquips[index].name, player.plr.Index, slot, player.plr.TPlayer.miscEquips[index].prefix);
            }

            //Misc Dye slots
            else if (slot < NetItem.InventorySlots + NetItem.ArmorSlots + NetItem.DyeSlots + NetItem.MiscEquipSlots + NetItem.MiscDyeSlots)
            {
                index = slot - (NetItem.InventorySlots + NetItem.ArmorSlots + NetItem.DyeSlots + NetItem.MiscEquipSlots);
                player.plr.TPlayer.miscDyes[index] = item;

                NetMessage.SendData((int)PacketTypes.PlayerSlot, -1, -1, player.plr.TPlayer.miscDyes[index].name, player.plr.Index, slot, player.plr.TPlayer.miscDyes[index].prefix);
            }
        }
        public static void ClearPlayerInventory(MGPlayer player)
        {
            for (int i = 0; i < NetItem.MaxInventory; ++i)
            {
                UpdatePlayerInventory(player, new Item(), i);
            }
        }

        public static Item GetItem(int slot, Player player)
        {
            int index = 0;
            if (slot < NetItem.InventorySlots)
            {
                index = slot;
                return player.inventory[index];
            }

            //Armor & Accessory slots
            else if (slot < NetItem.InventorySlots + NetItem.ArmorSlots)
            {
                index = slot - NetItem.InventorySlots;
                return player.armor[index];
            }

            //Dye slots
            else if (slot < NetItem.InventorySlots + NetItem.ArmorSlots + NetItem.DyeSlots)
            {
                index = slot - NetItem.InventorySlots;
                return player.dye[index];
            }

            //Misc Equipment slots
            else if (slot < NetItem.InventorySlots + NetItem.ArmorSlots + NetItem.DyeSlots + NetItem.MiscEquipSlots)
            {
                index = slot - NetItem.InventorySlots;
                return player.miscEquips[index];
            }

            //Misc Dye slots
            else if (slot < NetItem.InventorySlots + NetItem.ArmorSlots + NetItem.DyeSlots + NetItem.MiscEquipSlots + NetItem.MiscDyeSlots)
            {
                index = slot - NetItem.InventorySlots;
                return player.miscDyes[index];
            }
        }

        public static Item GetItem(int id)
        {
            return TShock.Utils.GetItemById(id);
        }

        public static Item GetItem(string name)
        {
            return TShock.Utils.GetItemByName(name)[0];
        }

        public static void RunCommand(string command, TSPlayer plr = null)
        {
            Commands.HandleCommand(plr ?? TSPlayer.Server, command);
        }

        public static string ConvertToMinutesSeconds(int totalSeconds)
        {
            int seconds = totalSeconds % 60;
            int minutes = totalSeconds / 60;
            return (minutes > 0 ? minutes + (minutes > 1 ? " minutes" : " minute") : "") + (seconds > 0 && minutes > 0 ? seconds + (seconds > 1 ? " seconds" : " minute") : "");
        }

        public static Random GetRand()
        {
            return new Random();
        }

        public static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = GetRand().Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

    }

}


		
		
		
		
