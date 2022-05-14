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
    public class Tile
    {
        public Boolean isPath = false;
        public Boolean isPlaceable = false;
        public Boolean towerPlaced = false;
        public Boolean hasDecoration = false;
        public Boolean isCorner = false;
        public Boolean isWater = false;
        public Boolean isMoat = false;
        public Boolean isWaterTop = false;
        public int upVelocity = -3;
        public Texture2D texture;
        public Rectangle position;
        public Rectangle[] sources;
        public Rectangle source;
        public Decoration decoration;
        public int rotation = 0;
        Random rand = new Random();
        public SpriteEffects flip;
        public Color color = Color.White;
        public Boolean goUp;
        public int timer = 0;
        public Boolean goDown;
        public Boolean hasGoneUp = false;
        public Texture2D secondTexture;
        public Texture2D dummyTexture;
        public World world;
        public Color dummyColor = new Color(255, 255, 255, 0);
        public Tile()
        {
            sources = new Rectangle[3];
            sources[0] = new Rectangle(0, 0, 1028, 1028);
            sources[1] = new Rectangle(1028, 0, 1028, 1028);
            sources[2] = new Rectangle(0, 1028, 1028, 1028);
            flip = SpriteEffects.None;
        }

        public void isBeingHovered(MouseState mouse) {
            if(isPlaceable == false) {
                return;
            }
            if(mouse.X + world.offsetX - 640 >= position.X - position.Width/2 && mouse.X + world.offsetX - 640<= position.X + position.Width/2) {
                if (mouse.Y >= position.Y - position.Height/2 && mouse.Y <= position.Y + position.Height/2)
                {
                    dummyColor.A = 120;
                    return;
                }
            }
            dummyColor.A = 0;
        }
        
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (goUp)
            {
                if(!hasGoneUp) {
                    timer = 0;
                    hasGoneUp = true;
                }
                
                position.Y += upVelocity;
                if (timer % 2 == 0)
                {
                    upVelocity++;
                }
                if (upVelocity == 0)
                {
                    texture = secondTexture;
                    flip = SpriteEffects.None;
                    if (!isCorner && !isPath)
                    {
                        rotation = 0;
                    }
                }
                if(upVelocity == 3 && timer % 2 != 0)
                {
                    goUp = false;
                }
            }
            spriteBatch.Draw(texture, position, source, color, MathHelper.ToRadians(rotation), new Vector2(source.Width / 2, source.Height / 2), flip, 0.0f);
            spriteBatch.Draw(dummyTexture, position, source, dummyColor, MathHelper.ToRadians(rotation), new Vector2(source.Width / 2, source.Height / 2), flip, 0.0f);
            if (hasDecoration)
            {

                spriteBatch.Draw(decoration.texture, decoration.position, decoration.source, Color.White, MathHelper.ToRadians(0),
                new Vector2(decoration.source.Width / 2, decoration.source.Height / 2), SpriteEffects.None, 0.0f);
            }
            timer++;
        }
        public void setSource(int a)
        {

            // Console.WriteLine(a);
            if (a < 0)
            {
                source = sources[1];
                hasDecoration = false;
            }
            else if (a < 30)
            {
                source = sources[2];
            }
            else
            {
                source = sources[0];
            }
            if (a % 3 == 0 && !isPath)
            {
                flip = SpriteEffects.FlipHorizontally;
            }
            else if (a % 3 == 1 && a >= 3 && !isPath)
            {
                flip = SpriteEffects.FlipVertically;
            }
            else
            {
                flip = SpriteEffects.None;
            }
        }

    }
}
