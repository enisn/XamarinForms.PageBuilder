using ControlDesigner.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace ControlDesigner.ViewModels
{
    public class LayoutSelectionViewModel : BaseViewModel
    {
        private IList<View> _layoutsList;
        private View _selectedLayout;

        public LayoutSelectionViewModel()
        {

            LayoutsList = LayoutsList != null ?
                new ObservableCollection<View>(LayoutsList) :
                new ObservableCollection<View>()
                {
                    new FlexLayout(),
                    new StackLayout(),
                    new Grid(),
                    new AbsoluteLayout(),
                    new RelativeLayout(),
                };

        }

        public IList<View> LayoutsList { get => _layoutsList; set { _layoutsList = value; OnPropertyChanged(); } }
        public View SelectedLayout { get => _selectedLayout; set { _selectedLayout = value; LayoutSelected(value); OnPropertyChanged(); } }
        public Command SaveCommand { get; set; }
        private void LayoutSelected(View value)
        {
            if (value == null) return;
            Navigation.PushPage(new PreviewPage(value));
        }
    }
}
