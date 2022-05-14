using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;

namespace Eragonia_Demo_Day_One
{
    public class TowerSelector
    {
        public Texture2D texture;
        public World world;
        public int towerType;
        int mouseX;
        int mouseY;
        MouseState oldMouse = Mouse.GetState();
        public int height = 70;
        public int offset = 0;
        public int offsetX;
        public Range range;
        public void setRange(int radius) {
            range = new Range();
            range.center = new Vector2(mouseX - 32, mouseY - 320);
            range.radius = radius;
            range.world = world;
            range.texture = world.Content.Load<Texture2D>("Towers/GUI/range");
            range.addPosition();
        }
        public void Update(MouseState mouse)
        {
            mouseX = (mouse.X + world.offsetX - 640) / 64 * 64;
            
            mouseY = mouse.Y / 64 * 64;
            range.center = new Vector2(mouseX + 32, mouseY + 26);
            range.addPosition();
            if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed)
            {
                if(mouse.X > 350 && mouse.Y > 796) {
                    return;
                }
                world.loader.placeTower(towerType, mouseX, mouseY);
            }
            else if(world.kb.IsKeyDown(world.ctrl.controls["Place Tower"])) {
                if (mouse.X > 350 && mouse.Y > 796)
                {
                    return;
                }
                
                world.loader.placeTower(towerType, mouseX, mouseY);
            }
            
            oldMouse = mouse;
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(texture, new Rectangle(mouseX, mouseY + offset, 64, height), Color.White);
            range.Draw(gameTime, spriteBatch);
        }
    }
}