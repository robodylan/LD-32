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
        public static List<Block> map;
        public override void Setup()
        {
            this.setScale(30);
            player = new Entity("content/player.png", 0, 0, true);
            map = Util.getBlocksFrom("content/map.png", "content/player.png", 0);
            this.AddToScene(player);
            this.blocks = map;
            this.setFPS(120);
            this.setGravity(.01f);
        }

        public override void Update()
        {
            handleInput();
            setCenter((int)player.getX(), (int)player.getY());
        }

        public void handleInput()
        {
            if (Input.isKeyDown(Input.Key.Jump) && player.isColliding)
            {
                player.setVelocity(player.getVelocityX(), -1f);
            }
            if (Input.isKeyDown(Input.Key.Left))
            {
                player.setVelocity(-1f, player.getVelocityY());
            }
            if (Input.isKeyDown(Input.Key.Right))
            {
                player.setVelocity(1f, player.getVelocityY());
            }
        }
    }
}
