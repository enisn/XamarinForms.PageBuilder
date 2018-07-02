using ControlDesigner.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace ControlDesigner.ViewModels
{
    public class ControlDetailViewModel : BaseViewModel
    {
        private View _control;

        public ControlDetailViewModel()
        {
            InitCommands();
        }
        public ControlDetailViewModel(object control) : this()
        {
            if (control is View)
            {
                this.Control = control as View;
            }


        }
        void InitCommands()
        {
            EditPropertiesCommand = new Command(EditProperties);
        }



        public Command EditPropertiesCommand { get; set; }
        public View Control { get => _control; set { _control = value; OnPropertyChanged(); } }

        private void EditProperties(object obj)
        {
            Navigation.PushPropertiesPopup(obj);
        }
    }
}
