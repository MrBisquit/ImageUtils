using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Interaction logic for PixelRemoveReplace.xaml
    /// </summary>
    public partial class PixelRemoveReplace : Window
    {
        public PixelRemoveReplace()
        {
            InitializeComponent();
        }

        Bitmap bm;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Utils.DisableWindowButtons.Disable(this);

            Width = 600;
            Height = 350;

            int x = (int)(Globals.mainWindow.Left + (Globals.mainWindow.Width / 2) - (Width / 2));
            int y = (int)(Globals.mainWindow.Top + (Globals.mainWindow.Height / 2) - (Height / 2));

            Left = x;
            Top = y;

            bm = Globals.mainWindow.bm;
            MainImage.Source = Globals.mainWindow.ToBitmapImage(bm);
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
