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
    public class Slider
    {
        public Texture2D barTex;
        public Texture2D dotTex;
        public Texture2D dummyTexture;
        public Rectangle barPos;
        public Rectangle dumbPos;
        public Rectangle dotPos;
        public int index = 0;
        public World world;
        public MouseState oldMouse = Mouse.GetState();
        public void Initialize()
        {
            dotPos = new Rectangle(barPos.X + 660, barPos.Y, 80, 80);
            dumbPos = new Rectangle(barPos.X + 4, barPos.Y + 24, barPos.Width - 8, barPos.Height - 48);
            barTex = world.Content.Load<Texture2D>("GUI/sliderBar");
            dotTex = world.Content.Load<Texture2D>("GUI/sliderDot");
            dummyTexture = world.dummyTexture;
        }

        public void isClicking(MouseState mouse)
        {
            if(mouse.X >= 0 && mouse.X <= barPos.Right && mouse.Y >= dotPos.Top && mouse.Y <= dotPos.Bottom)
            {
                if(mouse.LeftButton == ButtonState.Pressed)
                {
                    dotPos.X = mouse.X - 40;
                    if(dotPos.X < dumbPos.X - 40)
                    {
                        dotPos.X = dumbPos.X - 40;
                    }
                    dumbPos.Width = (dotPos.X + 10) - barPos.X;
                }
            }
            if (index == 0)
            {
                double a = (double)(dotPos.X + 40 - barPos.X) / (double)barPos.X;
                a *= a;
                world.maxVolume = (float)a;
                MediaPlayer.Volume = (float)a;
            }
            if (index == 1 && mouse.LeftButton == ButtonState.Released && oldMouse.LeftButton != ButtonState.Released)
            {
                double a = (double)(dotPos.X + 40 - barPos.X) / (double)barPos.X;
                a *= a;
                world.maxSFX = MathHelper.Clamp((float)a, 0, 1);
                world.sfx.PlaySound("click");
            }
            oldMouse = mouse;

        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(dummyTexture, dumbPos, Color.Cyan);
            spriteBatch.Draw(barTex, barPos, Color.White);
            spriteBatch.Draw(dotTex, dotPos, Color.White);
        }
    }
}
