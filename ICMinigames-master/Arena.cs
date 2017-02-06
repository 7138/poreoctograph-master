using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TShockAPI;
using TerrariaApi.Server;
using Terraria;
using Microsoft.Xna.Framework;

namespace ICMinigames
{
    public class Arena
    {
        public string name;
        public string information;
        public string author;
        public MinigameType minigameType;
        public string schematicName;
        public Vector2[] spawnPoints;
        public float width;
        public float height;

        public Arena()
        {
            name = "";
            information = "";
            author = "";
            minigameType = MinigameType.None;
            schematicName = "null";
            spawnPoints = new Vector2[] { Vector2.Zero };
            width = 0;
            height = 0;
        }
    }
}
