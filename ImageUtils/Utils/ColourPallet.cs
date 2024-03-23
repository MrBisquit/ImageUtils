using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageUtils.Utils
{
    public static class ColourPallet
    {
        public static Color[] GetColourPallet(Bitmap bm)
        {
            List<Color> colours = new List<Color>();

            for (int y = 0; y < bm.Height; y++)
            {
                for (int x = 0; x < bm.Width; x++)
                {
                    if (!colours.Contains(bm.GetPixel(x, y))) colours.Add(bm.GetPixel(x, y));
                }
            }

            return colours.ToArray();
        }
    }
}
