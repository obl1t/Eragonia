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
    public class FormatterMaster
    {
        public int offset;
        public String formatString(int length, String text)
        {
            String[] temp = text.Split(' ');
            int len = 0;
            String formattedString = "";
            for (int i = 0; i < temp.Length; i++)
            {

                len += temp[i].Length * 25;
                len += 25;
                if (len >= length)
                {
                    formattedString += "\n";
                    len = 0;
                }
                formattedString += temp[i];
                formattedString += " ";
            }
            return formattedString;
        }
        public String formatString(int length, String text, int spaceLength)
        {
            String[] temp = text.Split(' ');
            int len = 0;
            String formattedString = "";
            for (int i = 0; i < temp.Length; i++)
            {

                len += temp[i].Length * 25;
                len += spaceLength;
                if (len >= length)
                {
                    formattedString += "\n";
                    len = 0;
                }
                formattedString += temp[i];
                formattedString += " ";
            }
            return formattedString;
        }
        public Rectangle formatRect(Rectangle pos)
        {
            Rectangle rect = pos;
            if (rect.X < 0 + offset - 640)
            {
                rect.X = 0 + offset - 640;
            }
            if (rect.X > 1280 - rect.Width + offset)
            {
                rect.X = 1280 - rect.Width + offset;
            }
            if (rect.Y < 0)
            {
                rect.Y = 0;
            }
            if (rect.Y > 960 - rect.Height)
            {
                rect.Y = 960 - rect.Height;
            }
            return rect;
        }


    }
}
