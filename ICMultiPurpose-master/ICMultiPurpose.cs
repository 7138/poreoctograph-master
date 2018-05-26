//'Import' necessary packages
using System;
using TShockAPI;
using Terraria;
using TerrariaApi.Server;
using System.Collections.Generic;
using OTAPI;

namespace ICMultiPurpose
{

	[ApiVersion(2,0)]
	
	public class ICMultiPurpose : TerrariaPlugin
	{

		public override Version Version
		{
			get { return new Version(1, 0); }
		}
		
		public override string Name
		{
			get { return "ICMultipurpose"; }
		}
		
		public override string Author
		{
			get { return "Slayr & Plad"; }
		}
		

		public override string Description
		{
			get { return "Multipurpose features plugin for IC"; }
		}
		
        public ICMultiPurpose(Main game)
			: base(game)
		{
            Order = +4;
		}
		

		public override void Initialize()
		{

	     }
	}
}
		
		
		
		
