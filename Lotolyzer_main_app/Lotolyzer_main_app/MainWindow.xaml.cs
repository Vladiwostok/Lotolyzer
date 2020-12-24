using System;
using System.Windows;

namespace Lotolyzer_main_app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainMenuViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                MainDatabaseTools.InsertTables();
            });            
        }
    }
}
