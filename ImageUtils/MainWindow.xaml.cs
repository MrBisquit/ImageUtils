using Ookii.Dialogs.Wpf;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageUtils
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Globals.mainWindow = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Events.LayerUpdate += (object? sender, EventArgs e) =>
            {
                UpdateImage();
            };
        }

        public Bitmap bm;
        public Windows.Layers LayersWindow = null;

        private void ImportImage_Click(object sender, RoutedEventArgs e)
        {
            VistaOpenFileDialog ofd = new VistaOpenFileDialog()
            {
                Title = "ImageUtils - Import an image",
                Filter = "All Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.tif;*.tiff"
            };

            if((bool)ofd.ShowDialog())
            {
                bm = new Bitmap(System.Drawing.Image.FromFile(ofd.FileName));
                //UpdateImage();

                Project.ProjectManager.CreateTempProject();
                Project.LayerManager.Layers.Add(new Project.Layer() { LayerLevel = 0, Content = bm, Visible = true, Name = "Top Layer" });
                Project.LayerManager.Layers.Add(new Project.Layer() { LayerLevel = 1, Content = new Bitmap(bm.Width, bm.Height), Visible = true, Name = "Bottom Layer" });

                if (LayersWindow == null)
                {
                    LayersWindow = new Windows.Layers();
                    LayersWindow.Show();
                }

                Events.RaiseLayerUpdate(EventArgs.Empty);
            }
        }

        public void UpdateImage()
        {
            MainImage.Source = ToBitmapImage(Project.LayerManager.MergeLayers());
            //MainImage.Source = ToBitmapImage(bm);
        }

        // https://stackoverflow.com/a/23831231/16426057
        public BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }

        private void LowColourEffect_Click(object sender, RoutedEventArgs e)
        {
            bm = Effects.LowColour.ToLowColour(bm);
            UpdateImage();
        }

        private void LowColourEffectDeepFry_Click(object sender, RoutedEventArgs e)
        {
            bm = Effects.LowColour.DeepFry(bm);
            UpdateImage();
        }

        private void CompressionTools_Utils_Click(object sender, RoutedEventArgs e)
        {
            Windows.Compression compression = new Windows.Compression();
            compression.ShowDialog();
        }

        private void ResizeWindowMatchImageSizeTools_Utils_Click(object sender, RoutedEventArgs e)
        {
            Width = bm.Width - 5;
            Height = bm.Height + 41;
        }

        private void Remove_ReplacePixelsTools_Utils_Click(object sender, RoutedEventArgs e)
        {
            Windows.PixelRemoveReplace pixelRemoveReplace = new Windows.PixelRemoveReplace();
            pixelRemoveReplace.ShowDialog();
        }

        private void ImageStatsView_Click(object sender, RoutedEventArgs e)
        {
            Windows.ImageStatistics imageStats = new Windows.ImageStatistics();
            imageStats.Show(); // ShowDialog();
        }

        private void BrightnessDarkenEffects_Click(object sender, RoutedEventArgs e)
        {
            bm = Effects.DarkenImage.Darken(bm);
            UpdateImage();
        }

        private void BrightnessMatteDarkenEffects_Click(object sender, RoutedEventArgs e)
        {
            bm = Effects.DarkenImage.MatteDarken(bm);
            UpdateImage();
        }

        private void BrightnessSheenDarkenEffects_Click(object sender, RoutedEventArgs e)
        {
            bm = Effects.DarkenImage.Sheen(bm);
            UpdateImage();
        }

        private void BrightnessBrightenEffects_Click(object sender, RoutedEventArgs e)
        {
            bm = Effects.DarkenImage.Brighten(bm);
            UpdateImage();
        }

        private void SplitLayersLayers_Click(object sender, RoutedEventArgs e)
        {
            Windows.LayerSplitter layerSplitter = new Windows.LayerSplitter();
            layerSplitter.ShowDialog();
        }
    }
}