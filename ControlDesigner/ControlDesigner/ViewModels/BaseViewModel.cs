
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ControlDesigner.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public NavigationViewModel Navigation { get => NavigationViewModel.Instance; }
    }
}
