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
    public class DialogueBox
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
        public List<String> dialogues = new List<String>();
        public List<String> bossDialogues = new List<String>();
        public World world;
        public Boolean shouldUpdate = false;
        public MouseState oldMouse = Mouse.GetState();
        public Boolean isBoss = false;
        public int offset;
        public void Initialize() {
            font = world.Content.Load<SpriteFont>("Other/Font2");
            dialogueTex = world.Content.Load<Texture2D>("GUI/dialogueBox");
            buttonTex = world.Content.Load<Texture2D>("GUI/dialogueButton");
            sources[0] = new Rectangle(0, 0, 256, 256);
            sources[1] = new Rectangle(0, 256, 256, 256);
            textPos = new Vector2(270, 30);
            dialoguePos = new Rectangle(240, 0, 800, 200);
            buttonPos = new Rectangle(940, 170, 64, 64);

            dialogues.Add("Welcome.");
            dialogues.Add("So you are the person tasked with defending our Kingdom.");
            dialogues.Add("...");
            dialogues.Add("I sense that you are inexperienced. ");
            dialogues.Add("Well then. I suppose it is my duty to introduce you to the way things work.");
            dialogues.Add("What you see on the bar below you are your attack towers. They are your sole means of attacking 'Them'");
            dialogues.Add("Here are your resource towers. Click that stone digger and place one down on the grass.");
            dialogues.Add("Or don't, I suppose.");
            dialogues.Add("You get gold from enemies, but the only way to get the other resources is through your resource towers.");
            dialogues.Add("Not placing a stone digger down by the start of the game is an easy, and frankly, embarrassing way to die.");
            dialogues.Add("And here is your tier up screen. You can advance your civilization here, assuming you have the resources.");
            dialogues.Add("Wooden arrows and clubs may suffice against the weaker Miscreants, but will soon get overwhelmed.");
            dialogues.Add("Don't let 'Them' cross that moat. That is your only true goal.");
            dialogues.Add("...");
            dialogues.Add("I have shown you the basics of your defense. I am certain you will figure out the rest.");
            dialogues.Add("Please do not let Eragonia down.");



        }
        public void changePos(int offsetX) {
            dialoguePos.X += offsetX;
            textPos.X += offsetX;
            offset += offsetX;
            buttonPos.X += offsetX;
        }
        public void IsClicking(MouseState mouse) {
            if (mouse.X + offset >= buttonPos.Left && mouse.X + offset <= buttonPos.Right && mouse.Y >= buttonPos.Top && mouse.Y <= buttonPos.Bottom) {
                currentIndex = 1;
                
            
                if(mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed) {
                    currentTextIndex++;
                    numLines = 1;
                    textIndex = 0;
                    shouldUpdate = true;
                    displayedText = "";
                    cooldown = 5;
                    timer = 0;
                    if(currentTextIndex >= dialogues.Count) {
                        world.mainBar.viewType = SelectionBar.ViewType.attack;
                        
                        world.mainBar.buttons.current = BarButtons.Selected.Attack;
                        world.gS = World.gameState.PlayGame;
                        return;
                    }
                    if(currentTextIndex == 6) {
                        
                        world.mainBar.viewType = SelectionBar.ViewType.resource;
                        world.mainBar.buttons.current = BarButtons.Selected.Resource;
                    }
                    if (currentTextIndex == 10)
                    {
                        world.mainBar.viewType = SelectionBar.ViewType.tierUp;
                        world.mainBar.buttons.current = BarButtons.Selected.Tier;
                    }
                    if(currentTextIndex == 12) {
                        world.cameraManager.isMovingRight = true;
                    }
                    if(currentTextIndex == 13) {
                        world.cameraManager.isMovingLeft = true;
                    }
                }
            }
            else {
                currentIndex = 0;
            }

            oldMouse = mouse;
            Update();
        }
        public void Update() {
            if(world.hasPlacedResourceTower[0]) {
                dialogues[7] = "Good.";
            }
            if (shouldUpdate)
            {
                
                String a = dialogues[currentTextIndex].Substring(textIndex, 1);
                //Console.WriteLine(a);
                
                if (timer == cooldown)
                {
                    displayedText += a;
                    world.sfx.PlaySound("blipHigh", 0.05f);
                    if (a.Equals(" ") && StartNewLine(displayedText))
                    {
                        displayedText += "\n";
                        numLines++;
                    }
                    timer = 0;
                    textIndex++;
                    //Console.WriteLine(textIndex);
                    if (textIndex >= dialogues[currentTextIndex].Length)
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
        public Boolean StartNewLine(String text) {
            if(font.MeasureString(text).X > 700 * numLines){
                return true;
            }
            return false;
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Draw(dialogueTex, dialoguePos, Color.White);
            spriteBatch.Draw(buttonTex, buttonPos, sources[currentIndex], Color.White);
            spriteBatch.DrawString(font, displayedText, textPos, Color.White);
        }
    }
}
