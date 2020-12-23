using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Lotolyzer_main_app
{
    /// <summary>
    /// A view model for the main menu
    /// </summary>
    class MainMenuViewModel : BaseViewModel
    {
        public ICommand CloseCommand { get; set; }
        public ICommand ShowDrawTableCommand { get; set; }

        public DataView CurrentDataView { get; set; }
        public DataTable CurrentDataTable { get; set; }

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainMenuViewModel()
        {
            this.CloseCommand = new RelayCommand(() => CloseApp(), true);
            this.ShowDrawTableCommand = new RelayCommand(() => ShowDrawTable(), true);
        }

        #endregion

        /// <summary>
        /// Closes the app correctly, shutting down the database
        /// </summary>
        private async void CloseApp()
        {
            // This needs to be changed

            await Task.Run(() =>
           {
               Task.Run(() =>
               {
                   MessageBox.Show("Closing, please be patient...");
               });

               Task.Run(() =>
               {
                   DatabaseControl.CloseConnection();
               });
           });

            await Task.Delay(500);

            Application.Current.MainWindow.Close();

        }

        /// <summary>
        /// Shows the draw table
        /// </summary>
        private void ShowDrawTable()
        {
            CurrentDataTable = DatabaseControl.GetDataTable("SELECT * FROM DrawTable");
            CurrentDataView = CurrentDataTable.DefaultView;

            DrawAnalysis.ParseArchiveDraw(DrawURL.BaseDrawArchiveURL + "1993");
        }
    }
}
