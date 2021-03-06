﻿using ControlDesigner.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ControlDesigner.ViewModels
{
    public class NavigationViewModel
    {
        public static NavigationViewModel Instance = new NavigationViewModel();
        public static bool ShemeIsValid => Application.Current.MainPage is MasterDetailPage && (Application.Current.MainPage as MasterDetailPage).Detail is NavigationPage;
        public static NavigationPage NavPage => ShemeIsValid ? (Application.Current.MainPage as MasterDetailPage).Detail as NavigationPage : null;
        public static MasterDetailPage MasPage => Application.Current.MainPage is MasterDetailPage ? Application.Current.MainPage as MasterDetailPage : null;
        public Command GoControlDetailCommand { get; set; }
        public Command PushPropertiesPopupCommand { get; private set; }
        public Command OpenUriCommand { get; set; }
        public Command GoAboutCommand { get; set; }

        public Task DisplayAlert(string title, string message, string cancel)
        {
            return Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }
        public Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }
        public NavigationViewModel()
        {
            PushPopupCommand = new Command(PushPopup);
            PopPopupCommand = new Command(PopPopup);

            GoControlDetailCommand = new Command(GoControlDetail);
            PushPropertiesPopupCommand = new Command(PushPropertiesPopup);
            OpenUriCommand = new Command(OpenUri);
            GoAboutCommand = new Command(GoAbout);
        }

        private void GoAbout(object obj)
        {
            PushPopup(new Frame { BackgroundColor = Color.White, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, Margin = 25,
                Content = new StackLayout
                {
                    Children =
                    {
                        new Label { Text ="Developed by Enis Necipoğlu" , FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center, TextColor = Color.FromHex("#212121") },
                        new Button { Text = "CLOSE", BackgroundColor = Color.Transparent, Command = new Command(()=> PopPopup() ) }
                    }
                }
            });
        }

        private void OpenUri(object obj)
        {
            try
            {
                Device.OpenUri(new Uri(obj.ToString()));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        public void PushPropertiesPopup(object obj)
        {
            PushPopup(new PropertiesView(obj));
        }

        public void GoControlDetail(object obj)
        {
            PushPage(new PreviewPage(obj));
        }

        public void SetDetail(Page page)
        {
            MasPage.Detail = page;
        }
        public void PushPage(Page page)
        {
            NavPage.PushAsync(page);
        }
        public void PopPage()
        {
            NavPage.PopAsync();
        }
        public void PopToRoot()
        {
            NavPage.PopToRootAsync();
        }

        #region PopupService
        public Command PushPopupCommand { get; set; }
        public Command PopPopupCommand { get; set; }
        public void PushPopup(View view)
        {
            PopupNavigation.PushAsync(new Rg.Plugins.Popup.Pages.PopupPage { Content = view });
        }
        public void PushPopup(object obj)
        {
            if (obj is View)
                PushPopup(obj);
        }
        public void PopPopup()
        {
            PopupNavigation.PopAsync();
        }
        public void PopPopuopToRoot()
        {
            PopupNavigation.PopAllAsync();
        }
        #endregion
    }
}
