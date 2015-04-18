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
        public static List<Entity> enemies;
        public override void Setup()
        {
            this.setTitle("LD 32");
            this.setScale(64);
            player = new Entity("content/img/player", 0, 0, true);
            map = Util.getBlocksFrom("content/lvl/map.png", "content/img/Floor01.png", 0);
            enemies = Util.getEntitiesFrom("content/lvl/map.png", "content/img/enemy", 255);
            this.entities = enemies;
            this.blocks = map;
            this.AddToScene(player);
            this.setFPS(60);
            this.setGravity(.25f);
            player.Health = 100;
            player.killedByTouch = true;
        }

        public override void Update()
        {
            handleInput();
            setCenter((int)player.getX(), (int)player.getY());
            foreach(Block b in blocks)
            {
                b.setHoverColor(255,0,0);
            }
            if(player.touching)
            {
                player.Health--;
            }
        }

        public void handleInput()
        {
            if (Input.isKeyDown(Input.Key.Jump) && player.isColliding)
            {
                player.setVelocity(player.getVelocityX(), -7.5f);
            }
            if (Input.isKeyDown(Input.Key.Left))
            {
                player.setVelocity(-4f, player.getVelocityY());
            }
            if (Input.isKeyDown(Input.Key.Right))
            {
                player.setVelocity(4f , player.getVelocityY());
            }
        }
    }
}
