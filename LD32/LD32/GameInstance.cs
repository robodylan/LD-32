using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dunn_2D;

namespace LD32
{
    class GameInstance : Game 
    {
        public static Entity player;
        public static Block block;
        public override void Setup()
        {
            player = new Entity("content/player.png", 0, 0, true);
            block = new Block("content/player.png", 0, 100, true);
            this.AddToScene(player);
            this.AddToScene(block);
            this.setFPS(60);
        }

        public override void Update()
        {
            player.addVelocity(0,0.01f);
        }
    }
}
