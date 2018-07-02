using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace ControlDesigner.ViewModels
{
    public class MasterViewModel : BaseViewModel
    {
        public MasterViewModel()
        {
            ViewsList = new ObservableCollection<View>()
            {
                new Label(),
                new Button(),
                new Slider(),
                new Entry(),
                new Frame(),
                new Stepper(),
                new Xamarin.Forms.SearchBar(),
                new Xamarin.Forms.ProgressBar(),
                new Xamarin.Forms.Picker(),
                new Xamarin.Forms.Image(),
                new Xamarin.Forms.Editor(),
                new Xamarin.Forms.DatePicker(),
                new Xamarin.Forms.BoxView(),
            };

            


        }
        public IList<View>  ViewsList{ get; set; }
        private View _selectedVew;

        public View SelectedView
        {
            get { return _selectedVew; }
            set { _selectedVew = value; if (value == null) return; Navigation.GoControlDetailCommand.Execute(value); }
        }

    }
}
