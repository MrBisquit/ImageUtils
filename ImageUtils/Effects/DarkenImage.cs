using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ImageUtils.Effects
{
    public static class DarkenImage
    {
        public static Bitmap Darken(Bitmap bm)
        {
            for (int y = 0; y < bm.Height; y++)
            {
                for (int x = 0; x < bm.Width; x++)
                {
                    Color c = bm.GetPixel(x, y);

                    //if(c.R >= (255 / 1.5) && c.G >= (255 / 1.5) && c.B >= (255 / 1.5))
                    if(c.GetBrightness() >= 50)
                    {
                        c = Color.FromArgb(c.R / 4, c.G / 4, c.B / 4);
                    } else
                    {
                        //MessageBox.Show(((c.R * 4) % 255).ToString());
                        c = Color.FromArgb((c.R * 2) % 255, (c.G * 2) % 255, (c.B * 2) % 255);
                    }

                    bm.SetPixel(x, y, c);
                }
            }

            return bm;
        }

        // Opposite of Darken
        public static Bitmap Brighten(Bitmap bm)
        {
            for (int y = 0; y < bm.Height; y++)
            {
                for (int x = 0; x < bm.Width; x++)
                {
                    Color c = bm.GetPixel(x, y);

                    //if(c.R >= (255 / 1.5) && c.G >= (255 / 1.5) && c.B >= (255 / 1.5))
                    if (c.GetBrightness() !>= 50)
                    {
                        c = Color.FromArgb(c.R / 4, c.G / 4, c.B / 4);
                    }
                    else
                    {
                        //MessageBox.Show(((c.R * 4) % 255).ToString());
                        c = Color.FromArgb((c.R * 2) % 255, (c.G * 2) % 255, (c.B * 2) % 255);
                    }

                    bm.SetPixel(x, y, c);
                }
            }

            return bm;
        }

        public static Bitmap MatteDarken(Bitmap bm)
        {
            for (int y = 0; y < bm.Height; y++)
            {
                for (int x = 0; x < bm.Width; x++)
                {
                    Color c = bm.GetPixel(x, y);

                    //if(c.R >= (255 / 1.5) && c.G >= (255 / 1.5) && c.B >= (255 / 1.5))
                    if (c.R == c.G && c.G == c.B && c.G == c.R)
                    {
                        c = Color.FromArgb(c.R / 4, c.G / 4, c.B / 4);
                    }
                    else
                    {
                        //MessageBox.Show(((c.R * 4) % 255).ToString());
                        c = Color.FromArgb((c.R * 4) / 255, (c.G * 4) / 255, (c.B * 4) / 255);
                    }

                    bm.SetPixel(x, y, c);
                }
            }

            return bm;
        }

        public static Bitmap ContrastChange(Bitmap bm)
        {
            for (int y = 0; y < bm.Height; y++)
            {
                for (int x = 0; x < bm.Width; x++)
                {
                    Color c = bm.GetPixel(x, y);

                    //if(c.R >= (255 / 1.5) && c.G >= (255 / 1.5) && c.B >= (255 / 1.5))
                    if (c.R == c.G && c.G == c.B && c.G == c.R)
                    {
                        c = Color.FromArgb(c.R / 4, c.G / 4, c.B / 4);
                    }
                    else
                    {
                        //MessageBox.Show(((c.R * 4) % 255).ToString());
                        c = Color.FromArgb((c.R * 2) % 255, (c.G * 2) % 255, (c.B * 2) % 255);
                    }

                    bm.SetPixel(x, y, c);
                }
            }

            return bm;
        }

        // Sheen - Type of shine
        public static Bitmap Sheen(Bitmap bm)
        {
            for (int y = 0; y < bm.Height; y++)
            {
                for (int x = 0; x < bm.Width; x++)
                {
                    Color c = bm.GetPixel(x, y);

                    //if(c.R >= (255 / 1.5) && c.G >= (255 / 1.5) && c.B >= (255 / 1.5))
                    if (c.R >= 128 || c.G >= 128 || c.B >= 128)
                    {
                        c = Color.FromArgb(c.R / 4, c.G / 4, c.B / 4);
                    }
                    else
                    {
                        //MessageBox.Show(((c.R * 4) % 255).ToString());
                        c = Color.FromArgb((c.R * 2) % 255, (c.G * 2) % 255, (c.B * 2) % 255);
                    }

                    bm.SetPixel(x, y, c);
                }
            }

            return bm;
        }

        public static Bitmap Idk(Bitmap bm)
        {
            for (int y = 0; y < bm.Height; y++)
            {
                for (int x = 0; x < bm.Width; x++)
                {
                    Color c = bm.GetPixel(x, y);

                    //if(c.R >= (255 / 1.5) && c.G >= (255 / 1.5) && c.B >= (255 / 1.5))
                    if (c.R >= 192 || c.G >= 192 || c.B >= 192)
                    {
                        c = Color.FromArgb(c.R / 4, c.G / 4, c.B / 4);
                    }
                    else
                    {
                        //MessageBox.Show(((c.R * 4) % 255).ToString());
                        c = Color.FromArgb((c.R * 2) % 255, (c.G * 2) % 255, (c.B * 2) % 255);
                    }

                    bm.SetPixel(x, y, c);
                }
            }

            return bm;
        }
    }
}
