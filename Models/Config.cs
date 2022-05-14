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
    public class Config
    {
        public Texture2D tex;
        public Rectangle rec;
        public Rectangle pos;
        public Color col;
        public MouseState mouse;
        public World world;
        public Boolean isFirstTime = true;
        KeyboardState keyb = Keyboard.GetState();

        public Button[] configButtons;


        public Config(Rectangle r, Rectangle p, Color c, Button[] b)
        {
            rec = r;
            pos = p;
            col = c;
            configButtons = b;

        }
        public void Center(String text, int buttonIndex)
        {
            SpriteFont font = configButtons[0].font;

            Vector2 temp = font.MeasureString(text);
            configButtons[buttonIndex].painNum = configButtons[buttonIndex].rec.X + configButtons[buttonIndex].rec.Width / 2 - (int)(temp.X * 1.5);
            if(buttonIndex == 3) {
                configButtons[buttonIndex].painNum = 288;
            }
        }
        public void ResetOtherButtons(int index)
        {
            for (int i = 0; i < 6; i++)
            {
                if (i == index)
                {
                    continue;
                }
                configButtons[i].col = Color.White;
            }
        }
        public void Update()
        {
            for (int i = 0; i < 6; i++)
            {
                configButtons[i].isOverChoice(mouse.X, mouse.Y, mouse, world.oldM);
                configButtons[i].whatButton();
            }
            if (configButtons[0].bp == Button.buttonPress.pressed) //pause
            {
                world.ctrl.controlkey = "Pause";

                world.configObj.configButtons[0].col = Color.Yellow;
                ResetOtherButtons(0);

                world.ctrl.pressed = true;

            }
            if (configButtons[1].bp == Button.buttonPress.pressed) //cancel tower
            {
                world.ctrl.controlkey = "Cancel Tower";
                world.configObj.configButtons[1].col = Color.Yellow;
                ResetOtherButtons(1);

                world.ctrl.pressed = true;
            }
            if (configButtons[2].bp == Button.buttonPress.pressed)// place tower
            {
                world.ctrl.controlkey = "Place Tower";
                world.configObj.configButtons[2].col = Color.Yellow;
                ResetOtherButtons(2);

                world.ctrl.pressed = true;
            }
            if (configButtons[3].bp == Button.buttonPress.pressed && world.optObj.optButtons[0].bp != Button.buttonPress.pressed)// return
            {
                world.gS = World.gameState.Options;
            }
            if (configButtons[4].bp == Button.buttonPress.pressed)
            {
                world.ctrl.controlkey = "Move Left";
                world.configObj.configButtons[4].col = Color.Yellow;
                ResetOtherButtons(4);

                world.ctrl.pressed = true;
            }
            if (configButtons[5].bp == Button.buttonPress.pressed)
            {
                world.ctrl.controlkey = "Move Right";
                world.configObj.configButtons[5].col = Color.Yellow;
                ResetOtherButtons(5);

                world.ctrl.pressed = true;
            }
            else
            {
                world.optObj.Update();
            }

            String currentButton = "Pause";
            for (int i = 0; i < 6; i++)
            {
                Center(configButtons[i].text, i);
                //String currentButton = "Pause";
                if (i == 1)
                {
                    currentButton = "Cancel Tower";
                }
                if (i == 2)
                {
                    currentButton = "Place Tower";
                }
                if (i == 3)
                {
                    currentButton = "none";
                }
                
                if (i == 4)
                {
                    currentButton = "Move Left";
                }
                if (i == 5)
                {
                    currentButton = "Move Right";
                }
                if ((configButtons[i].text != world.ctrl.controls[currentButton].ToString() && world.ctrl.isBinding == true) &&
                !world.ctrl.controls[currentButton].ToString().Equals("BrowserBack") && !currentButton.Equals("none"))
                {


                    if (world.ctrl.controls[currentButton] == Keys.OemComma)
                    {
                        configButtons[i].text = ",";
                    }
                    else if (world.ctrl.controls[currentButton] == Keys.OemPeriod)
                    {
                        configButtons[i].text = ".";
                    }
                    else if (world.ctrl.controls[currentButton] == Keys.OemQuestion)
                    {
                        configButtons[i].text = "/";
                    }
                    
                    //else if (world.ctrl.controls[currentButton] == Keys.BrowserSearch)
                    //{
                    //    configButtons[i].text = "Return";
                    //}

                    else
                    {
                        configButtons[i].text = world.ctrl.controls[currentButton].ToString();

                    }

                    Center(configButtons[i].text, i);
                    world.configObj.configButtons[i].col = Color.White;
                    world.ctrl.pressed = false;
                    world.ctrl.isBinding = false;
                    world.ctrl.bind = Keys.BrowserBack;

                }
            }


            //if (world.kb.IsKeyDown(Keys.A) && world.ctrl.pressed == true)
            //{
            //    world.ctrl.bind = Keys.A;
            //}
            //else if (world.kb.IsKeyDown(Keys.B) && world.ctrl.pressed == true)
            //{
            //    world.ctrl.bind = Keys.B;
            //}
            //else if (world.kb.IsKeyDown(Keys.P) && world.ctrl.pressed == true)
            //{
            //    world.ctrl.bind = Keys.P;
            //}

        }


        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            // TODO: Add your drawing code here

            //new Rectangle(140,70,1000,500)
            //every 50

            //sb.Draw(tex, rec, pos, col);
            for (int i = 0; i < 6; i++)
            {
                configButtons[i].Draw(gameTime, sb);
            }


        }
    }
}
