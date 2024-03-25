using System;
using System.Collections.Generic;
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
    /// Interaction logic for ProgressWindow.xaml
    /// </summary>
    public partial class ProgressWindow : Window
    {
        public ProgressWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Utils.DisableWindowButtons.Disable(this);
        }

        public void Update(bool IsIntermediate, string? T = null, string? C = null)
        {
            PB.IsIndeterminate = IsIntermediate;

            if(T != null)
            {
                Title.Text = T;
            }

            if(C != null)
            {
                Current.Text = C;
            }
        }

        public void Update(int p = 0, int t = 100, string? T = null, string? C = null)
        {
            PB.Value = p;
            PB.Maximum = t;

            double pd = (double)p;
            double td = (double)t;

            double percentage = (pd / td) * 100;

            PBL.Text = $"{Math.Round(percentage, 2)}%";

            if(T != null)
            {
                Title.Text = T;
            }

            if(C != null)
            {
                Current.Text = C;
            }
        }
    }
}
