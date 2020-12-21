using System;
using System.Data;
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
        public DataGrid CurrentDataGrid { get; set; }

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
        private void CloseApp()
        {
            // This needs to be changed
            System.Threading.Tasks.Task.Run(() => MessageBox.Show("Closing...Please be patient"));
            System.Threading.Thread.Sleep(500);
            Application.Current.MainWindow.Close();
        }

        /// <summary>
        /// Shows the draw table
        /// </summary>
        private void ShowDrawTable()
        {
            MessageBox.Show("merge");
            //CurrentDataView = DatabaseControl.GetDataTable("SELECT * FROM DrawTable ORDER BY [Numarul extragerii] DESC").DefaultView;
            CurrentDataTable = DatabaseControl.GetDataTable("SELECT * FROM DrawTable");
            CurrentDataView = CurrentDataTable.DefaultView;
            //CurrentDataGrid.DataContext = CurrentDataTable.DefaultView;
        }
    }
}
