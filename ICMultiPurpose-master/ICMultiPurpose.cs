//'Import' necessary packages
using System;
using TShockAPI;
using Terraria;
using TerrariaApi.Server;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using TShockAPI.DB;

namespace ICMultiPurpose
{
    [ApiVersion(2, 1)]

    public class ICMultiPurpose : TerrariaPlugin
    {
        

        public override Version Version
        {
            get { return new Version(1, 0); }
        }

        public override string Name
        {
            get { return "ICMultiPurpose"; }
        }

        public override string Author
        {
            get { return "Plad & Slayr"; }
        }

        public override string Description
        {
            get { return "Multi Purpose plugin with any features wanted"; }
        }

        public ICMultiPurpose(Main game)
            : base(game)
        {
            Order = +4;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameUpdate.Deregister(this, GameUpdate);
            }
            base.Dispose(disposing);
        }


        public override void Initialize()
        {
            ServerApi.Hooks.GameUpdate.Register(this, GameUpdate);
        }

        public void GameUpdate(EventArgs args)
        {
            #region Prevent Natural Meteors
            WorldGen.spawnMeteor = false;
            Console.WriteLine("Meteor successfully prevented");
            #endregion Prevent Natural Meteors
        }
    }
}



