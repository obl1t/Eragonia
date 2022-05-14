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
    public class BossDialogue
    {
        public Texture2D dialogueTex;
        public Texture2D buttonTex;
        public SpriteFont font;
        public Rectangle[] sources = new Rectangle[2];
        public int currentIndex = 0;
        public int currentTextIndex = -1;
        public Rectangle dialoguePos;
        public Rectangle buttonPos;
        public int timer = 0;
        public int cooldown = 5;
        public Vector2 textPos;
        public String displayedText = "...";
        public int textIndex = 0;
        public int numLines = 1;
        public int offset;
        public List<String> bossDialogues = new List<String>();
        public World world;
        public Boolean shouldUpdate = false;
        public Boolean shouldDraw = true;
        public MouseState oldMouse = Mouse.GetState();
        public Boolean isBoss = false;
        public void Initialize()
        {
            font = world.Content.Load<SpriteFont>("Other/Font2");
            dialogueTex = world.Content.Load<Texture2D>("GUI/dialogueBox");
            buttonTex = world.Content.Load<Texture2D>("GUI/dialogueButton");
            sources[0] = new Rectangle(0, 0, 256, 256);
            sources[1] = new Rectangle(0, 256, 256, 256);
            textPos = new Vector2(270, 30);
            dialoguePos = new Rectangle(240, 0, 800, 200);
            buttonPos = new Rectangle(940, 170, 64, 64);

            bossDialogues.Add("Pathethic, are they not?");
            bossDialogues.Add("I am ashamed to say they are my strongest creations.");
            bossDialogues.Add("Yet, they fall like wilting leaves to your soliders.");
            bossDialogues.Add("Though, I will admit.");
            bossDialogues.Add("They have taught me a valuable lesson.");
            bossDialogues.Add("From their failure, I have learned that, \nif you want something done...");
            bossDialogues.Add("...you have to do it yourself.");
            
        }
        public void changePos(int offsetX)
        {
            dialoguePos.X += offsetX;
            textPos.X += offsetX;
            offset += offsetX;
            buttonPos.X += offsetX;
        }
        public void IsClicking(MouseState mouse)
        {
            if (mouse.X + offset >= buttonPos.Left && mouse.X + offset <= buttonPos.Right && mouse.Y >= buttonPos.Top && mouse.Y <= buttonPos.Bottom && shouldDraw)
            {
                currentIndex = 1;


                if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed)
                {
                    currentTextIndex++;
                    numLines = 1;
                    textIndex = 0;
                    shouldUpdate = true;
                    displayedText = "";
                    cooldown = 5;
                    timer = 0;
                    if (currentTextIndex >= bossDialogues.Count)
                    {
                        world.transistioner.turnBlack = true;
                        world.bossTimer = 300;
                        world.music.doTransistion3 = true;
                        world.transistion = World.transistionState.Transistioning;
                        world.cameraManager.isMovingLeft = true;
                        world.transistioner.state = World.gameState.PlayGame;
                        world.isPreparingForBoss = false;
                        shouldDraw = false;
                        world.isPreparingForBoss = false;
                        return;
                    }
                    if(currentTextIndex == 4) {
                        world.transistioner.turnBlack = true;
                        world.music.doTransistion3 = true;
                        world.transistion = World.transistionState.Transistioning;
                        world.transistioner.state = World.gameState.Cutscene;
                    }
                    if (currentTextIndex == 5)
                    {
                        world.cutscene.state = CutsceneManager.CutsceneState.Eyes;
                        world.cutscene.isFadingIn = true;
                    }
                    if (currentTextIndex == 6)
                    {
                        shouldDraw = false;
                        world.cutscene.isTurningWhite = true;
                        world.music.PlaySong(2, false);
                        world.sfx.PlaySound("thunder");
                    }
                }
            }
            else
            {
                currentIndex = 0;
            }

            oldMouse = mouse;
            Update();
        }
        public void Update()
        {
            
            if (shouldUpdate && shouldDraw)
            {

                String a = bossDialogues[currentTextIndex].Substring(textIndex, 1);
                //Console.WriteLine(a);
                if(currentTextIndex == 8 && textIndex == 0 && timer == 0) {
                    world.sfx.PlaySound("thunder");
                    world.cutscene.timer = 5;
                    world.cutscene.isTurningWhite = true;
                }
                if (timer == cooldown)
                {
                    displayedText += a;
                    world.sfx.PlaySound("blipLow", 1f);
                    if (a.Equals(" ") && StartNewLine(displayedText))
                    {
                        displayedText += "\n";
                        numLines++;
                    }
                    timer = 0;
                    textIndex++;
                    //Console.WriteLine(textIndex);
                    if (textIndex >= bossDialogues[currentTextIndex].Length)
                    {

                        shouldUpdate = false;
                    }
                    if (a.Equals(","))
                    {
                        cooldown = 15;
                    }
                    else if (a.Equals(".") || a.Equals("!"))
                    {
                        cooldown = 20;
                    }
                    else
                    {
                        cooldown = 3;
                    }
                }

                timer++;
            }
        }
        public Boolean StartNewLine(String text)
        {
            if (font.MeasureString(text).X > 700 * numLines)
            {
                return true;
            }
            return false;
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(dialogueTex, dialoguePos, Color.White);
            spriteBatch.Draw(buttonTex, buttonPos, sources[currentIndex], Color.White);
            spriteBatch.DrawString(font, displayedText, textPos, Color.Red);
        }
    }
}
