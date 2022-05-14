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
    public class UpgradeButton
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
        public Boolean showInfoBox;
        public int offset;
        public UpgradeButton()
        {
            sources[0] = new Rectangle(0, 0, 680, 680);
            sources[1] = new Rectangle(0, 680, 680, 680);
            sources[2] = new Rectangle(0, 1360, 680, 680);

        }
        public void isClicking(MouseState mouse)
        {
            if (mouse.X + offset >= position.Left && mouse.X  + offset <= position.Right && mouse.Y <= position.Bottom && mouse.Y >= position.Top && type != ViewType.Maxed)
            {
                tower.isOverUpgrade = true;
                type = ViewType.Hovered;
                showInfoBox = true;

                world.shownInfoBox = tower.box;


                if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed)
                {
                    tower.checkUpgrade();
                }
            }
            else if (type != ViewType.Maxed)
            {
                type = ViewType.Default;
                world.shownInfoBox = null;
                showInfoBox = false;
            }
            else
            {
                world.shownInfoBox = null;
                showInfoBox = false;
            }

            oldMouse = mouse;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, sources[(int)type], Color.White);

        }
    }
}
