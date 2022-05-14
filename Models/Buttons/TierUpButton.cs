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
    public class TierUpButton
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
        public ResourceTower resourceTower;
        public Boolean isEnabled;
        public FormatterMaster formatter = new FormatterMaster();
        public Boolean showInfoBox;
        public int offset; 
        public TierUpButton()
        {
            sources[0] = new Rectangle(0, 0, 680, 680);
            sources[1] = new Rectangle(0, 680, 680, 680);
            sources[2] = new Rectangle(0, 1360, 680, 680);

        }
        public void isClicking(MouseState mouse)
        {
            if (mouse.X + offset >= position.Left && mouse.X + offset<= position.Right && mouse.Y <= position.Bottom && mouse.Y >= position.Top && type != ViewType.Maxed)
            {
                type = ViewType.Hovered;
                if (tower != null)
                {
                    tower.isOverUpgrade = false;
                    //Console.WriteLine(tower.box.text);
                  
                    world.shownInfoBox = tower.box;
                }
                else
                {
                    world.shownInfoBox = resourceTower.box;
                }
                showInfoBox = true;




                if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed)
                {
                    if (tower != null)
                    {
                        tower.checkTier();
                    }
                    else
                    {
                        resourceTower.checkTier();
                    }
                }
            }
            else if (type != ViewType.Maxed)
            {
                type = ViewType.Default;
                if (tower != null && !tower.upgrade.showInfoBox)
                {
                    world.shownInfoBox = null;
                }
                else if (tower == null)
                {
                    world.shownInfoBox = null;
                }
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
