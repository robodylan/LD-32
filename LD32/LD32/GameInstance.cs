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
        public Entity player;
        public Text healthText;
        public Text scoreText;
        public Text resourceText;
        public List<Block> unbreakable;
        public List<Entity> enemies;
        public bool playerWon = false;
        public GameInstance(int highscore)
        {
        }
        public override void Setup()
        {
            Destroyed = 5;
            this.setTitle("");
            this.setScale(64);
            this.setFPS(60);
            this.setGravity(1f);
            this.setBackground("content/img/bg.png");
            this.setOverlay("content/img/overlay.png");
            scoreText = new Text(10, 60, "Score: 0");
            healthText = new Text(10,10, "Health: 0");
            resourceText = new Text(10, 110, "Resources: 0");
            player = new Entity("content/img/playerLeft", 0, 0, true);
            unbreakable = Util.getBlocksFrom("content/lvl/map.png", "content/img/Floor03.png", 64);
            blocks = Util.getBlocksFrom("content/lvl/map.png", "content/img/Floor01.png", 0);
            blocks.AddRange(Util.getBlocksFrom("content/lvl/map.png", "content/img/Floor02.png", 128));
            blocks.AddRange(Util.getBlocksFrom("content/lvl/map.png", "content/img/Floor04.png", 172));
            blocks.AddRange(unbreakable);
            entities = Util.getEntitiesFrom("content/lvl/map.png", "content/img/enemyLeft", 255);

            this.AddToScene(healthText);
            this.AddToScene(player);
            this.AddToScene(scoreText);
            this.AddToScene(resourceText);

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
            if(player.getX() > (250 * 64))
            {
                playerWon = true;
            }
            if(Destroyed > 15)
            {
                Destroyed = 15;
            }
            handleInput();
            scoreText.text = "Score " + Kills * 17;
            scoreText.setColor(134, 64, 255);
            healthText.text = "Health " + player.Health;
            healthText.setColor(255 - player.Health, player.Health, 0);
            resourceText.text = "Black Holes " + Destroyed;
            resourceText.setColor(255 - (Destroyed * 15), 0, (Destroyed * 15));
            setCenter((int)player.getX(), (int)player.getY());
            if(player.touching && player.Health > 0)
            {               
                player.Health--;
            }
            if(player.Health < 2 || player.getY() > 10024)
            {
                Cleanup();
            }

            foreach(Entity e in entities)
            {
                e.targetX = (int)player.getX();
                e.targetY = (int)player.getY();
            }
            foreach(Block b in blocks)
            {
                if (getMouseX()+32> b.getX() && getMouseX() + 32< b.getX() + Block.blockSize && getMouseY()+32> b.getY() && getMouseY()+32< Block.blockSize + b.getY() && Destroyed < 15)
                {
                    if (Math.Abs(b.getX() - (player.getX())) < 128 && Math.Abs(b.getY() -(player.getY())) < 128)
                    {
                        b.setColor(255,128,128);
                        if (Input.isLeftClicked() && b.ID != 64 && b.ID != 14)
                        {
                            b.mouseHover = true;
                            b.damage = b.damage + 2;
                            b.setColor((byte)(255 - b.damage * 2), (byte)(255 - b.damage * 2), (byte)(255 - b.damage * 2));
                        }
                        else
                        {
                            if(b.ID != 14) b.damage = 0;
                        }
                    }

                }
                else
                {
                    b.setColor(255, 255, 255);
                }
                if (b.damage > 80)
                {
                    Destroyed++;
                    createParticleSystem((int)(b.getX() / 64) * 64, (int)(b.getY() / 64) * 64, "content/img/dirtParticle");
                    blocks.Remove(b);
                    return;
                }
            }
        }

        public void handleInput()
        {
            if (Input.isKeyDown(Input.Key.Jump) && player.isColliding)
            {
                player.setVelocity(player.getVelocityX(), -19f);
            }
            if (Input.isKeyDown(Input.Key.Left))
            {
                if ((player.fileName != "content/img/playerLeft"))
                {
                    player.Move(0, 0);
                    player.fileName = "content/img/playerLeft";
                }
                player.setVelocity(-8f, player.getVelocityY());
            }
            else
            {
                player.fileName = "content/img/playerIdle";
            }
            if (Input.isKeyDown(Input.Key.Right))
            {
                if ((player.fileName != "content/img/playerRight"))
                {
                    player.Move(0, 0);
                    player.fileName = "content/img/playerRight";
                }
                player.setVelocity(8f, player.getVelocityY());
            }


            if (!player.isColliding && player.fileName == "content/img/playerRight")
            {
                player.fileName = "content/img/playerJumpRight";
            }

            if (!player.isColliding && player.fileName == "content/img/playerLeft")
            {
                player.fileName = "content/img/playerJumpLeft";
            }
            Block b = new Block("content/img/Portal01.png", ((getMouseX() + 32) / 64) * 64, ((getMouseY() + 32) / 64) * 64, true);
            b.ID = 14;
            if (Input.isRightClicked() && Destroyed > 0)
            {
                bool test = false;
                foreach (Block block in blocks)
                {
                    if (b.getX() == block.getX() && b.getY() == block.getY())
                    {
                        test = true;
                    }
                }
                if (!test)
                {
                    Destroyed--;
                    blocks.Add(b);
                }
                return;
            }
        }
    }
}
