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
    public class CutsceneManager
    {
        public Texture2D eyeTex;
        public Texture2D bossTex;
        public Texture2D fadeTex;
        public Texture2D dummyTexture;
        public Rectangle screenRect;
        public Rectangle fadeRect;
        public Color dummyColor;
        public Color eyeColor;
        public Boolean isTurningWhite;
        public Boolean isTurningBlack;
        public Boolean isFadingIn;
        public World world;
        public int timer = 0;
        public enum CutsceneState { Nothing, Eyes, Full};

        public CutsceneState state = CutsceneState.Nothing;
        public void Initialize() {
            screenRect = new Rectangle(0, 0, 1280, 960);
            fadeRect = new Rectangle(0, 720, 1280, 400);
            eyeColor = new Color(0, 0, 0, 0);
            dummyColor = new Color(255, 255, 255, 0);
            eyeTex = world.Content.Load<Texture2D>("Boss/bhayEye");
            fadeTex = world.Content.Load<Texture2D>("Boss/blackFade");
            bossTex = world.Content.Load<Texture2D>("Boss/bhaykerr");
            dummyTexture = world.dummyTexture;
        }
        public void setPosition(int offset) {
            fadeRect.X += offset;
            screenRect.X += offset;
        }
        public void Update() {
            if(isTurningWhite) {
                
                if (timer > 150)
                {
                    dummyColor.A += 25;
                    if (dummyColor.A >= 250)
                    {
                        isTurningWhite = false;
                        state = CutsceneState.Full;
                       
                        isTurningBlack = true;
                    }
                }
                else {
                    timer++;
                }
            }
            if(isTurningBlack) {
                dummyColor.A -= 25;
                if(dummyColor.A <= 0) {
                    isTurningBlack = false;
                    world.bossDialogue.shouldDraw = true;
                }
            }
            if(isFadingIn) {
                eyeColor.A += 10;
                eyeColor.R += 10;
                eyeColor.G += 10;
                eyeColor.B += 10;
                if (eyeColor.A >= 250) {
                    isFadingIn = false;
                }
            }
        }
        
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            if(state == CutsceneState.Eyes) {
                spriteBatch.Draw(dummyTexture, screenRect, Color.Black);
                spriteBatch.Draw(eyeTex, screenRect, eyeColor);
                spriteBatch.Draw(fadeTex, fadeRect, Color.White);
                
            }
            if(state == CutsceneState.Full) {
                spriteBatch.Draw(bossTex, screenRect, Color.White);
                spriteBatch.Draw(fadeTex, fadeRect, Color.White);
            }
            if(state == CutsceneState.Nothing) {
                spriteBatch.Draw(dummyTexture, screenRect, Color.Black);
            }
            spriteBatch.Draw(dummyTexture, screenRect, dummyColor);
        }



    }
}
