using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ImageUtils.Windows
{
    /// <summary>
    /// Interaction logic for Compression.xaml
    /// </summary>
    public partial class Compression : Window
    {
        public Compression()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Utils.DisableWindowButtons.Disable(this);

            Width = 600;

            int x = (int)(Globals.mainWindow.Left + (Globals.mainWindow.Width / 2) - (Width / 2));
            int y = (int)(Globals.mainWindow.Top + (Globals.mainWindow.Height / 2) - (Height / 2));

            Left = x;
            Top = y;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
