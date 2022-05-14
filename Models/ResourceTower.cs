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
    public class ResourceTower
    {
        public Texture2D texture;
        public Rectangle position;
        public List<Rectangle> sources = new List<Rectangle>();
        public List<int> animationOrder = new List<int>();
        public int resourceType;
        public int resourceAmount;
        public ResourceBar bar;
        public int multiplier;
        public int fps = 5;
        public Boolean isFirstTower;
        public int currentIndex = 0;
        public int type = 0;
        public Boolean showRange;
        public World world;
        public Texture2D tierUpTex;
        public TierUpButton tierUp = new TierUpButton();
        public FormatterMaster formatter = new FormatterMaster();
        public MouseState oldMouse = Mouse.GetState();
        public int id;
        int timer = 0;
        public SpriteFont font;
        public Infobox box = new Infobox();
        public Texture2D infoTex;
        public int currentTier = 0;
        public Texture2D iconTex;
        public Icon icon = new Icon();
        public String[] infoTexts = new String[2];
        public void Intitialize()
        {
            tierUp.texture = tierUpTex;
            tierUp.position = formatter.formatRect(new Rectangle(position.X + 12, position.Y - 54, 40, 40));
            tierUp.resourceTower = this;
            tierUp.world = world;
            box.texture = infoTex;
            box.position = formatter.formatRect(new Rectangle(position.X - 93, position.Y - 225, 250, 300));
            box.text = "";
            box.font = font;
            box.isInitialized = true;
            icon.texture = world.Content.Load<Texture2D>("Towers/GUI/icons");
            icon.position = new Rectangle(position.X + 42, position.Y + 41, 30, 30);

            if (resourceType == 1)
            {
                icon.index = 0;
                infoTexts[0] = formatter.formatString(200, "50 Iron. \nUpgrade to Stone Miner");
                infoTexts[1] = formatter.formatString(200, "50 Steel. \nUpgrade to Stone Prospector");
            }
            if (resourceType == 2)
            {
                
                icon.index = 1;
                infoTexts[1] = formatter.formatString(200, "50 Steel. \nUpgrade to Iron Prospector");
            }
            if (resourceType == 3)
            {
                currentTier = 2;
                icon.index = 2;
                infoTexts[1] = formatter.formatString(200, "50 Steel. \nUpgrade to NANI?!");
            }

        }
        public void Update()
        {
            if (timer % fps == 0)
            {
                currentIndex++;
            }

            if (currentIndex >= animationOrder.Count)
            {
                currentIndex = 0;
            }



            timer++;



        }
        public void checkTier()
        {
            if (resourceType == 1 && currentTier == 0)
            {
                if (world.bar.resources[2] >= 50)
                {
                    world.bar.resources[2] -= 50;
                    world.shownTier = null;
                    world.shownInfoBox = null;
                    world.loader.replaceMiner(this);
                }
            }
            if (resourceType == 1 && currentTier == 1)
            {
                if (world.bar.resources[3] >= 50)
                {
                    world.bar.resources[3] -= 50;
                    world.shownTier = null;
                    world.shownInfoBox = null;
                    world.loader.replaceProspector(this);
                }
            }
            if (resourceType == 2 && currentTier == 1)
            {
                if (world.bar.resources[3] >= 50)
                {
                    world.bar.resources[3] -= 50;
                    world.shownTier = null;
                    world.shownInfoBox = null;
                    world.loader.replaceProspector(this);
                }
            }
        }
        public void isClicking(MouseState mouse)
        {
            if (world.era > currentTier)
            {
                tierUp.showInfoBox = true;
                box.text = infoTexts[currentTier];
            }
            else
            {
                tierUp.showInfoBox = false;
            }

            if (mouse.X >= position.X && mouse.X <= position.X + position.Width)
            {
                if (mouse.Y >= position.Y && mouse.Y <= position.Y + position.Height)
                {

                    if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed)
                    {
                        showRange = !showRange;
                        if (showRange)
                        {

                            world.shownTier = tierUp;
                            world.shownUpgrade = null;
                        }
                        else
                        {

                            world.shownTier = null;

                        }
                        world.shownRangeID = id;
                    }

                }
            }
            if (world.shownRangeID != id)
            {
                showRange = false;
            }
            if (tierUp.showInfoBox && showRange)
            {
                tierUp.isClicking(mouse);
            }
            oldMouse = mouse;
        }
        public void UpdateResources()
        {
            if (isFirstTower)
            {
                if (timer % 60 == 0)
                {

                    bar.updateResources(resourceType, resourceAmount / 2);
                    world.resourcesGained[resourceType] += resourceAmount / 2;
                }
            }
            else
            {
                if (timer % 120 == 0)
                {
                    bar.updateResources(resourceType, resourceAmount / 2);
                    world.resourcesGained[resourceType] += resourceAmount / 2;
                }
            }
        }
        public void addAnimationOrder(int[] order)
        {
            for (int i = 0; i < order.Length; i++)
            {
                animationOrder.Add(order[i] - 1);
            }
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle(position.X + 3, position.Y + 3, position.Width, position.Height), sources[animationOrder[currentIndex]], new Color(0, 0, 0, 120));
            spriteBatch.Draw(texture, position, sources[animationOrder[currentIndex]], Color.White);
            icon.Draw(gameTime, spriteBatch);
        }
    }
}
