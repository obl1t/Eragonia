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
using System.IO;

namespace Eragonia_Demo_Day_One
{
    public class WaveLoader : Microsoft.Xna.Framework.Game
    {
        public World world;
        public List<int> spawnT = new List<int>();


        public WaveLoader()
        {
            enemiesPerWave.Add(0);
        }
        public List<int> enemiesPerWave = new List<int>();

        public void loadEnemies(String fileName, List<EnemySuper> enemi)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                try
                {
                    int i = 0;
                    while (!reader.EndOfStream)
                    {
                        String[] temp = reader.ReadLine().Split(' ');
                        String s = temp[0];
                        Char[] chars = s.ToCharArray();
                        enemiesPerWave.Add(chars.Length);
                        if(fileName.Equals(@"Content/Levels/cheatLevel.txt")) {
                           // Console.WriteLine(chars.Length);
                        }
                        for (int j = 0; j < chars.Length; j++)
                        {

                            if (chars[j] == 'm')
                            {
                                Measeling enemy = new Measeling();
                                enemy.rotation = 90;
                                enemy.spriteSheet = world.Content.Load<Texture2D>("Enemy/Measeling");
                                enemy.fireIcon = world.Content.Load<Texture2D>("Enemy/GUI/fireIcon");
                                enemy.position = new Rectangle(0, 480, 64, 64);
                                enemy.world = world;
                                enemi.Add(enemy);
                            }
                            else if (chars[j] == 'i')
                            {
                                Mire enemy = new Mire();
                                enemy.rotation = 90;
                                enemy.spriteSheet = world.Content.Load<Texture2D>("Enemy/Mire");
                                enemy.fireIcon = world.Content.Load<Texture2D>("Enemy/GUI/fireIcon");
                                enemy.position = new Rectangle(0, 480, 64, 64);
                                enemy.world = world;
                                enemi.Add(enemy);
                            }
                            else if (chars[j] == 'a')
                            {
                                Mammoth enemy = new Mammoth();
                                enemy.rotation = 90;
                                enemy.spriteSheet = world.Content.Load<Texture2D>("Enemy/Mammoth");
                                enemy.fireIcon = world.Content.Load<Texture2D>("Enemy/GUI/fireIcon");
                                enemy.position = new Rectangle(0, 480, 64, 64);
                                enemy.world = world;
                                enemi.Add(enemy);
                            }
                            else if (chars[j] == 'g')
                            {
                                Magnapinna enemy = new Magnapinna();
                                enemy.rotation = 90;
                                enemy.spriteSheet = world.Content.Load<Texture2D>("Enemy/Magnapinna");
                                enemy.fireIcon = world.Content.Load<Texture2D>("Enemy/GUI/fireIcon");
                                enemy.position = new Rectangle(0, 480, 64, 64);
                                enemy.world = world;
                                enemi.Add(enemy);
                            }
                            else if (chars[j] == 'c')
                            {
                                Mace enemy = new Mace();
                                enemy.rotation = 90;
                                enemy.spriteSheet = world.Content.Load<Texture2D>("Enemy/Mace");
                                enemy.passiveTexture = world.Content.Load<Texture2D>("Enemy/MaceS");
                                enemy.fireIcon = world.Content.Load<Texture2D>("Enemy/GUI/fireIcon");
                                enemy.position = new Rectangle(0, 480, 64, 64);
                                enemy.world = world;
                                enemy.loader = this;
                                enemi.Add(enemy);
                            }
                            else if (chars[j] == 'o')
                            {
                                Mossanite enemy = new Mossanite();
                                enemy.rotation = 90;
                                enemy.spriteSheet = world.Content.Load<Texture2D>("Enemy/Mossanite");
                                enemy.fireIcon = world.Content.Load<Texture2D>("Enemy/GUI/fireIcon");
                                enemy.position = new Rectangle(0, 480, 64, 64);
                                enemy.world = world;
                                enemi.Add(enemy);
                            }
                            else if (chars[j] == 'e')
                            {
                                Megaling enemy = new Megaling();
                                enemy.rotation = 90;
                                enemy.spriteSheet = world.Content.Load<Texture2D>("Enemy/Megaling");
                                enemy.fireIcon = world.Content.Load<Texture2D>("Enemy/GUI/fireIcon");
                                enemy.position = new Rectangle(0, 480, 64, 64);
                                enemy.world = world;
                                enemi.Add(enemy);
                            }
                            else if (chars[j] == 'y')
                            {
                                Mycelium enemy = new Mycelium();
                                enemy.rotation = 90;
                                enemy.spriteSheet = world.Content.Load<Texture2D>("Enemy/Mycellium");
                                enemy.fireIcon = world.Content.Load<Texture2D>("Enemy/GUI/fireIcon");
                                enemy.position = new Rectangle(0, 480, 64, 64);
                                enemy.world = world;
                                enemi.Add(enemy);
                            }
                            else if (chars[j] == 'r')
                            {
                                Mariner enemy = new Mariner();
                                enemy.rotation = 90;
                                enemy.spriteSheet = world.Content.Load<Texture2D>("Enemy/Mariner");
                                enemy.fireIcon = world.Content.Load<Texture2D>("Enemy/GUI/fireIcon");
                                enemy.position = new Rectangle(0, 480, 64, 64);
                                enemy.world = world;
                                enemi.Add(enemy);
                            }
                            else if (chars[j] == 'u')
                            {
                                Marauder enemy = new Marauder();
                                enemy.rotation = 90;
                                enemy.spriteSheet = world.Content.Load<Texture2D>("Enemy/Marauder");
                                enemy.fireIcon = world.Content.Load<Texture2D>("Enemy/GUI/fireIcon");
                                enemy.position = new Rectangle(0, 480, 64, 64);
                                enemy.world = world;
                                enemi.Add(enemy);
                            }
                            else if (chars[j] == 't')
                            {
                                Motivator enemy = new Motivator();
                                enemy.rotation = 90;
                                enemy.spriteSheet = world.Content.Load<Texture2D>("Enemy/Motivator");
                                enemy.fireIcon = world.Content.Load<Texture2D>("Enemy/GUI/fireIcon");
                                enemy.position = new Rectangle(0, 480, 64, 64);
                                enemy.world = world;
                                enemy.Initialize();
                                enemi.Add(enemy);
                            }
                            else if (chars[j] == 's')
                            {
                                Myster enemy = new Myster();
                                enemy.rotation = 90;
                                enemy.spriteSheet = world.Content.Load<Texture2D>("Enemy/Myster");
                                enemy.fireIcon = world.Content.Load<Texture2D>("Enemy/GUI/fireIcon");
                                enemy.position = new Rectangle(0, 480, 64, 64);
                                enemy.world = world;
                                enemi.Add(enemy);
                            }
                            else if (chars[j] == 'h')
                            {
                                Malachite enemy = new Malachite();
                                enemy.rotation = 90;
                                enemy.spriteSheet = world.Content.Load<Texture2D>("Enemy/Malachite");
                                enemy.fireIcon = world.Content.Load<Texture2D>("Enemy/GUI/fireIcon");
                                enemy.position = new Rectangle(0, 480, 64, 64);
                                enemy.world = world;
                                enemi.Add(enemy);
                            }
                            else if (chars[j] == 'w')
                            {
                                Mirage enemy = new Mirage();
                                enemy.rotation = 90;
                                enemy.spriteSheet = world.Content.Load<Texture2D>("Enemy/Mirage");
                                enemy.fireIcon = world.Content.Load<Texture2D>("Enemy/GUI/fireIcon");
                                enemy.position = new Rectangle(0, 480, 64, 64);
                                enemy.world = world;
                                enemy.loader = this;
                                enemi.Add(enemy);
                            }
                            else if (chars[j] == 'z')
                            {
                                Motorizer enemy = new Motorizer();
                                enemy.rotation = 90;
                                enemy.spriteSheet = world.Content.Load<Texture2D>("Enemy/MotorizerSlow");
                                enemy.chargeTexture = world.Content.Load<Texture2D>("Enemy/MotorizerCharge");
                                enemy.boostTexture = world.Content.Load<Texture2D>("Enemy/MotorizerFast");
                                enemy.fireIcon = world.Content.Load<Texture2D>("Enemy/GUI/fireIcon");
                                enemy.position = new Rectangle(0, 480, 64, 64);
                                enemy.world = world;
                                enemi.Add(enemy);
                            }


                            i++;
                        }
                        spawnT.Add(Int32.Parse(temp[1]));
                    }

                }
                catch
                {
                    Console.WriteLine(enemi.Count);
                }
            }
        }
        public void spawnMiniling(int xPos, int yPos, double pixMoved, int direction)
        {
            Miniling enemy = new Miniling();
            enemy.rotation = 90;
            enemy.spriteSheet = world.Content.Load<Texture2D>("Enemy/Miniling");
            enemy.pixelsMoved = pixMoved;
            enemy.position = new Rectangle(xPos, yPos, 64, 64);
            enemy.calculateOrigin();
            enemy.fireIcon = world.Content.Load<Texture2D>("Enemy/GUI/fireIcon");
            enemy.healthSprite = world.Content.Load<Texture2D>("Enemy/EnemyHealthBar");
            enemy.healthBar = new Rectangle(enemy.position.X, enemy.position.Y, enemy.position.Width, 10);
            enemy.dummyTexture = world.dummyTexture;
            enemy.world = world;
            enemy.movementDirection = direction;
            world.activeEnemies.Add(enemy);
        }
        public void spawnMyster()
        {
            Myster enemy = new Myster();
            enemy.rotation = 90;
            enemy.spriteSheet = world.Content.Load<Texture2D>("Enemy/Myster");
            enemy.position = new Rectangle(0, 480, 64, 64);
            enemy.calculateOrigin();
            enemy.healthSprite = world.Content.Load<Texture2D>("Enemy/EnemyHealthBar");
            enemy.healthBar = new Rectangle(enemy.position.X, enemy.position.Y, enemy.position.Width, 10);
            enemy.fireIcon = world.Content.Load<Texture2D>("Enemy/GUI/fireIcon");
            enemy.dummyTexture = world.dummyTexture;
            enemy.world = world;
            world.activeEnemies.Add(enemy);
        }
        public void spawnMotorizer()
        {
            Motorizer enemy = new Motorizer();
            enemy.rotation = 90;
            enemy.spriteSheet = world.Content.Load<Texture2D>("Enemy/MotorizerSlow");
            enemy.chargeTexture = world.Content.Load<Texture2D>("Enemy/MotorizerCharge");
            enemy.boostTexture = world.Content.Load<Texture2D>("Enemy/MotorizerFast");
            enemy.position = new Rectangle(0, 480, 64, 64);
            enemy.calculateOrigin();
            enemy.healthSprite = world.Content.Load<Texture2D>("Enemy/EnemyHealthBar");
            enemy.healthBar = new Rectangle(enemy.position.X, enemy.position.Y, enemy.position.Width, 10);
            enemy.fireIcon = world.Content.Load<Texture2D>("Enemy/GUI/fireIcon");
            enemy.dummyTexture = world.dummyTexture;
            enemy.world = world;
            world.activeEnemies.Add(enemy);
        }
        public void spawnShadow(int xPos, int yPos, double pixMoved, int direction)
        {
            MirageShadow enemy = new MirageShadow();
            enemy.rotation = 90;
            enemy.spriteSheet = world.Content.Load<Texture2D>("Enemy/MirageShadow");
            enemy.pixelsMoved = pixMoved;
            enemy.position = new Rectangle(xPos, yPos, 64, 64);
            enemy.calculateOrigin();
            enemy.fireIcon = world.Content.Load<Texture2D>("Enemy/GUI/fireIcon");
            enemy.healthSprite = world.Content.Load<Texture2D>("Enemy/EnemyHealthBar");
            enemy.healthBar = new Rectangle(enemy.position.X, enemy.position.Y, enemy.position.Width, 10);
            enemy.dummyTexture = world.dummyTexture;
            enemy.movementDirection = direction;
            enemy.canEndGame = false;
            enemy.world = world;
            world.activeEnemies.Add(enemy);
        }
    }
}
