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
    public class BossPieces
    {
        public Texture2D texture;
        public Rectangle leftPiece;
        public Rectangle rightPiece;
        public Rectangle leftPos;
        public Rectangle rightPos;
        public int downMovement;
        public int timer;
        public int leftRot;
        public int rightRot;
        public Color color = Color.Red;
        public World world;
        public int offset;
        public Boolean shouldTransistion;
        public void Initialize() {
            texture = world.Content.Load<Texture2D>("Boss/GUI/bossPieces");
            leftPiece = new Rectangle(0, 350, 330, 350);
            rightPiece = new Rectangle(0, 0, 330, 350);
            shouldTransistion = false;
            rightPos.X -= world.offsetX - 640;
            leftPos.X -= world.offsetX - 640;
           
        }
        public void Update() {
            if(timer % 2 != 0) {
                timer++;
                return;
            }
            if(timer == 250) {
               
                world.music.TransistionSong(4, true);
                
            }
            if(timer >= 400) {
                shouldTransistion = true;
            }
            leftPos.X -= 2;
            leftRot--;
            rightRot++;
            rightPos.X += 2;
            leftPos.Y += downMovement;
            rightPos.Y += downMovement;
            downMovement++;
            timer++;
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, leftPos, leftPiece, color, MathHelper.ToRadians(leftRot), new Vector2(leftPiece.Width / 2, leftPiece.Height / 2), SpriteEffects.None, 0.0f);
            spriteBatch.Draw(texture, rightPos, rightPiece, color, MathHelper.ToRadians(rightRot), new Vector2(leftPiece.Width / 2, leftPiece.Height / 2), SpriteEffects.None, 0.0f);

        }
    }
}
