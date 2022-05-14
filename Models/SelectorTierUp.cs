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
    public class SelectorTierUp
    {
        public Rectangle position;
        public Texture2D texture;
        public Rectangle[] sources = new Rectangle[3];
        public World world;
        public ResourceBar bar;
        public int index;
        public Infobox box;
        public enum ViewType { Default, Hovered, Maxed };
        public ViewType type;
        public MouseState oldMouse = Mouse.GetState();
        public AttackSuper tower;
        public Boolean isEnabled;
        public FormatterMaster formatter = new FormatterMaster();
        public SelectionBar selector;
        public int offset;
        public SelectorTierUp()
        {
            sources[0] = new Rectangle(0, 0, 680, 680);
            sources[1] = new Rectangle(0, 680, 680, 680);
            sources[2] = new Rectangle(0, 1360, 680, 680);
            position = new Rectangle(700, 860, 80, 80);

        }
        public void isClicking(MouseState mouse)
        {
            if (mouse.X + offset >= position.Left && mouse.X + offset <= position.Right && mouse.Y <= position.Bottom && mouse.Y >= position.Top && type != ViewType.Maxed)
            {
                type = ViewType.Hovered;


                if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed)
                {
                    selector.TierUp();
                }
            }
            else if (type != ViewType.Maxed)
            {
                type = ViewType.Default;


            }

            oldMouse = mouse;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, sources[(int)type], Color.White);
        }
    }
}
