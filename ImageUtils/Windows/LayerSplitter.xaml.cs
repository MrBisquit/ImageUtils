using Ookii.Dialogs.Wpf;
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
    /// Interaction logic for LayerSplitter.xaml
    /// </summary>
    public partial class LayerSplitter : Window
    {
        public LayerSplitter()
        {
            InitializeComponent();

            Width /= 1.25;
            Height /= 1.25;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Utils.DisableWindowButtons.Disable(this);

            LoadOptions();

            MainImage.Source = Utils.BitmapUtils.ToBitmapImage(Globals.mainWindow.bm);

            ApplyBtn.Focus();
        }

        private void LoadOptions()
        {

        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ApplyBtn_Click(object sender, RoutedEventArgs e)
        {
            TaskDialog td = new TaskDialog()
            {
                WindowTitle = "ImageUtils - Confirm Layer Splitting",
                MainIcon = TaskDialogIcon.Warning,
                AllowDialogCancellation = false,
                MainInstruction = "Are you sure you want to split the layers of this image?",
                Content = "This will clear all existing layers and split the originally loaded image saved in memory."
            };

            TaskDialogButton Yes = new TaskDialogButton(ButtonType.Yes);
            TaskDialogButton No = new TaskDialogButton(ButtonType.No);

            td.Buttons.Add(Yes);
            td.Buttons.Add(No);

            if(Selector.SelectedIndex == 3)
            {
                td.Content += $"\n\nNo value was selected so {'"'}Brightness{'"'} was chosen automatically";
            }

            TaskDialogButton result = td.ShowDialog();

            if(result == Yes)
            {
                if (Selector.SelectedIndex == 3)
                {
                    Hide();
                    Utils.LayerSplitter.ByBrightness();
                    Show();
                    Close();
                } else
                {
                    Hide();

                    switch(Selector.SelectedIndex)
                    {
                        case 0:
                            Utils.LayerSplitter.ByBrightness();
                            break;
                    }

                    Show();
                    Close();
                }
            }
        }

        private void Selector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadOptions();
        }
    }
}
