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
    /// Interaction logic for Rename.xaml
    /// </summary>
    public partial class Rename : Window
    {
        public Rename()
        {
            InitializeComponent();
        }

        public string ChangedText = "";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Utils.DisableWindowButtons.Disable(this);
        }

        public void Start(string CurrentText, string Label)
        {
            ChangedText = CurrentText;
            InputLabel.Content = Label;

            InputBox.Text = ChangedText;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RenameBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangedText = InputBox.Text;
            Close();
        }
    }
}
