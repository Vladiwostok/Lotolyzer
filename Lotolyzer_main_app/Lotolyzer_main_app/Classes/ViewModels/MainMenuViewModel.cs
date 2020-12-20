using System;
using System.Windows;
using System.Windows.Input;

namespace Lotolyzer_main_app
{
    /// <summary>
    /// A view model for the main menu
    /// </summary>
    class MainMenuViewModel : BaseViewModel
    {
        public ICommand CloseCommand { get; set; }

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainMenuViewModel()
        {
            this.CloseCommand = new RelayCommand(() => CloseApp(), true); 
        }

        #endregion

        /// <summary>
        /// Closes the app correctly, shutting down the database
        /// </summary>
        private void CloseApp()
        {
            // This needs to be changed
            System.Threading.Tasks.Task.Run(() => MessageBox.Show("Closing...Please be patient"));
            System.Threading.Thread.Sleep(500);
            Application.Current.MainWindow.Close();
        }
    }
}
