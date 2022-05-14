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
    public class CameraManager
    {
        public World world;
        public Camera camera;
        public int xRight = 0;
        public int xLeft = 0;
        public Boolean isMovingRight;
        public Boolean isMovingLeft;
        public int xOffset;
        public int totalOffset = 0;
        public void IsClicking(MouseState mouse, KeyboardState kb) {
            xOffset = 0;
            if(isMovingRight) {
                xOffset = 15;

                if (world.offsetX + 15 >= 1920)
                {


                    xOffset = 1920 - world.offsetX;
                    isMovingRight = false;
                    
                }
                
                
                
                
                world.offsetX += xOffset;
                updateUI();
                return;
            }
            else if(isMovingLeft) {
                xOffset = -15;
                if (world.offsetX - 15 <= 640)
                {

                    xOffset = 640 - world.offsetX;
                    isMovingLeft = false;
                   
                }
                

                
                
                world.offsetX += xOffset;
                updateUI();
                return;
            }
            if(mouse.X >= 1130 && mouse.Y >= 196 && mouse.Y < 800 && world.offsetX <= 1920) {
                xLeft = 0;
                if(xRight < 15) {
                    xRight++;
                }
                if (world.offsetX + xRight >= 1920)
                {
                    xRight = 1920 - world.offsetX;

                }
                xOffset = xRight;
                world.offsetX += xOffset;
            }
            else if(mouse.X <= 150 && mouse.Y >= 80 && world.offsetX >= 640) {
                xRight = 0;
                if (xLeft < 15)
                {
                    xLeft++;
                }
                if (world.offsetX - xLeft <= 640)
                {
                    xLeft =  world.offsetX - 640;

                }
                xOffset = -xLeft;

                world.offsetX += xOffset;
            }
            else if(kb.IsKeyDown(world.ctrl.controls["Move Right"]) && world.offsetX <= 1920) {
                xLeft = 0;
                if (xRight < 15)
                {
                    xRight++;
                }
                if(world.offsetX + xRight >= 1920) {
                    xRight = 1920 - world.offsetX;
                  
                }
                xOffset = xRight;
                world.offsetX += xOffset;
            }
            else if (kb.IsKeyDown(world.ctrl.controls["Move Left"]) && world.offsetX >=  640)
            {
                xRight = 0;
                if (xLeft < 15)
                {
                    xLeft++;
                }
                if (world.offsetX - xLeft <= 640)
                {
                    xLeft = world.offsetX - 640;

                }
                xOffset = -xLeft;
                world.offsetX += xOffset;
            }
            updateUI();
            //world.bar.offset += xOffset;
        }
    public void resetOffsets() {
            xOffset = -totalOffset;
            world.mainBar.setPosition(xOffset);
            world.waveButton.position.X += xOffset;
            world.waveButton.offset += xOffset;
            world.mainBar.offset += xOffset;
            world.mainBar.buttons.offset += xOffset;
            world.pauseinfobox.position.X += xOffset;
            world.pauseButton[0].offset += xOffset;
            world.pauseButton[0].rec.X += xOffset;
            world.pauseButton[1].offset += xOffset;
            world.pauseinfobox.offset += xOffset;
            world.pauseButton[1].rec.X += xOffset;
            world.healthBar.healthPos.X += xOffset;
            world.healthBar.barPos.X += xOffset;
            world.healthBar.textPos.X += xOffset;
            world.bar.position.X += xOffset;
            world.particles.offset += xOffset;
            world.loader.offset += xOffset;
            world.dialogue.changePos(xOffset);
            world.bossDialogue.changePos(xOffset);
            world.heart.position.X += xOffset;
            world.cutscene.setPosition(xOffset);
            world.transistioner.position.X += xOffset;
            world.infoManager.offset += xOffset;
            world.warning.offset += xOffset;
            world.pauseObj.setPosition(xOffset);
            if (world.shownInfo != null)
            {
                world.shownInfo.setPosition(xOffset);
            }
        }
    public void updateUI() {
            totalOffset += xOffset;
            world.mainBar.setPosition(xOffset);
            world.waveButton.position.X += xOffset;
            world.waveButton.offset += xOffset;
            world.mainBar.offset += xOffset;
            world.mainBar.buttons.offset += xOffset;
            world.pauseinfobox.position.X += xOffset;
            world.pauseButton[0].offset += xOffset;
            world.pauseButton[0].rec.X += xOffset;
            world.pauseButton[1].offset += xOffset;
            world.pauseinfobox.offset += xOffset;
            world.pauseButton[1].rec.X += xOffset;
            world.healthBar.healthPos.X += xOffset;
            world.healthBar.barPos.X += xOffset;
            world.healthBar.textPos.X += xOffset;
            world.bar.position.X += xOffset;
            world.particles.offset += xOffset;
            world.loader.offset += xOffset;
            world.dialogue.changePos(xOffset);
            world.bossDialogue.changePos(xOffset);
            world.heart.position.X += xOffset;
            world.cutscene.setPosition(xOffset);
            world.transistioner.position.X += xOffset;
            world.infoManager.offset += xOffset;
            world.warning.offset += xOffset;
            world.pauseObj.setPosition(xOffset);
            
            if(world.shownInfo != null) {
                world.shownInfo.setPosition(xOffset);
            }
        }
    }
}
