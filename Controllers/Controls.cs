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
    public class Controls
    {
        public World world;
        public Dictionary<String, Keys> controls;
        KeyboardState kb;
        Config config;
        public String controlkey;
        public Keys bind;
        public bool isBinding = false;
        public bool pressed = false;

        public Controls()
        {
            bind = Keys.BrowserBack;
            controlkey = "";
            controls = new Dictionary<string, Keys>();
            kb = Keyboard.GetState();
            controls.Add("Pause", Keys.P);
            controls.Add("Cancel Tower", Keys.Back);
            controls.Add("Toggle Tower", Keys.Tab);
            controls.Add("Place Tower", Keys.Enter);
            controls.Add("View Attack", Keys.D1);
            controls.Add("View Resource", Keys.D2);
            controls.Add("View Upgrade", Keys.D3);
            controls.Add("Move Left", Keys.Left);
            controls.Add("Move Right", Keys.Right);
            controls.Add("none", Keys.BrowserSearch);

        }


        public void Update(KeyboardState kb, KeyboardState oldKb)
        {
            if (kb.IsKeyDown(Keys.A))
            {
                this.bind = Keys.A;
            }
            if (kb.IsKeyDown(Keys.B))
            {
                this.bind = Keys.B;
            }
            if (kb.IsKeyDown(Keys.C))
            {
                this.bind = Keys.C;
            }
            if (kb.IsKeyDown(Keys.D))
            {
                this.bind = Keys.D;
            }
            if (kb.IsKeyDown(Keys.E))
            {
                this.bind = Keys.E;
            }
            if (kb.IsKeyDown(Keys.F))
            {
                this.bind = Keys.F;
            }
            if (kb.IsKeyDown(Keys.G))
            {
                this.bind = Keys.G;
            }
            if (kb.IsKeyDown(Keys.H))
            {
                this.bind = Keys.H;
            }
            if (kb.IsKeyDown(Keys.I))
            {
                this.bind = Keys.I;
            }
            if (kb.IsKeyDown(Keys.J))
            {
                this.bind = Keys.J;
            }
            if (kb.IsKeyDown(Keys.K))
            {
                this.bind = Keys.K;
            }
            if (kb.IsKeyDown(Keys.L))
            {
                this.bind = Keys.L;
            }
            if (kb.IsKeyDown(Keys.M))
            {
                this.bind = Keys.M;
            }
            if (kb.IsKeyDown(Keys.N))
            {
                this.bind = Keys.N;
            }
            if (kb.IsKeyDown(Keys.O))
            {
                this.bind = Keys.O;
            }
            if (kb.IsKeyDown(Keys.P))
            {
                this.bind = Keys.P;
            }
            if (kb.IsKeyDown(Keys.Q))
            {
                this.bind = Keys.Q;
            }
            if (kb.IsKeyDown(Keys.R))
            {
                this.bind = Keys.R;
            }
            if (kb.IsKeyDown(Keys.S))
            {
                this.bind = Keys.S;
            }
            if (kb.IsKeyDown(Keys.T))
            {
                this.bind = Keys.T;
            }
            if (kb.IsKeyDown(Keys.U))
            {
                this.bind = Keys.U;
            }
            if (kb.IsKeyDown(Keys.V))
            {
                this.bind = Keys.V;
            }
            if (kb.IsKeyDown(Keys.W))
            {
                this.bind = Keys.W;
            }
            if (kb.IsKeyDown(Keys.X))
            {
                this.bind = Keys.X;
            }
            if (kb.IsKeyDown(Keys.Y))
            {
                this.bind = Keys.Y;
            }
            if (kb.IsKeyDown(Keys.Z))
            {
                this.bind = Keys.Z;
            }
            if (kb.IsKeyDown(Keys.Back))
            {
                this.bind = Keys.Back;
            }
            if (kb.IsKeyDown(Keys.Enter))
            {
                this.bind = Keys.Enter;
            }
            if (kb.IsKeyDown(Keys.OemComma))
            {
                this.bind = Keys.OemComma;
            }
            if (kb.IsKeyDown(Keys.OemPeriod))
            {
                this.bind = Keys.OemPeriod;
            }
            if (kb.IsKeyDown(Keys.OemQuestion))
            {
                this.bind = Keys.OemQuestion;
            }
            if (kb.IsKeyDown(Keys.OemSemicolon))
            {
                this.bind = Keys.OemSemicolon;
            }
            if (kb.IsKeyDown(Keys.OemQuotes))
            {
                this.bind = Keys.OemQuotes;
            }
            if (kb.IsKeyDown(Keys.OemOpenBrackets))
            {
                this.bind = Keys.OemOpenBrackets;
            }
            if (kb.IsKeyDown(Keys.OemCloseBrackets))
            {
                this.bind = Keys.OemCloseBrackets;
            }
            if (kb.IsKeyDown(Keys.OemBackslash)) //UH OH!
            {
                this.bind = Keys.OemBackslash;
            }
            if (kb.IsKeyDown(Keys.OemMinus))
            {
                this.bind = Keys.OemMinus;
            }
            if (kb.IsKeyDown(Keys.OemPlus))
            {
                this.bind = Keys.OemPlus;
            }
            if (kb.IsKeyDown(Keys.Tab))
            {
                this.bind = Keys.Tab;
            }
            if (kb.IsKeyDown(Keys.LeftShift))
            {
                this.bind = Keys.LeftShift;
            }
            if (kb.IsKeyDown(Keys.RightShift))
            {
                this.bind = Keys.RightShift;
            }
            if (kb.IsKeyDown(Keys.OemTilde))
            {
                this.bind = Keys.OemTilde;
            }
            if (kb.IsKeyDown(Keys.LeftControl))
            {
                this.bind = Keys.LeftControl;
            }
            if (kb.IsKeyDown(Keys.RightControl))
            {
                this.bind = Keys.RightControl;
            }
            if (kb.IsKeyDown(Keys.LeftAlt))
            {
                this.bind = Keys.LeftAlt;
            }
            if (kb.IsKeyDown(Keys.RightAlt))
            {
                this.bind = Keys.RightAlt;
            }
            if (kb.IsKeyDown(Keys.Left))
            {
                this.bind = Keys.Left;
            }
            if (kb.IsKeyDown(Keys.Right))
            {
                this.bind = Keys.Right;
            }
            if (kb.IsKeyDown(Keys.Up))
            {
                this.bind = Keys.Up;
            }
            if (kb.IsKeyDown(Keys.Down))
            {
                this.bind = Keys.Down;
            }

            if (kb.IsKeyDown(Keys.D1))
            {
                this.bind = Keys.D1;
            }
            if (kb.IsKeyDown(Keys.D2))
            {
                this.bind = Keys.D2;
            }
            if (kb.IsKeyDown(Keys.D3))
            {
                this.bind = Keys.D3;
            }


            if (pressed == true)
            {



                if (controlkey == "Pause")
                {
                    controls["Pause"] = bind;
                    isBinding = true;
                }
                if (controlkey == "Cancel Tower")
                {

                    controls["Cancel Tower"] = bind;
                    isBinding = true;
                }
                if (controlkey == "Place Tower")
                {

                    controls["Place Tower"] = bind;
                    isBinding = true;
                }
                if (controlkey == "Move Left")
                {

                    controls["Move Left"] = bind;
                    isBinding = true;
                }
                if (controlkey == "Move Right")
                {

                    controls["Move Right"] = bind;
                    isBinding = true;
                }


            }
        }
    }
}
