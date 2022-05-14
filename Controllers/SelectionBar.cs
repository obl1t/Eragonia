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
    public class SelectionBar
    {
        public Texture2D texture;
        public Rectangle position;
        public List<Texture2D> attackTowerTextures = new List<Texture2D>();
        public List<Texture2D> resourceTowerTextures = new List<Texture2D>();
        public MouseState oldMouse = Mouse.GetState();
        public enum ViewType { attack, resource, tierUp };
        public enum Era { Stone, Medieval, Industrial, Modern };
        public Era era;
        public ViewType viewType = ViewType.attack;
        public Texture2D dummyTexture;
        public Texture2D infoTexture;
        public SpriteFont font;
        public Rectangle dummyPos;
        public Boolean drawSelection = false;
        public ResourceBar resources;
        public World world;
        public BarButtons buttons;
        public Infobox box = new Infobox();
        public String[] infoTexts = new String[15];
        public SpriteFont tierFont;
        public String tierText = "MEDIEVAL ERA";
        public String neededItemText = "  100 GOLD  300 STONE";
        public SelectorTierUp tierUp = new SelectorTierUp();
        public Texture2D tierUpTex;
        public int offset;
        public FormatterMaster formatter = new FormatterMaster();
        public void Initialize()
        {
            buttons = new BarButtons();
            for (int i = 0; i < 15; i++)
            {
                infoTexts[i] = " ";
            }
            box.texture = infoTexture;
            box.font = font;
            box.position = new Rectangle(-250, 500, 250, 300);
            infoTexts[0] = "\n75 GOLD \n40 STONE \n\n\n\n\nThe clubber\nlikes smashing \nthings. \nBhaykerr's \nminions are \nno exception.\n\nDamage: Med\nRange: 100\nAverage Speed\nMild AOE";
            infoTexts[1] = "\n80 GOLD \n40 STONE \n\n\n\n\nShoots enemies \nwith deadly \naccuracy. For \na caveman, \nthat is. \n\n\nDamage: High\nRange: 350\nSlow Speed\nSingle-target";
            infoTexts[2] = "\n125 GOLD \n40 STONE \n\n\nWhen asked \nwhere he \ngot the \nendless supply \nof knives\nfrom, the\nknifer replied \n\"Ooga Booga\" \n\nDamage: Low\nRange: 210\nFast Speed\nSingle-target";
            infoTexts[3] = "\n100 GOLD \n50 STONE \n\n\n\n\nChannels fire \nto burn \nenemies into \na pile\nof ash.\n\nDamage: Low\nRange: 100\nAverage Speed\nMild DOT";
            infoTexts[4] = "\n250 GOLD \n150 STONE \n80 IRON \n\n\n\n\nLobs heavy \nprojectiles \nat enemies \n\n\nDamage: Extreme\nRange: 500\nGlacial Speed\nLarge AOE";
            infoTexts[5] = "\nTELEGRAM\nTOWER\n200 GOLD \n200 STONE \n100 IRON \n70 STEEL \n\n\nThe latest\ntechnology!\nBuffs towers\nwithin range\n\n\nRange: 200";
            infoTexts[10] = "\n75 GOLD \n30 STONE \n\n\n\n\nDigs up \npebbles below\nthe surface\nof the\nplentiful \nvalley. \n\nCollection: \n1 Stone\nper second";
            infoTexts[11] = "\n90 GOLD \n100 STONE \n\n\n\n\nMines the \niron below\nthe surface\nof the\nplentiful \nvalley. \n\nCollection: \n1 Iron\nper second";
            infoTexts[12] = "\n105 GOLD \n200 STONE \n100 IRON \n\n\n\nMines the \nsteel below\nthe surface\nof the\nplentiful \nvalley. \n\nCollection: \n1 Steel\nper second";
            box.isInitialized = true;
            tierUp.selector = this;
            tierUp.world = this.world;
            tierUp.texture = tierUpTex;
        }
        public void setPosition(int offsetX) {
            position.X += offsetX;
            tierUp.position.X += offsetX;
            buttons.position.X += offsetX;
            tierUp.offset += offsetX;
        }
        public void isClicking(MouseState mouse, KeyboardState kb)
        {
            if (mouse.X > 350  && mouse.Y > position.Y + 40 && mouse.X < 1240 && viewType != ViewType.tierUp)
            {
                int index = (mouse.X - 350) / 128;
                
                if (index < (int)era + 4)
                {
                    box.text = infoTexts[index];
                    box.position.X = ((mouse.X - 350) / 128) * 128 + 275 + offset;
                }
                else
                {
                    box.position.X = -250;
                }
                if (viewType == ViewType.resource)
                {
                    if (index < (int)era + 1)
                    {
                        box.text = infoTexts[(mouse.X - 350) / 128 + 10];
                        box.position.X = ((mouse.X - 350) / 128) * 128 + 275 + offset;
                    }

                    else
                    {
                        box.position.X = -250;
                    }
                }

                drawSelection = true;
                dummyPos = new Rectangle(((mouse.X - 350) / 128) * 128 + 350 + offset, position.Y + 40, 128, 128);
                if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed)
                {
                    if (viewType == ViewType.attack)
                    {
                        spawnAttackSelector((mouse.X - 350) / 128);
                    }
                    if (viewType == ViewType.resource)
                    {
                        spawnResourceSelector((mouse.X - 350) / 128);
                    }
                }




            }
            else
            {
                drawSelection = false;


            }
            if (viewType == ViewType.tierUp)
            {
                tierUp.isClicking(mouse);
            }

            if (kb.IsKeyDown(Keys.D1) || kb.IsKeyDown(Keys.NumPad1))
            {
                viewType = ViewType.attack;
                buttons.current = BarButtons.Selected.Attack;
            }
            if (kb.IsKeyDown(Keys.D2) || kb.IsKeyDown(Keys.NumPad2))
            {
                viewType = ViewType.resource;
                buttons.current = BarButtons.Selected.Resource;
            }
            if (kb.IsKeyDown(Keys.D3) || kb.IsKeyDown(Keys.NumPad3))
            {
                viewType = ViewType.tierUp;
                buttons.current = BarButtons.Selected.Tier;
            }
            if (kb.IsKeyDown(world.ctrl.controls["Cancel Tower"]))
            {
                world.loader.spawner = null;
            }
            buttons.isClicking(mouse);
            viewType = (ViewType)(int)buttons.current;
            oldMouse = mouse;
        }

        public void spawnAttackSelector(int index)
        {
            if (index < (int)era + 4)
            {
                world.loader.LoadSpawner(index);
            }

        }
        public void TierUp()
        {

            if ((int)era == 0 && resources.resources[0] >= 100 && resources.resources[1] >= 300)
            {
                tierText = "INDUSTRIAL ERA";
                neededItemText = "  200 GOLD  500 STONE\n\n  300 IRON";
                resources.resources[0] -= 100;
                resources.resources[1] -= 300;
                era++;
                world.era++;
                world.prestigeMult = 2;
                world.tierTransCol.R = 0;
                world.callTierTransistion();
                world.sfx.PlaySoundQuietly("tierUp");
                return;
            }
            if ((int)era == 1 && resources.resources[0] >= 350 && resources.resources[1] >= 750 && resources.resources[2] >= 200)
            {
                tierText = "MAXED";
                neededItemText = "  MAXED";
                resources.resources[0] -= 350;
                resources.resources[1] -= 750;
                resources.resources[2] -= 200;
                era++;
                world.era++;
                world.prestigeMult = 4;
                world.tierTransCol.R = 0;
                world.callTierTransistion();
                world.sfx.PlaySoundQuietly("tierUp");
                return;
            }

        }
        public void spawnResourceSelector(int index)
        {
            if (index < (int)era + 1)
            {
                world.loader.LoadResourceSpawner(index);
            }

        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
            if (viewType == ViewType.attack)
            {

                for (int i = 0; i < (int)era + 4; i++)
                {
                    
                    if (i == 4)
                    {
                        spriteBatch.Draw(attackTowerTextures[i], new Rectangle(350  + offset + i * 128, position.Y + 40, 128, 128), Color.White);
                        continue;
                    }
                    spriteBatch.Draw(attackTowerTextures[i], new Rectangle(350 + offset + i * 128, position.Y + 40, 128, 140), Color.White);

                }

            }
            if (viewType == ViewType.resource)
            {

                for (int i = 0; i < (int)era + 1; i++)
                {

                    spriteBatch.Draw(resourceTowerTextures[i], new Rectangle(350 + offset + i * 128, position.Y + 40, 128, 140), Color.White);

                }


            }

            if (viewType == ViewType.tierUp)
            {
                tierUp.Draw(gameTime, spriteBatch);
                spriteBatch.DrawString(tierFont, "Upgrade To:", new Vector2(400 + offset, 870), Color.LightGray);
                spriteBatch.DrawString(tierFont, tierText, new Vector2(400 + offset, 900), Color.Gold);
                spriteBatch.DrawString(tierFont, neededItemText, new Vector2(800 + offset, 870), Color.LightGray);
            }
            buttons.Draw(gameTime, spriteBatch);
            if (drawSelection)
            {
                spriteBatch.Draw(dummyTexture, dummyPos, new Color(255, 255, 255, 120));
                box.Draw(gameTime, spriteBatch);
            }
        }
    }
}
