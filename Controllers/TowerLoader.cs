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
    public class TowerLoader
    {
        public World world;
        public TowerSelector spawner;
        public int offset;
        public AttackSuper temp;
        public ResourceTower temp2;
        public void LoadSpawner(int index)
        {
            if (index == 0)
            {
                spawner = new TowerSelector();
                spawner.texture = world.Content.Load<Texture2D>("GUI/Thumbnails/clubberThumb");
                spawner.world = world;
                spawner.towerType = 0;
                spawner.setRange(96);
                spawner.Update(Mouse.GetState());
            }
            if (index == 1)
            {
                spawner = new TowerSelector();
                spawner.texture = world.Content.Load<Texture2D>("GUI/Thumbnails/bowerThumb");
                spawner.world = world;
                spawner.towerType = 1;
                spawner.setRange(350);
                spawner.Update(Mouse.GetState());
            }
            if (index == 2)
            {
                spawner = new TowerSelector();
                spawner.texture = world.Content.Load<Texture2D>("GUI/Thumbnails/knifeThumb");
                spawner.world = world;
                spawner.towerType = 2;
                spawner.setRange(210);
                spawner.Update(Mouse.GetState());
            }
            if (index == 3)
            {
                spawner = new TowerSelector();
                spawner.texture = world.Content.Load<Texture2D>("GUI/Thumbnails/torcherThumb");
                spawner.world = world;
                spawner.towerType = 3;
                spawner.setRange(96);
                spawner.Update(Mouse.GetState());
            }
            if (index == 4)
            {
                spawner = new TowerSelector();
                spawner.texture = world.Content.Load<Texture2D>("GUI/Thumbnails/catapultThumb");
                spawner.world = world;
                spawner.towerType = 4;
                spawner.setRange(500); 
                spawner.height = 64;
                spawner.Update(Mouse.GetState());
            }
            if (index == 5)
            {
                spawner = new TowerSelector();
                spawner.texture = world.Content.Load<Texture2D>("GUI/Thumbnails/teleThumb");
                spawner.world = world;
                spawner.towerType = 5;
                spawner.setRange(130);
                spawner.offset = -6;
                spawner.Update(Mouse.GetState());
            }
            spawner.offsetX = offset;
        }
        public void LoadResourceSpawner(int index)
        {
            if (index == 0)
            {
                spawner = new TowerSelector();
                spawner.texture = world.Content.Load<Texture2D>("GUI/Thumbnails/stonerThumb");
                spawner.world = world;
                spawner.towerType = 10;
                spawner.setRange(0);
                spawner.Update(Mouse.GetState());
            }
            if (index == 1)
            {
                spawner = new TowerSelector();
                spawner.texture = world.Content.Load<Texture2D>("GUI/Thumbnails/minerThumb");
                spawner.world = world;
                spawner.towerType = 11;
                spawner.setRange(0);
                spawner.Update(Mouse.GetState());
            }
            if (index == 2)
            {
                spawner = new TowerSelector();
                spawner.texture = world.Content.Load<Texture2D>("GUI/Thumbnails/prospectorThumb");
                spawner.world = world;
                spawner.towerType = 12;
                spawner.setRange(0);
                spawner.Update(Mouse.GetState());
            }
            spawner.offsetX = offset;


        }
        public void RemoveSpawner()
        {
            spawner = null;
        }

        public void placeTower(int index, int mouseX, int mouseY)
        {
            Tile t = world.getTile(mouseX, mouseY);
            if (t.isPlaceable && !t.towerPlaced)
            {


                int i = mouseX / 64;
                int j = mouseY / 64;
                if (index == 0)
                {
                    if (world.bar.resources[0] >= 75 && world.bar.resources[1] >= 40)
                    {
                        t.towerPlaced = true;
                        world.bar.resources[0] -= 75;
                        world.bar.resources[1] -= 40;
                        loadClubber(new Rectangle(i * 64 + 32, j * 64 + 26, 64, 70));
                        t.hasDecoration = false;
                    }
                }
                if (index == 1)
                {
                    if (world.bar.resources[0] >= 80 && world.bar.resources[1] >= 40)
                    {
                        t.towerPlaced = true;
                        world.bar.resources[0] -= 80;
                        world.bar.resources[1] -= 40;
                        loadBower(new Rectangle(i * 64 + 32, j * 64 + 26, 64, 70));
                        t.hasDecoration = false;
                    }
                }
                if (index == 2)
                {
                    if (world.bar.resources[0] >= 125 && world.bar.resources[1] >= 40)
                    {
                        t.towerPlaced = true;
                        world.bar.resources[0] -= 125;
                        world.bar.resources[1] -= 40;
                        loadKnifer(new Rectangle(i * 64 + 32, j * 64 + 26, 64, 70));
                        t.hasDecoration = false;
                    }
                }
                if (index == 3)
                {
                    if (world.bar.resources[0] >= 100 && world.bar.resources[1] >= 50)
                    {
                        t.towerPlaced = true;
                        world.bar.resources[0] -= 100;
                        world.bar.resources[1] -= 50;
                        loadTorcher(new Rectangle(i * 64 + 32, j * 64 + 26, 64, 70));
                        t.hasDecoration = false;
                    }
                }

                if (index == 4)
                {
                    if (world.bar.resources[0] >= 250 && world.bar.resources[1] >= 150 && world.bar.resources[2] >= 80)
                    {
                        t.towerPlaced = true;
                        world.bar.resources[0] -= 250;
                        world.bar.resources[1] -= 150;
                        world.bar.resources[2] -= 80;
                        loadCatapulter(new Rectangle(i * 64 + 32, j * 64 + 32, 64, 64));
                        t.hasDecoration = false;
                    }
                }

                if (index == 5)
                {
                    if (world.bar.resources[0] >= 200 && world.bar.resources[1] >= 200 && world.bar.resources[2] >= 100 && world.bar.resources[3] >= 70)
                    {
                        t.towerPlaced = true;
                        world.bar.resources[0] -= 200;
                        world.bar.resources[1] -= 200;
                        world.bar.resources[2] -= 100;
                        world.bar.resources[3] -= 70;
                        loadTelegram(new Rectangle(i * 64 + 32, j * 64 + 20, 64, 70));
                        t.hasDecoration = false;
                    }
                }
                if (index == 10)
                {
                    if (world.bar.resources[0] >= 75 && world.bar.resources[1] >= 30)
                    {
                        t.towerPlaced = true;
                        world.bar.resources[0] -= 75;
                        world.bar.resources[1] -= 30;
                        loadStoner(new Rectangle(i * 64, j * 64, 64, 70));
                        t.hasDecoration = false;
                    }
                }
                if (index == 11)
                {
                    if (world.bar.resources[0] >= 90 && world.bar.resources[1] >= 100)
                    {
                        t.towerPlaced = true;
                        world.bar.resources[0] -= 90;
                        world.bar.resources[1] -= 100;
                        loadMiner(new Rectangle(i * 64, j * 64, 64, 70));
                        t.hasDecoration = false;
                    }
                }
                if (index == 12)
                {
                    if (world.bar.resources[0] >= 105 && world.bar.resources[1] >= 200 && world.bar.resources[2] >= 100)
                    {
                        t.towerPlaced = true;
                        world.bar.resources[0] -= 105;
                        world.bar.resources[1] -= 200;
                        world.bar.resources[2] -= 100;
                        loadProspector(new Rectangle(i * 64, j * 64, 64, 70));
                        t.hasDecoration = false;
                    }
                }
                if(index == -1) {
                    t.towerPlaced = true;
                    loadWeakStoner(new Rectangle(i * 64, j * 64, 64, 70));
                    t.hasDecoration = false;
                }
                if(t.towerPlaced) {
                    world.sfx.PlaySound("towerPlace");
                }
            }
            spawner = null;
        }
        public void loadTorcher(Rectangle pos)
        {

            Torcher temp = new Torcher();
            temp.spriteSheet = world.Content.Load<Texture2D>("Towers/torcher");
            temp.position = pos;
            temp.calculateOrigin();
            temp.addRange(world.Content.Load<Texture2D>("Towers/GUI/range"), 96);
            temp.world = world;
            temp.shockTex = world.Content.Load<Texture2D>("Towers/GUI/shockwave");
            temp.id = world.towerID;
            world.towerID++;
            temp.infoTex = world.Content.Load<Texture2D>("GUI/infobox2");
            temp.font = world.Content.Load<SpriteFont>("Other/Font2");
            temp.upgradeTexture = world.Content.Load<Texture2D>("GUI/upgradeButton");
            temp.tierUpTex = world.Content.Load<Texture2D>("GUI/tierUpButton");
            temp.setUpgrade();
            world.attackTowers.Add(temp);
        }
        public void loadBower(Rectangle pos)
        {

            Bower temp = new Bower();
            temp.spriteSheet = world.Content.Load<Texture2D>("Towers/bower");
            temp.position = pos;
            temp.calculateOrigin();
            temp.addRange(world.Content.Load<Texture2D>("Towers/GUI/range"), 350);
            temp.world = world;
            temp.projectileTex = world.Content.Load<Texture2D>("Towers/GUI/arrow");
            temp.id = world.towerID;
            world.towerID++;
            temp.infoTex = world.Content.Load<Texture2D>("GUI/infobox2");
            temp.font = world.Content.Load<SpriteFont>("Other/Font2");
            temp.upgradeTexture = world.Content.Load<Texture2D>("GUI/upgradeButton");
            temp.tierUpTex = world.Content.Load<Texture2D>("GUI/tierUpButton");
            temp.setUpgrade();
            world.attackTowers.Add(temp);
        }

        public void loadClubber(Rectangle pos)
        {
            Clubber temp = new Clubber();
            temp.spriteSheet = world.Content.Load<Texture2D>("Towers/clubber");
            temp.position = pos;
            temp.calculateOrigin();
            temp.addRange(world.Content.Load<Texture2D>("Towers/GUI/range"), 96);
            temp.world = world;
            temp.shockTex = world.Content.Load<Texture2D>("Towers/GUI/shockwave");
            temp.id = world.towerID;
            world.towerID++;
            temp.infoTex = world.Content.Load<Texture2D>("GUI/infobox2");
            temp.font = world.Content.Load<SpriteFont>("Other/Font2");
            temp.upgradeTexture = world.Content.Load<Texture2D>("GUI/upgradeButton");
            temp.tierUpTex = world.Content.Load<Texture2D>("GUI/tierUpButton");
            temp.setUpgrade();
            world.attackTowers.Add(temp);
        }

        public void loadKnifer(Rectangle pos)
        {
            KnifeThrower temp = new KnifeThrower();
            temp.spriteSheet = world.Content.Load<Texture2D>("Towers/knifer");
            temp.position = pos;
            temp.calculateOrigin();
            temp.addRange(world.Content.Load<Texture2D>("Towers/GUI/range"), 210);
            temp.world = world;
            temp.projectileTex = world.Content.Load<Texture2D>("Towers/GUI/knife");
            temp.id = world.towerID;
            world.towerID++;
            temp.infoTex = world.Content.Load<Texture2D>("GUI/infobox2");
            temp.font = world.Content.Load<SpriteFont>("Other/Font2");
            temp.upgradeTexture = world.Content.Load<Texture2D>("GUI/upgradeButton");
            temp.tierUpTex = world.Content.Load<Texture2D>("GUI/tierUpButton");
            temp.setUpgrade();
            world.attackTowers.Add(temp);
        }
        public void loadCatapulter(Rectangle pos)
        {
            Catapulter temp = new Catapulter();
            temp.spriteSheet = world.Content.Load<Texture2D>("Towers/catapult");
            temp.position = pos;
            temp.calculateOrigin();
            temp.addRange(world.Content.Load<Texture2D>("Towers/GUI/range"), 500);
            temp.world = world;

            temp.explosionTex = world.Content.Load<Texture2D>("Towers/GUI/explosion");
            temp.shadowTex = world.Content.Load<Texture2D>("Towers/GUI/shadow");
            temp.setProjectile();
            temp.id = world.towerID;
            world.towerID++;
            temp.infoTex = world.Content.Load<Texture2D>("GUI/infobox2");
            temp.font = world.Content.Load<SpriteFont>("Other/Font2");
            temp.upgradeTexture = world.Content.Load<Texture2D>("GUI/upgradeButton");
            temp.tierUpTex = world.Content.Load<Texture2D>("GUI/tierUpButton");
            temp.setUpgrade();
            temp.setAttack(); //
            world.attackTowers.Add(temp);
        }

        public void replaceRogue(AttackSuper replacing)
        {
            Rogue temp = new Rogue();

            temp.spriteSheet = world.Content.Load<Texture2D>("Towers/rogue");
            temp.position = replacing.position;
            world.attackTowers.Remove(replacing);
            temp.calculateOrigin();
            temp.addRange(world.Content.Load<Texture2D>("Towers/GUI/range"), 210);
            temp.world = world;
            temp.projectileTex = world.Content.Load<Texture2D>("Towers/GUI/knife");
            temp.id = world.towerID;
            world.towerID++;
            temp.infoTex = world.Content.Load<Texture2D>("GUI/infobox2");
            temp.font = world.Content.Load<SpriteFont>("Other/Font2");
            temp.upgradeTexture = world.Content.Load<Texture2D>("GUI/upgradeButton");
            temp.tierUpTex = world.Content.Load<Texture2D>("GUI/tierUpButton");
            temp.setUpgrade();
            world.attackTowers.Add(temp);
        }

        public void replaceKnight(AttackSuper replacing)
        {
            Knight temp = new Knight();
            temp.spriteSheet = world.Content.Load<Texture2D>("Towers/knight");
            temp.position = replacing.position;
            world.attackTowers.Remove(replacing);
            temp.calculateOrigin();
            temp.addRange(world.Content.Load<Texture2D>("Towers/GUI/range"), 96);
            temp.world = world;
            temp.shockTex = world.Content.Load<Texture2D>("Towers/GUI/shockwave");
            temp.id = world.towerID;
            world.towerID++;
            temp.infoTex = world.Content.Load<Texture2D>("GUI/infobox2");
            temp.font = world.Content.Load<SpriteFont>("Other/Font2");
            temp.upgradeTexture = world.Content.Load<Texture2D>("GUI/upgradeButton");
            temp.tierUpTex = world.Content.Load<Texture2D>("GUI/tierUpButton");
            temp.setUpgrade();
            world.attackTowers.Add(temp);
        }

        public void replaceCrossbower(AttackSuper replacing)
        {
            Crossbower temp = new Crossbower();
            temp.spriteSheet = world.Content.Load<Texture2D>("Towers/crossbower");
            temp.position = replacing.position;
            world.attackTowers.Remove(replacing);
            temp.calculateOrigin();
            temp.addRange(world.Content.Load<Texture2D>("Towers/GUI/range"), 420);
            temp.world = world;
            temp.projectileTex = world.Content.Load<Texture2D>("Towers/GUI/arrow");
            temp.id = world.towerID;
            world.towerID++;
            temp.infoTex = world.Content.Load<Texture2D>("GUI/infobox2");
            temp.font = world.Content.Load<SpriteFont>("Other/Font2");
            temp.upgradeTexture = world.Content.Load<Texture2D>("GUI/upgradeButton");
            temp.tierUpTex = world.Content.Load<Texture2D>("GUI/tierUpButton");
            temp.setUpgrade();
            world.attackTowers.Add(temp);
        }
        public void replacePyro(AttackSuper replacing)
        {
            Pyromancer temp = new Pyromancer();
            temp.spriteSheet = world.Content.Load<Texture2D>("Towers/pyromancer");
            temp.position = replacing.position;
            world.attackTowers.Remove(replacing);
            temp.calculateOrigin();
            temp.addRange(world.Content.Load<Texture2D>("Towers/GUI/range"), 96);
            temp.world = world;
            temp.shockTex = world.Content.Load<Texture2D>("Towers/GUI/shockwave");
            temp.id = world.towerID;
            world.towerID++;
            temp.infoTex = world.Content.Load<Texture2D>("GUI/infobox2");
            temp.font = world.Content.Load<SpriteFont>("Other/Font2");
            temp.upgradeTexture = world.Content.Load<Texture2D>("GUI/upgradeButton");
            temp.tierUpTex = world.Content.Load<Texture2D>("GUI/tierUpButton");
            temp.setUpgrade();
            world.attackTowers.Add(temp);
        }

        public void replaceAssasin(AttackSuper replacing)
        {
            Assasin temp = new Assasin();

            temp.spriteSheet = world.Content.Load<Texture2D>("Towers/assasin");
            temp.position = replacing.position;
            world.attackTowers.Remove(replacing);
            temp.calculateOrigin();
            temp.addRange(world.Content.Load<Texture2D>("Towers/GUI/range"), 210);
            temp.world = world;
            temp.projectileTex = world.Content.Load<Texture2D>("Towers/GUI/knife");
            temp.id = world.towerID;
            world.towerID++;
            temp.infoTex = world.Content.Load<Texture2D>("GUI/infobox2");
            temp.font = world.Content.Load<SpriteFont>("Other/Font2");
            temp.upgradeTexture = world.Content.Load<Texture2D>("GUI/upgradeButton");
            temp.tierUpTex = world.Content.Load<Texture2D>("GUI/tierUpButton");
            temp.setUpgrade();
            world.attackTowers.Add(temp);
        }

        public void replaceSmelter(AttackSuper replacing)
        {
            Smelter temp = new Smelter();
            temp.spriteSheet = world.Content.Load<Texture2D>("Towers/smelter");
            temp.position = replacing.position;
            world.attackTowers.Remove(replacing);
            temp.calculateOrigin();
            temp.addRange(world.Content.Load<Texture2D>("Towers/GUI/range"), 96);
            temp.world = world;
            temp.shockTex = world.Content.Load<Texture2D>("Towers/GUI/shockwave");
            temp.id = world.towerID;
            world.towerID++;
            temp.infoTex = world.Content.Load<Texture2D>("GUI/infobox2");
            temp.font = world.Content.Load<SpriteFont>("Other/Font2");
            temp.upgradeTexture = world.Content.Load<Texture2D>("GUI/upgradeButton");
            temp.tierUpTex = world.Content.Load<Texture2D>("GUI/tierUpButton");
            temp.setUpgrade();
            world.attackTowers.Add(temp);
        }

        public void replaceSamurai(AttackSuper replacing)
        {
            Samurai temp = new Samurai();
            temp.spriteSheet = world.Content.Load<Texture2D>("Towers/samurai");
            temp.position = replacing.position;
            world.attackTowers.Remove(replacing);
            temp.calculateOrigin();
            temp.addRange(world.Content.Load<Texture2D>("Towers/GUI/range"), 96);
            temp.world = world;
            temp.shockTex = world.Content.Load<Texture2D>("Towers/GUI/shockwave");
            temp.id = world.towerID;
            world.towerID++;
            temp.infoTex = world.Content.Load<Texture2D>("GUI/infobox2");
            temp.font = world.Content.Load<SpriteFont>("Other/Font2");
            temp.upgradeTexture = world.Content.Load<Texture2D>("GUI/upgradeButton");
            temp.tierUpTex = world.Content.Load<Texture2D>("GUI/tierUpButton");
            temp.setUpgrade();
            world.attackTowers.Add(temp);
        }

        public void replaceMortar(AttackSuper replacing)
        {
            Mortar temp = new Mortar();
            temp.spriteSheet = world.Content.Load<Texture2D>("Towers/mortar");
            temp.position = replacing.position;
            world.attackTowers.Remove(replacing);
            temp.calculateOrigin();
            temp.addRange(world.Content.Load<Texture2D>("Towers/GUI/range"), 500);
            temp.world = world;

            temp.explosionTex = world.Content.Load<Texture2D>("Towers/GUI/explosion");
            temp.shadowTex = world.Content.Load<Texture2D>("Towers/GUI/shadow");
            temp.setProjectile();
            temp.id = world.towerID;
            world.towerID++;
            temp.infoTex = world.Content.Load<Texture2D>("GUI/infobox2");
            temp.font = world.Content.Load<SpriteFont>("Other/Font2");
            temp.upgradeTexture = world.Content.Load<Texture2D>("GUI/upgradeButton");
            temp.tierUpTex = world.Content.Load<Texture2D>("GUI/tierUpButton");
            temp.setUpgrade();
            temp.setAttack(); //
            world.attackTowers.Add(temp);
        }

        public void replaceMusket(AttackSuper replacing)
        {
            Musketeer temp = new Musketeer();
            temp.spriteSheet = world.Content.Load<Texture2D>("Towers/musketeer");
            temp.position = replacing.position;
            world.attackTowers.Remove(replacing);
            temp.calculateOrigin();
            temp.addRange(world.Content.Load<Texture2D>("Towers/GUI/range"), 420);
            temp.world = world;
            temp.projectileTex = world.Content.Load<Texture2D>("Towers/GUI/musketball");
            temp.id = world.towerID;
            world.towerID++;
            temp.infoTex = world.Content.Load<Texture2D>("GUI/infobox2");
            temp.font = world.Content.Load<SpriteFont>("Other/Font2");
            temp.upgradeTexture = world.Content.Load<Texture2D>("GUI/upgradeButton");
            temp.tierUpTex = world.Content.Load<Texture2D>("GUI/tierUpButton");
            temp.setUpgrade();
            world.attackTowers.Add(temp);
        }

        public void loadTelegram(Rectangle pos) {
            Telegram temp = new Telegram();
            temp.spriteSheet = world.Content.Load<Texture2D>("Towers/telegraph");
            temp.position = pos;
            
            temp.calculateOrigin();
            temp.addRange(world.Content.Load<Texture2D>("Towers/GUI/range"), 130);
            temp.world = world;
           
            temp.id = world.towerID;
            world.towerID++;
            temp.infoTex = world.Content.Load<Texture2D>("GUI/infobox2");
            temp.font = world.Content.Load<SpriteFont>("Other/Font2");
            temp.upgradeTexture = world.Content.Load<Texture2D>("GUI/upgradeButton");
            temp.tierUpTex = world.Content.Load<Texture2D>("GUI/tierUpButton");
            temp.setUpgrade();
            world.attackTowers.Add(temp);
        }
        public void loadStoner(Rectangle pos)
        {
            temp2 = new ResourceTower();
            temp2.addAnimationOrder(new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 3, 3, 3, 3, 4, 4, 4, 4, 3, 3, 3, 3, 4, 4, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 1, 1, 1, 1 });
            temp2.texture = world.Content.Load<Texture2D>("Towers/digger");
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    temp2.sources.Add(new Rectangle(j * 832, i * 910, 832, 910));
                }
            }
            temp2.bar = world.bar;
            temp2.world = world;
            
            if (!world.hasPlacedResourceTower[0])
            {
                world.hasPlacedResourceTower[0] = true;
                temp2.isFirstTower = true;
            }
            else
            {
                temp2.isFirstTower = false;
            }
            temp2.position = pos;
            temp2.resourceAmount = 2;
            temp2.resourceType = 1;
            temp2.id = world.towerID;
            world.towerID++;
            temp2.tierUpTex = world.Content.Load<Texture2D>("GUI/tierUpButton");
            temp2.infoTex = world.Content.Load<Texture2D>("GUI/infobox2");
            temp2.font = world.Content.Load<SpriteFont>("Other/Font2");

            temp2.Intitialize();

            world.resourceTowers.Add(temp2);
        }

        public void loadWeakStoner(Rectangle pos)
        {
            temp2 = new ResourceTower();
            temp2.addAnimationOrder(new int[] { 1, 1, 1, 1, 1, 1, 1, 2, 3, 3, 3, 3, 4, 4, 3, 3, 4, 4, 3, 3, 4, 4, 3, 3, 3, 3, 3, 2, 2, 1, 1 });
            temp2.texture = world.Content.Load<Texture2D>("Towers/digger");
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    temp2.sources.Add(new Rectangle(j * 832, i * 910, 832, 910));
                }
            }
            temp2.bar = world.bar;
            temp2.world = world;
            temp2.isFirstTower = false;
            temp2.position = pos;
            temp2.resourceAmount = 1;
            temp2.resourceType = 1;
            temp2.id = world.towerID;
            world.towerID++;
            temp2.tierUpTex = world.Content.Load<Texture2D>("GUI/tierUpButton");
            temp2.infoTex = world.Content.Load<Texture2D>("GUI/infobox2");
            temp2.font = world.Content.Load<SpriteFont>("Other/Font2");

            temp2.Intitialize();

            world.resourceTowers.Add(temp2);
        }
        public void loadMiner(Rectangle pos)
        {
            temp2 = new ResourceTower();
            temp2.addAnimationOrder(new int[] { 1, 2, 3, 4, 5, 6, 1, 1, 1, 1 });
            temp2.texture = world.Content.Load<Texture2D>("Towers/miner");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    temp2.sources.Add(new Rectangle(j * 512, i * 560, 512, 560));
                }
            }
            temp2.bar = world.bar;
            temp2.world = world;
            if (!world.hasPlacedResourceTower[1])
            {
                world.hasPlacedResourceTower[1] = true;
                temp2.isFirstTower = true;
            }
            else
            {
                temp2.isFirstTower = false;
            }
            temp2.position = pos;
            temp2.resourceAmount = 2;
            temp2.resourceType = 2;
            temp2.id = world.towerID;
            world.towerID++;
            temp2.tierUpTex = world.Content.Load<Texture2D>("GUI/tierUpButton");
            temp2.infoTex = world.Content.Load<Texture2D>("GUI/infobox2");
            temp2.font = world.Content.Load<SpriteFont>("Other/Font2");
            temp2.Intitialize();
            temp2.currentTier = 1;
            world.resourceTowers.Add(temp2);
        }

        public void replaceMiner(ResourceTower replacing)
        {
            temp2 = new ResourceTower();
            temp2.addAnimationOrder(new int[] { 1, 2, 3, 4, 5, 6, 1, 1, 1, 1 });
            temp2.texture = world.Content.Load<Texture2D>("Towers/miner");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    temp2.sources.Add(new Rectangle(j * 512, i * 560, 512, 560));
                }
            }
            temp2.bar = world.bar;
            temp2.world = world;
            if (!world.hasPlacedResourceTower[1])
            {
                world.hasPlacedResourceTower[1] = true;
                temp2.isFirstTower = true;
            }
            else
            {
                temp2.isFirstTower = false;
            }
            temp2.position = replacing.position;
            world.resourceTowers.Remove(replacing);
            temp2.resourceAmount = 4;
            temp2.resourceType = 1;
            temp2.id = world.towerID;
            world.towerID++;
            temp2.currentTier = 1;
            temp2.tierUpTex = world.Content.Load<Texture2D>("GUI/tierUpButton");
            temp2.infoTex = world.Content.Load<Texture2D>("GUI/infobox2");
            temp2.font = world.Content.Load<SpriteFont>("Other/Font2");
            temp2.Intitialize();
            world.resourceTowers.Add(temp2);
        }
        public void loadProspector(Rectangle pos) {
            temp2 = new ResourceTower();
            temp2.addAnimationOrder(new int[] { 1, 2, 3, 4, 5, 6, 1, 1, 1, 1 });
            temp2.texture = world.Content.Load<Texture2D>("Towers/prospector");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    temp2.sources.Add(new Rectangle(j * 512, i * 560, 512, 560));
                }
            }
            temp2.bar = world.bar;
            temp2.world = world;
            if (!world.hasPlacedResourceTower[1])
            {
                world.hasPlacedResourceTower[1] = true;
                temp2.isFirstTower = true;
            }
            else
            {
                temp2.isFirstTower = false;
            }
            temp2.position = pos;
            temp2.resourceType = 3;
            temp2.resourceAmount = 2;


            temp2.id = world.towerID;
            world.towerID++;
            temp2.currentTier = 2;
            temp2.tierUpTex = world.Content.Load<Texture2D>("GUI/tierUpButton");
            temp2.infoTex = world.Content.Load<Texture2D>("GUI/infobox2");
            temp2.font = world.Content.Load<SpriteFont>("Other/Font2");
            temp2.Intitialize();
            world.resourceTowers.Add(temp2);
        }
        public void replaceProspector(ResourceTower replacing)
        {
            temp2 = new ResourceTower();
            temp2.addAnimationOrder(new int[] { 1, 2, 3, 4, 5, 6, 1, 1, 1, 1 });
            temp2.texture = world.Content.Load<Texture2D>("Towers/prospector");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    temp2.sources.Add(new Rectangle(j * 512, i * 560, 512, 560));
                }
            }
            temp2.bar = world.bar;
            temp2.world = world;
            temp2.isFirstTower = false;
            temp2.position = replacing.position;
            if (replacing.resourceType == 1)
            {
                temp2.resourceType = 1;
                temp2.resourceAmount = 8;
            }
            else {
                temp2.resourceType = 2;
                temp2.resourceAmount = 4;
            }
            world.resourceTowers.Remove(replacing);
            
            
            temp2.id = world.towerID;
            world.towerID++;
            temp2.currentTier = 2;
            temp2.tierUpTex = world.Content.Load<Texture2D>("GUI/tierUpButton");
            temp2.infoTex = world.Content.Load<Texture2D>("GUI/infobox2");
            temp2.font = world.Content.Load<SpriteFont>("Other/Font2");
            temp2.Intitialize();
            world.resourceTowers.Add(temp2);
        }

    }
}
