using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImageUtils.Project
{
    public static class LayerManager
    {
        public static List<Layer> Layers = new List<Layer>();

        public static Bitmap MergeLayers()
        {
            Bitmap bm = new Bitmap(Layers[0].Content.Width, Layers[0].Content.Height);

            Layers.OrderBy(layer => layer.LayerLevel).ToList();

            using (Graphics g = Graphics.FromImage(bm))
            {
                Layers.ForEach(layer =>
                {
                    if(layer.Visible)
                    {
                        g.DrawImage(layer.Content, new PointF(0, 0));
                    }
                });
            }

            return bm;
        }
    }

    public class Layer
    {
        public int LayerLevel { get; set; } = 0;
        public bool Visible { get; set; } = true;

        public Bitmap Content = new Bitmap(1, 1);
        public BitmapImage Preview { get; set; } = Globals.mainWindow.ToBitmapImage(new Bitmap(1, 1));
        public string Name { get; set; } = "Layer 0";
    }
}
