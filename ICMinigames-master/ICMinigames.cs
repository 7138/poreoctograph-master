//'Import' necessary packages
using System;
using TShockAPI;
using Terraria;
using TerrariaApi.Server;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ICMinigames
{
    //Version of TShock API
    [ApiVersion(2, 0)]

    public class ICMinigames : TerrariaPlugin
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
        public ICMinigames(Main game)
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

            Commands.ChatCommands.Add(new Command("icminigame.mgmode", MinigameMode(), "mgmode"));

            public void MinigameMode(CommandArgs args)
        {
            TSPlayer plr = args.Player;

            Group plrGroup = plr.Group;
            plr.Group = new minigameGroup();
        }
        }

        public void SetupCTGArenas()
        {

        }

    }

}


		
		
		
		
