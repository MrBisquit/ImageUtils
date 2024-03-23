using ImageUtils.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ImageUtils.Windows
{
    /// <summary>
    /// Interaction logic for ImageStatistics.xaml
    /// </summary>
    public partial class ImageStatistics : Window
    {
        public ImageStatistics()
        {
            InitializeComponent();
        }

        Bitmap bm;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DisableWindowButtons.Disable(this);

            Width = 700; // 600
            Height = 350;

            int x = (int)(Globals.mainWindow.Left + (Globals.mainWindow.Width / 2) - (Width / 2));
            int y = (int)(Globals.mainWindow.Top + (Globals.mainWindow.Height / 2) - (Height / 2));

            Left = x;
            Top = y;

            bm = new Bitmap(Globals.mainWindow.bm);

            StartLoadingStats();

            MainImage.Source = Globals.mainWindow.ToBitmapImage(new Bitmap(bm));
        }

        private void StartLoadingStats()
        {
            Pixels.Text = $"Total pixels: {bm.Width * bm.Height} ({bm.Width}x{bm.Height})";
            DPI.Text = $"Image DPI (Pixels Per Inch instead) (Vertical x Horizontal): {bm.VerticalResolution}x{bm.HorizontalResolution}";
            ColourDepth.Text = $"Image colour depth: Unknown"; // TODO
            // TODO: Implement most used colour
            Size.Text = $"Size: Unknown"; // TODO
            // TODO: Smallest compressed size

            using (MemoryStream ms = new MemoryStream())
            {
                bm.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

                Size.Text = $"Size: As Bitmap (BMP): {LengthUtils.BytesToFormatted(ms.Length)}";
            }

            // Loading the colour pallet
            /*System.Drawing.Color[] colourPallet = Utils.ColourPallet.GetColourPallet(bm); //bm.Palette.Entries;

            ImageColourPalletTitle.Text = $"Image colour pallet (RGB) ({colourPallet.Length}):";

            foreach (var colour in colourPallet)
            {
                Bitmap _bm = new Bitmap(1, 1);
                _bm.SetPixel(0, 0, colour);

                WrapPanel wrapPanel = new WrapPanel();

                BitmapImage _bmi = Globals.mainWindow.ToBitmapImage(_bm);
                System.Windows.Controls.Image _img = new System.Windows.Controls.Image();
                _img.Source = _bmi;
                _img.Width = 15;
                _img.Height = 15;

                wrapPanel.Children.Add(_img);

                TextBlock _tb = new TextBlock();
                _tb.Text = $"R: {colour.R} G: {colour.G} B: {colour.B}";
                _tb.Width = 125;

                wrapPanel.Children.Add(_tb);

                ColourPallet.Children.Add(wrapPanel);
            }

            ColourPallet.UpdateLayout();*/

            ImageColourPalletTitle.Text = $"Image colour pallet (RGB): Loading... (This can take a long time)";

            GenerateColourPallet();
        }

        private void GenerateColourPallet()
        {
            Task.Factory.StartNew(async () =>
            {
                List<System.Drawing.Color> colourPallet = new List<System.Drawing.Color>();
                Dictionary<System.Drawing.Color, int> colourNumbers = new Dictionary<System.Drawing.Color, int>();

                for (int y = 0; y < bm.Height; y++)
                {
                    Dispatcher.Invoke(() =>
                    {
                        //int Percentage = y / bm.Height * 100;
                        //ImageColourPalletTitle.Text = $"Image colour pallet (RGB): Loading... (This can take a long time)... {Percentage}%";
                        ImageColourPalletTitle.Text = $"Image colour pallet (RGB): Loading... (This can take a long time)... {y}/{bm.Height}";
                    });

                    for (int x = 0; x < bm.Width; x++)
                    {
                        /*Dispatcher.Invoke(() =>
                        {
                            ImageColourPalletTitle.Text = $"Image colour pallet (RGB) ({x}/{bm.Width} {y}/{bm.Height}): Loading... (This can take a long time)";
                        });*/

                        System.Drawing.Color c = bm.GetPixel(x, y);

                        if(!colourPallet.Contains(c))
                        {
                            colourPallet.Add(c);
                            colourNumbers[c] = 1;
                        } else
                        {
                            colourNumbers[c] += 1;
                        }
                    }

                    await Task.Delay(10);
                }

                foreach (var colour in colourPallet)
                {
                    Bitmap _bm = new Bitmap(1, 1);
                    _bm.SetPixel(0, 0, colour);

                    Dispatcher.Invoke(() =>
                    {
                        WrapPanel wrapPanel = new WrapPanel();

                        BitmapImage _bmi = Globals.mainWindow.ToBitmapImage(_bm);
                        System.Windows.Controls.Image _img = new System.Windows.Controls.Image();
                        _img.Source = _bmi;
                        _img.Width = 15;
                        _img.Height = 15;

                        wrapPanel.Children.Add(_img);

                        TextBlock _tb = new TextBlock();
                        _tb.Text = $"R: {colour.R} G: {colour.G} B: {colour.B}";
                        _tb.Width = 125;

                        wrapPanel.Children.Add(_tb);
                        
                        ColourPallet.Children.Add(wrapPanel);

                        //ImageColourPalletTitle.Text = $"Image colour pallet (RGB) ({colourPallet.Count}): Loading... (This can take a long time)";

                        //Task.Delay(10);
                    });
                }

                Dispatcher.Invoke(() =>
                {
                    ImageColourPalletTitle.Text = $"Image colour pallet (RGB) ({colourPallet.Count}):";
                });

                System.Drawing.Color mUC = System.Drawing.Color.White;
                int mCUT = 0;

                foreach (var colour in colourPallet)
                {
                    if(colourNumbers[colour] > mCUT)
                    {
                        mUC = colour;
                        mCUT = colourNumbers[colour];
                    }
                }

                Dispatcher.Invoke(() =>
                {
                    MostUsedColour.Text = $"Most used colour: R: {mUC.R} G: {mUC.G} B: {mUC.B} ({mUC.Name}), total times used in image: {mCUT}";

                    Bitmap _c = new Bitmap(1, 1);
                    _c.SetPixel(0, 0, mUC);

                    MostUsedColourI.Source = Globals.mainWindow.ToBitmapImage(_c);
                });
            });
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
