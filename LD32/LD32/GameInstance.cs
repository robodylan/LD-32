using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dunn_2D;
using Dunn_2D.GUI;
namespace LD32
{
    class GameInstance : Game 
    {
        public static Entity player;
        public static Text healthText;
        public static List<Block> map;
        public static List<Entity> enemies;
        public override void Setup()
        {
            this.setTitle("");
            this.setScale(64);
            this.setFPS(120);
            this.setGravity(.25f);
            this.setBackground("content/img/bg.png");

            healthText = new Text(10,10, "Health: 0");
            player = new Entity("content/img/playerLeft", 0, 0, true);

            blocks = Util.getBlocksFrom("content/lvl/map.png", "content/img/Floor01.png", 0);
            blocks.AddRange(Util.getBlocksFrom("content/lvl/map.png", "content/img/Floor02.png", 128));
            entities = Util.getEntitiesFrom("content/lvl/map.png", "content/img/enemyLeft", 255);

            this.AddToScene(healthText);
            this.AddToScene(player);

            player.isControlled = true;
            player.Health = 255;
            player.killedByTouch = true;

            foreach(Entity e in entities)
            {
                e.setColor(255,255,255);
            }
        }

        public override void Update()
        {
            handleInput();
            healthText.text = "Health " + player.Health;
            healthText.setColor(255 - player.Health, player.Health, 0);
            setCenter((int)player.getX(), (int)player.getY());
            foreach(Block b in blocks)
            {
                b.setHoverColor(255, 0, 0);
            }
            if(player.touching && player.Health > 0)
            {               
                player.Health--;
            }
            if(player.Health < 2 || player.getY() > 1024)
            {
                //createParticleSystem((int)player.getX(), (int)player.getY(), "content/img/playerLeft");
                Cleanup();
            }

            foreach(Entity e in entities)
            {
                e.targetX = (int)player.getX();
                e.targetY = (int)player.getY();
            }
        }

        public void handleInput()
        {
            if (Input.isKeyDown(Input.Key.Jump) && player.isColliding)
            {
                player.setVelocity(player.getVelocityX(), -9.5f);
            }
            if (Input.isKeyDown(Input.Key.Left))
            {
                if ((player.fileName != "content/img/playerLeft"))
                {
                    player.Move(0, 0);
                    player.fileName = "content/img/playerLeft";
                }
                player.setVelocity(-4f, player.getVelocityY());
            }
            if (Input.isKeyDown(Input.Key.Right))
            {
                if((player.fileName != "content/img/playerRight"))
                {
                    player.Move(0, 0);
                    player.fileName = "content/img/playerRight";
                }
                player.setVelocity(4f, player.getVelocityY());
            }
        }
    }
}
