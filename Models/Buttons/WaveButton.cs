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
    public class WaveButton
    {
        public enum Selected { Default, Disabled, Selected, Pressed};
        public Selected current = Selected.Default;
        public Texture2D texture;
        public Rectangle[] sources = new Rectangle[4];
        public Rectangle position;
        public MouseState oldMouse = Mouse.GetState();
        public World world;
        public int offset;
        public Boolean permaDisabled;
        public WaveButton()
        {

            sources[0] = new Rectangle(0, 0, 376, 256);
            sources[1] = new Rectangle(0, 256, 376, 256);
            sources[2] = new Rectangle(0, 512, 376, 256);
            sources[3] = new Rectangle(0, 512, 376, 256);
        }
        public void isOverChoice(int mouseX, int mouseY, MouseState m)
        {
            //Console.WriteLine((int)current);
            if (mouseX + offset >= position.Left && mouseX + offset <= position.Right && mouseY >= position.Top && mouseY <= position.Bottom && current != Selected.Disabled)
            {
                current = Selected.Selected;
                if (m.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed)
                {
                    current = Selected.Pressed;
                    //if(world.currentWave == 1) {
                    //    world.isPreparingForBoss = true;
                   // }
                }
            }
            else
            {
                if (current != Selected.Disabled)
                {
                    current = Selected.Default;
                }
            }
            oldMouse = m;
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // TODO: Add your drawing code here
            spriteBatch.Draw(texture, position, sources[(int)current], Color.White);
        }
    }
}
