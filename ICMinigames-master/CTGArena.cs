using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TShockAPI;
using Terraria;
using TerrariaApi.Server;

namespace ICMG
{
    public class CTGArena : Arena
    {
        public CTGArena()
        {
            minigameType = MinigameType.CTG;
        }   
    }
}
