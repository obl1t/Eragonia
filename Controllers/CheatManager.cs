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
    public class CheatManager
    {
        public List<Keys> konami = new List<Keys>();
        public List<Keys> boss = new List<Keys>();
        public List<Keys> currentInputs = new List<Keys>();
        public List<Keys> bossInputs = new List<Keys>();
        public World world;
        public Boolean hasCheated = false;
        public Boolean hasCheated2 = false;
        public CheatManager() {
            konami.Add(Keys.Up);
            konami.Add(Keys.Up);
            konami.Add(Keys.Down);
            konami.Add(Keys.Down);
            konami.Add(Keys.Left);
            
            konami.Add(Keys.Right);
            konami.Add(Keys.Left);
            konami.Add(Keys.Right);
            konami.Add(Keys.B);
            konami.Add(Keys.A);

            boss.Add(Keys.B);
            boss.Add(Keys.A);
            boss.Add(Keys.K);
            boss.Add(Keys.E);
            boss.Add(Keys.R);
        }
        public void Update(KeyboardState kb, KeyboardState oldKb) {
            
            if(kb.GetPressedKeys().Length > 0 && currentInputs.Count < 10) {
                if (konami[currentInputs.Count] == kb.GetPressedKeys()[0]) {
                    //Console.WriteLine("HMM?");
                    currentInputs.Add(kb.GetPressedKeys()[0]);
                }
                
            }
            if (kb.GetPressedKeys().Length > 0 && bossInputs.Count < 5)
            {
                
                if (boss[bossInputs.Count] == kb.GetPressedKeys()[0])
                {
                    //Console.WriteLine("HMM?");
                    bossInputs.Add(kb.GetPressedKeys()[0]);
                   
                }

            }
            
            if (currentInputs.Count > 10) {
                currentInputs.RemoveAt(0);
            }
            if(bossInputs.Count > 5)
            {
                bossInputs.RemoveAt(0);
            }
            if(konami.SequenceEqual(currentInputs)) {
                if (!hasCheated)
                {
                    world.Cheat();
                    hasCheated = true;
                }
            }
            if (boss.SequenceEqual(bossInputs))
            {
                if (!hasCheated2)
                {
                    world.Cheat2();
                    hasCheated2 = true;
                }
              
                
            }
        }
    }
}
