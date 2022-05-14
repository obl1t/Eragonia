using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eragonia_Demo_Day_One
{
    public class StringFormatter
    {
        public String formatString(int length, String text)
        {
            String[] temp = text.Split(' ');
            int len = 0;
            String formattedString = "";
            for (int i = 0; i < temp.Length; i++)
            {

                len += temp[i].Length * 25;
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


    }
}
