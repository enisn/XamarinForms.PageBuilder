using ControlDesigner.ViewModels;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace ControlDesigner.Pages
{
    public class EditorPage : ContentPage
    {
        public EditorPage()
        {

        }
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == "Content")
            {
                (this.Content as Layout).ChildAdded += PropertiesViewModel.OnChildrenAdded;
            }
        }
        
    }
}
