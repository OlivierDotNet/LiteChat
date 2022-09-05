using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteChat.Core.Helpers
{
    public static class LayoutHelper
    {
        public static int[] CreateLayout(int maxWidth, params int[] spaceInPct)
        {
            if(spaceInPct.Length == 0 || spaceInPct.Length < 1)
            {
                throw new Exception("Layout has to contain atleast two consoles");
            }

            int[] layout = new int[spaceInPct.Length];
            int widthUsed = maxWidth;

            for (int i = 0; i < spaceInPct.Length; i++)
            {
                if (widthUsed <= 0)
                {
                    if (spaceInPct[i] == spaceInPct.Last())
                    {
                        layout[i] = maxWidth - widthUsed;
                    }
                }

                int dividedValue = spaceInPct[i] / 10;

                if (spaceInPct[i] > 50)
                {
                    layout[i] = (maxWidth / 2) + (maxWidth / dividedValue);
                }
                else if (spaceInPct[i] == 50)
                {
                    layout[i] = (maxWidth / 2);
                }
                else
                {
                    layout[i] = (maxWidth / dividedValue);
                }
                widthUsed -= layout[i];
            }

            return layout;
        }
    }
}
