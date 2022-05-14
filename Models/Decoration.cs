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
using System.IO;

namespace Eragonia_Demo_Day_One
{
    public class Decoration
    {
        public Boolean isVisible = true;

        public Texture2D texture;
        public Rectangle position;
        public Rectangle source;
    }
}
