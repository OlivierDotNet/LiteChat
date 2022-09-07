using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteChat.Core.Helpers
{
    public static class LayoutHelper
    {
        /// <summary>
        /// 
        /// Creates a horizontal layout on the current row based on the <paramref name="maxWidth"/> of the application aswell as the <paramref name="spaceInPct"/> provided for each <see cref="SadConsole.ScreenObject"/>. Should be used by percentage, meaning two consoles with a split view would be 50, 50. Maximum value should be 100
        /// </summary>
        /// <param name="maxWidth">The maximum width of this layout</param>
        /// <param name="spaceInPct">How much space in % the <see cref="SadConsole.ScreenObject"/> should use</param>
        /// <returns>An array of ints containing the width of each <see cref="SadConsole.ScreenObject"/> to be used, sorted by order of arguements</returns>
        /// <exception cref="Exception"></exception>
        public static int[] CreateHorizontalLayout(int maxWidth, params int[] spaceInPct)
        {
            if(spaceInPct.Length == 0 || spaceInPct.Length < 1)
            {
                throw new Exception("Layout has to contain atleast two consoles");
            }

            int[] layout = new int[spaceInPct.Length];
            int widthUsed = 0;

            for (int i = 0; i < spaceInPct.Length; i++)
            {

                if (widthUsed == maxWidth)
                {
                    return layout;
                }

                float dividedValue = (float)spaceInPct[i] / 100f;
               
                layout[i] = (int)MathF.Round(maxWidth * dividedValue);
                widthUsed += layout[i];
                
                //if (spaceInPct[i] == spaceInPct.Last())
                //{
                //    layout[i] = maxWidth - widthUsed;
                //}
                //if (spaceInPct[i] > 50)
                //{
                //}
                //else if (spaceInPct[i] == 50)
                //{
                //    layout[i] = (maxWidth * 2);
                //}
                //else
                //{
                //    layout[i] = (maxWidth / dividedValue);
                //}
            }

            return layout;
        }
    }
}
