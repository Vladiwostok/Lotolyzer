using System;
using System.Windows.Input;

namespace Lotolyzer_main_app
{
    /// <summary>
    /// Basic command that runs an action
    /// </summary>
    class RelayCommand : ICommand
    {
        #region Private members
        /// <summary>
        /// The action to run
        /// </summary>
        private Action _action;

        /// <summary>
        /// Indicates if the command can be executed
        /// </summary>
        private bool _canExecute;

        #endregion

        #region Public events

        /// <summary>
        /// The event that is fired when the <see cref="CanExecute(object)"/>
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="action"></param>
        public RelayCommand(Action action, bool canExecute)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            _action = action;
            _canExecute = canExecute;
        }

        #endregion

        #region Command methods

        /// <summary>
        /// A relay command can always execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        /// <summary>
        /// Execute the commands action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            _action();
        }

        #endregion
    }
}
