//'Import' necessary packages
using System;
using TShockAPI;
using Terraria;
using TerrariaApi.Server;
using System.Collections.Generic;
using OTAPI;

namespace ICMinigames
{
	//Version of TShock API
	[ApiVersion(2,0)]
	
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
		
		//Main code area
		public override void Initialize()
		{
			//Code for creating a command
			//Commands.ChatCommands.Add(new Command(permission, method, command));
            //eg. new Command("tutorial.bunny", SpawnBunny, "bunny");

            //ServerApi.Hooks
            //TShockAPI.Hooks
		}
	}
}
		
		
		
		
