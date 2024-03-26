using System.Configuration;
using System.Data;
using System.Windows;

namespace ImageUtils
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow = new MainWindow();
            ImageUtils.Windows.Splash splash = new ImageUtils.Windows.Splash();
            splash.Show();
            MainWindow.Show();
            splash.Close();
        }
    }
}
