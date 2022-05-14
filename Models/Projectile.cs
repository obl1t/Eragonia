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
    class Projectile
    {
        public Texture2D texture;
        public int velocity;
        public int decay;
        public Vector2 positionVector;
        public Rectangle position;
        public int damage;
        public int rotation;
        public Vector2 origin;
        public int offset;
        public Rectangle source;
        public Boolean shouldDelete = false;
        public int timer = 0;
        public void Update() {
            positionVector.X += (float) Math.Cos(MathHelper.ToRadians(rotation)) * velocity;
            positionVector.Y += (float) Math.Sin(MathHelper.ToRadians(rotation)) * velocity;
            position.X = (int)positionVector.X;
            position.Y = (int)positionVector.Y;
            if (position.X > 1280 + offset || position.X < -5|| position.Y > 960 || position.Y < -5) {
                shouldDelete = true;
            }
            timer++;
            if(timer > decay) {
                shouldDelete = true;
            }
            
        }
        public void setOrigin() {
            source = new Rectangle(0, 0, texture.Width, texture.Height);
            positionVector.X = position.X;
            positionVector.Y = position.Y;
            origin.X = source.Width / 2;
            origin.Y = source.Height / 2;
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, source, Color.White, MathHelper.ToRadians(rotation + 90), new Vector2(source.Width / 2, source.Height / 2), SpriteEffects.None, 0.0f);
        }
    }
}
