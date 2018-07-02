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
                (this.Content as Layout).ChildAdded += EditorPage_ChildAdded;
            }
        }
        private void EditorPage_ChildAdded(object sender, ElementEventArgs e)
        {
            (e.Element as View).GestureRecognizers.Clear();
            (e.Element as View).GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => OnTappedAControl(e.Element)),
            });
            
            if (e.Element is VisualElement)
                (e.Element as VisualElement).Focused += (s, args) => OnTappedAControl(s);
            if (e.Element is Button)
                (e.Element as Button).Clicked += (s, args) => OnTappedAControl(s);
        }
        void OnTappedAControl(object control)
        {
            NavigationViewModel.Instance.PushPropertiesPopup(control);
        }
    }
}
