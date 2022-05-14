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
    public class Warning
    {
        public Texture2D warningSprite;
        public Boolean shouldDraw;
        public Boolean isShown;
        public int timer;
        public Rectangle position;
        public World world;
        public SoundEffectInstance instance;
        public int offset;
        public void Initialize() {
            warningSprite = world.Content.Load<Texture2D>("GUI/warning");
            position = new Rectangle(1220, 460, 40, 40);
        }
        public void Update() {
            position.X = 1220 + offset;
            if(timer % 30 == 0) {
               
                if (instance != null)
                {
                    instance.Stop();
                }
                world.sfx.PlaySound("beep", 0.2f);
                instance = world.sfx.currentSound;
            }
            if(timer % 15 == 0) {
                isShown = !isShown;
            }
            timer++;
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (shouldDraw && isShown)
            {
                spriteBatch.Draw(warningSprite, position, Color.White);
            }
        }
    }
}
