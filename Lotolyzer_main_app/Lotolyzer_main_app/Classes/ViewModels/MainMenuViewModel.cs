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

        public ICommand ResetDatabaseCommand { get; set; }

        public ICommand ShowMainTableCommand { get; set; }
        public ICommand ShowDrawTableCommand { get; set; }
        public ICommand ShowNumberTableCommand { get; set; }

        public DataView CurrentDataView { get; set; }
        public DataTable CurrentDataTable { get; set; }

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainMenuViewModel()
        {
            this.CloseCommand = new RelayCommand(() => CloseApp(), true);

            this.ResetDatabaseCommand = new RelayCommand(() => ResetDatabase(), true);

            this.ShowMainTableCommand = new RelayCommand(() => ShowMainTable(), true);
            this.ShowDrawTableCommand = new RelayCommand(() => ShowDrawTable(), true);
            this.ShowNumberTableCommand = new RelayCommand(() => ShowNumberTable(), true);
        }

        #endregion

        // Change this
        /// <summary>
        /// Closes the app correctly, shutting down the database
        /// </summary>
        private async void CloseApp()
        {
           // await Task.Run(() =>
           //{
           //     DatabaseControl.CloseConnection();
           //});

            Application.Current.MainWindow.Close();

        }

        /// <summary>
        /// Shows the main table
        /// </summary>
        private void ShowMainTable()
        {
            Task.Run(() =>
           {
               CurrentDataTable = DatabaseControl.GetDataTable("SELECT * FROM MainTable ORDER BY Id DESC");
               DataTable cloned = CurrentDataTable.Clone();
               cloned.Columns[1].DataType = typeof(string);
               
               foreach(DataRow row in CurrentDataTable.Rows)
                   cloned.ImportRow(row);

               foreach (DataRow row in cloned.Rows)
                   row[1] = ((string)row[1]).Remove(((string)row[1]).IndexOf(' '));

               CurrentDataView = cloned.DefaultView;
           });
        }

        /// <summary>
        /// Shows the draw table
        /// </summary>
        private void ShowDrawTable()
        {
            Task.Run(() =>
            {
                CurrentDataTable = DatabaseControl.GetDataTable("SELECT * FROM DrawTable ORDER BY 'Draw Number' DESC");
                DataTable cloned = CurrentDataTable.Clone();
                cloned.Columns[1].DataType = typeof(string);

                foreach (DataRow row in CurrentDataTable.Rows)
                    cloned.ImportRow(row);

                foreach (DataRow row in cloned.Rows)
                    row[1] = ((string)row[1]).Remove(((string)row[1]).IndexOf(' '));

                CurrentDataView = cloned.DefaultView;
            });
        }

        /// <summary>
        /// Shows the number table
        /// </summary>
        private void ShowNumberTable()
        {
            Task.Run(() =>
            {
                CurrentDataTable = DatabaseControl.GetDataTable("SELECT * FROM NumberTable");
                CurrentDataView = CurrentDataTable.DefaultView;
            });
        }

        /// <summary>
        /// Resets the database
        /// </summary>
        private void ResetDatabase()
        {
            Task.Run(() =>
            {
                MainDatabaseTools.FullReset();
            });
        }
    }
}
