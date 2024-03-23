using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageUtils.Effects
{
    /// <summary>
    /// Converter for turning a normal Bitmap into a Low-Colour bitmap with only 27 colours per pixel
    /// </summary>
    public static class LowColour
    {
        /// <summary>
        /// Originally ToLowColour
        /// </summary>
        /// <param name="bm"></param>
        /// <returns></returns>
        public static Bitmap DeepFry(Bitmap bm)
        {
            Bitmap _temp = bm;

            for (int y = 0; y < _temp.Height; y++)
            {
                for (int x = 0; x < _temp.Width; x++)
                {
                    Color c = _temp.GetPixel(x, y);
                    int R = c.R;
                    int G = c.G;
                    int B = c.B;

                    if (R >= 128) R = 255;
                    else R = 0;

                    if(G >= 128) G = 255;
                    else G = 0;

                    if(B >= 128) B = 255;
                    else B = 0;

                    _temp.SetPixel(x, y, Color.FromArgb(R, G, B));
                }
            }

            return _temp;
        }

        public static Bitmap ToLowColour(Bitmap bm)
        {
            Bitmap _temp = bm;

            for (int y = 0; y < _temp.Height; y++)
            {
                for (int x = 0; x < _temp.Width; x++)
                {
                    Color c = _temp.GetPixel(x, y);
                    int R = c.R;
                    int G = c.G;
                    int B = c.B;

                    // 64

                    if (R == 0) R = 0;
                    if (R <= 64) R = 64;
                    if (R <= 128) R = 128;
                    if (R <= 192) R = 192;
                    else R = 255;

                    if (G == 0) G = 0;
                    if (G <= 64) G = 64;
                    if (G <= 128) G = 128;
                    if (G <= 192) G = 192;
                    else G = 255;

                    if (B == 0) B = 0;
                    if (B <= 64) B = 64;
                    if (B <= 128) B = 128;
                    if (B <= 192) B = 192;
                    else B = 255;

                    _temp.SetPixel(x, y, Color.FromArgb(R, G, B));
                }
            }

            return _temp;
        }
    }
}
