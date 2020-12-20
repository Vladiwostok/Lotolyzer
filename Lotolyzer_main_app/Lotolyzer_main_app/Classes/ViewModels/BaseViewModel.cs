using System.ComponentModel;

namespace Lotolyzer_main_app
{
    /// <summary>
    /// The base view model that fires PropertyChanged event as needed
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The event that is fired when any child property changes its value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
