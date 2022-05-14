using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Eragonia_Demo_Day_One
{
    public class EnemyInfoManager
    {
        List<EnemyInfo> enemyInfos = new List<EnemyInfo>();
        List<int> wavesToCall = new List<int>();
        public World world;
        public FormatterMaster formatter = new FormatterMaster();
        public Boolean shouldDisplay = false;
        public int currentIndex = 0;
        public int offset;
        public void Initialize() {
            EnemyInfo temp = new EnemyInfo();
            temp.world = world;
            temp.enemyTex = world.Content.Load<Texture2D>("Enemy/Measeling");
            temp.title = "Measeling";
            temp.stats = "Health: 25 \n\nSpeed: 2";
            temp.desc = formatter.formatString(450, "The runt of the Miscreants. Weak and frail. Has no special abilities, nor special weaknesses.");
            temp.enemySource = new Rectangle(0, 0, 682, 682);
            temp.Initialize();
            enemyInfos.Add(temp);
            wavesToCall.Add(1);
            //
            temp = new EnemyInfo();
            temp.world = world;
            temp.enemyTex = world.Content.Load<Texture2D>("Enemy/Mire");
            temp.title = "Mire";
            temp.stats = "Health: 150 \n\nSpeed: 1";
            temp.desc = formatter.formatString(450, "The Mire is tanky, but slow. While weak by itself, it can distract towers from faster enemies.", 35);
            temp.enemySource = new Rectangle(0, 0, 640, 640);
            temp.Initialize();
            enemyInfos.Add(temp);
            wavesToCall.Add(3);
            //
            temp = new EnemyInfo();
            temp.world = world;
            temp.enemyTex = world.Content.Load<Texture2D>("Enemy/Magnapinna");
            temp.title = "Magnapinna";
            temp.stats = "Health: 50 \n\nSpeed: 5 -> 3";
            temp.desc = formatter.formatString(450, "One of the fastest Miscreants. Hard to hit initially, but slows down upon taking damage.");
            temp.enemySource = new Rectangle(0, 0, 640, 640);
            temp.Initialize();
            enemyInfos.Add(temp);
            wavesToCall.Add(6);
            //
            temp = new EnemyInfo();
            temp.world = world;
            temp.enemyTex = world.Content.Load<Texture2D>("Enemy/Mammoth");
            temp.title = "Mammoth";
            temp.stats = "Health: 1500 \n\nSpeed: 1";
            temp.desc = formatter.formatString(450, "Extremely beefy, but slow. The dark energy that makes up this creature is extremely flammable.");
            temp.enemySource = new Rectangle(0, 0, 640, 640);
            temp.Initialize();
            enemyInfos.Add(temp);
            wavesToCall.Add(8);
            //
            temp = new EnemyInfo();
            temp.world = world;
            temp.enemyTex = world.Content.Load<Texture2D>("Enemy/Mace");
            temp.title = "Mace";
            temp.stats = "Health: 280 \n\nSpeed: 1";
            temp.desc = formatter.formatString(450, "Weak alone, but a master of swarms. Periodically stops to spawn its flighty, but frail kin.");
            temp.Initialize();
            temp.enemySource = new Rectangle(0, 0, 320, 320);
            enemyInfos.Add(temp);
            wavesToCall.Add(11);
            //
            temp = new EnemyInfo();
            temp.world = world;
            temp.enemyTex = world.Content.Load<Texture2D>("Enemy/Mossanite");
            temp.title = "Mossanite";
            temp.stats = "Health: 500 \n\nSpeed: 2";
            temp.desc = formatter.formatString(450, "A sturdy Miscreant. Has no special abilities, but can still pose a formidable danger.");
            temp.Initialize();
            temp.enemySource = new Rectangle(0, 0, 640, 640);
            enemyInfos.Add(temp);
            wavesToCall.Add(13);
            //
            temp = new EnemyInfo();
            temp.world = world;
            temp.enemyTex = world.Content.Load<Texture2D>("Enemy/Mycellium");
            temp.title = "Mycelium";
            temp.stats = "Health: 300 \n\nSpeed: 3";
            temp.desc = formatter.formatString(450, "Designed after the sturdy Agaricus Bisporus, the Mycelium completely negates the first hit on it.");
            temp.Initialize();
            temp.enemySource = new Rectangle(0, 0, 682, 682);
            enemyInfos.Add(temp);
            wavesToCall.Add(16);
            //
            temp = new EnemyInfo();
            temp.world = world;
            temp.enemyTex = world.Content.Load<Texture2D>("Enemy/Mariner");
            temp.title = "Mariner";
            temp.stats = "Health: 800 \n\nSpeed: 2";
            temp.desc = formatter.formatString(450, "Mixed with the Mariner's dark energy is unflammable liquid. Fire damage is futile against it.");
            temp.Initialize();
            temp.enemySource = new Rectangle(0, 0, 682, 682);
            enemyInfos.Add(temp);
            wavesToCall.Add(18);
            //
            temp = new EnemyInfo();
            temp.world = world;
            temp.enemyTex = world.Content.Load<Texture2D>("Enemy/Marauder");
            temp.title = "Marauder";
            temp.stats = "Health: 650 \n\nSpeed: 2";
            temp.desc = formatter.formatString(450, "Has a design that softens the impact of projectiles. Resistant to knives, arrows, and musket balls.");
            temp.Initialize();
            temp.enemySource = new Rectangle(0, 0, 682, 682);
            enemyInfos.Add(temp);
            wavesToCall.Add(21);
            //
            temp = new EnemyInfo();
            temp.world = world;
            temp.enemyTex = world.Content.Load<Texture2D>("Enemy/Motivator");
            temp.title = "Motivator";
            temp.stats = "Health: 3000 \n\nSpeed: 1";
            temp.desc = formatter.formatString(450, "Stops moving a few meters into the track. Speeds up Miscreants that pass over it.");
            temp.Initialize();
            temp.enemySource = new Rectangle(0, 0, 682, 682);
            enemyInfos.Add(temp);
            wavesToCall.Add(24);
            //
            temp = new EnemyInfo();
            temp.world = world;
            temp.enemyTex = world.Content.Load<Texture2D>("Enemy/Megaling");
            temp.title = "Megaling";
            temp.stats = "Health: 2000 \n\nSpeed: 2";
            temp.desc = formatter.formatString(450, "Durable, yet remarkably fast for its size. The strongest Miscreant without abilities.");
            temp.Initialize();
            temp.enemySource = new Rectangle(0, 0, 512, 512);
            enemyInfos.Add(temp);
            wavesToCall.Add(27);
            //
            temp = new EnemyInfo();
            temp.world = world;
            temp.enemyTex = world.Content.Load<Texture2D>("Enemy/Myster");
            temp.title = "Myster";
            temp.stats = "Health: 4000 \n\nSpeed: 1";
            temp.desc = formatter.formatString(450, "Coated with regenerative dark energy. Slowly heals when injured.");
            temp.Initialize();
            temp.enemySource = new Rectangle(0, 0, 682, 682);
            enemyInfos.Add(temp);
            wavesToCall.Add(30);
            //
            temp = new EnemyInfo();
            temp.world = world;
            temp.enemyTex = world.Content.Load<Texture2D>("Enemy/Malachite");
            temp.title = "Malachite";
            temp.stats = "Health: 1500 \n\nSpeed: 2";
            temp.desc = formatter.formatString(450, "Bred for stealth, the Malachite turns invisible for a short time after being first hit. ");
            temp.Initialize();
            temp.enemySource = new Rectangle(0, 0, 682, 682);
            enemyInfos.Add(temp);
            wavesToCall.Add(33);
          
            //
            temp = new EnemyInfo();
            temp.world = world;
            temp.enemyTex = world.Content.Load<Texture2D>("Enemy/Mirage");
            temp.title = "Mirage";
            temp.stats = "Health: 2500 \n\nSpeed: 2";
            temp.desc = formatter.formatString(450, "A master of illusions. Creates accurate, but harmless replicas of itself to distract your towers.");
            temp.Initialize();
            temp.enemySource = new Rectangle(0, 0, 682, 682);
            enemyInfos.Add(temp);
            wavesToCall.Add(35);

            //
            temp = new EnemyInfo();
            temp.world = world;
            temp.enemyTex = world.Content.Load<Texture2D>("Enemy/MotorizerSlow");
            temp.title = "Motorizer";
            temp.stats = "Health: 3000 \n\nSpeed: 2";
            temp.desc = formatter.formatString(450, "The strongest Miscreant. Charges up a burst of dark energy that propels it to insane speeds.");
            temp.Initialize();
            temp.enemySource = new Rectangle(0, 0, 682, 682);
            enemyInfos.Add(temp);
            wavesToCall.Add(37);
        }

        public void checkInfo(int wave) {
            if(wavesToCall.Contains(wave)) {
                if(currentIndex < enemyInfos.Count) {
                    enemyInfos[currentIndex].setPosition(offset);
                    world.shownInfo = enemyInfos[currentIndex];
                    world.gS = World.gameState.ShowInfo;
                    world.sfx.PlaySoundQuietly("waveStart");
                    currentIndex++;
                }
            }
            else {
                if (!world.isPreparingForBoss)
                {
                    world.sfx.PlaySound("waveStart");
                }
            }
        }

        public void showInfo(int index) {
            enemyInfos[currentIndex].setPosition(offset);
            world.shownInfo = enemyInfos[index];
            world.gS = World.gameState.ShowInfo;
            world.sfx.PlaySoundQuietly("waveStart");
            currentIndex++;
        }
    }
}
