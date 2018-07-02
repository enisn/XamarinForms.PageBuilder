using Plugin.InputKit.Shared.Controls;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ControlDesigner.Helpers
{
    public class DialogHelper
    {
        /// <summary>
        /// Textbox açarak kullanıcıdan string tipinde input alır ve sonucu return eder.
        /// </summary>
        /// <param name="title">Başlık olarak gözükecek yazı</param>
        /// <param name="message">Başlığın altında yer alacak açıklama</param>
        /// <param name="editorContent">Bu değer textbox'ın içinde yer alacak yazı, boş textbox için null bırakılmalı.</param>
        /// <returns></returns>
        public static Task<string> GetUserInputAsync(string title = "", string message = "Bir Değer Girin;", string editorContent = "")
        {
            var tcs = new TaskCompletionSource<string>();

            var lblTitle = new Label { Text = title, Margin = 10, HorizontalOptions = LayoutOptions.Center, FontAttributes = FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) };
            var lblMessage = new Label { Text = message };
            var txtInput = new Editor { Text = editorContent, HeightRequest = 80, BackgroundColor = Color.White };
            
            var btnOk = new Button
            {
                Text = "TAMAM",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Accent,
                TextColor = Color.White
            };
            btnOk.Clicked += async (s, e) =>
            {
                // close page
                var result = txtInput.Text;
                await PopupNavigation.PopAsync();
                // pass result
                tcs.SetResult(result);
            };

            var btnCancel = new Button
            {
                Text = "İPTAL",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.FromRgb(0.8, 0.8, 0.8)
            };
            btnCancel.Clicked += async (s, e) =>
            {
                await PopupNavigation.PopAsync();
                tcs.SetResult(null);
            };

            var slButtons = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { btnOk, btnCancel },
            };

            var layout = new StackLayout
            {
                BackgroundColor = Color.White,
                Margin = new Thickness(25),
                Padding = new Thickness(10),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children = { lblTitle, lblMessage, new Grid { Padding = 0.8, BackgroundColor = Color.FromHex("#005da4"), Children = { txtInput } }, slButtons },
            };

            // create and show page
            var page = new PopupPage();
            page.Content = layout;
            page.CloseWhenBackgroundIsClicked = false;
            PopupNavigation.PushAsync(page);
            // open keyboard
            txtInput.Focus();


            // code is waiting her, until result is passed with tcs.SetResult() in btn-Clicked
            // then proc returns the result
            return tcs.Task;
        }

        internal static Task<Type> PickControlAsync()
        {
            return GetRadioButtonResult("Choose a control to add:",Configuration.supportedControls);
        }

        internal static Task<DateTime> GetDateTimeAsync(string title, DateTime selectedDate = default(DateTime))
        {
            TaskCompletionSource<DateTime> tcs = new TaskCompletionSource<DateTime>();

            DatePicker dPicker = new DatePicker { Date = selectedDate };

             Button button = new Button
             {
                 Text = "TAMAM",
                 CornerRadius = 20,
                 HorizontalOptions = LayoutOptions.FillAndExpand,
                 BackgroundColor = Color.Accent,
                 TextColor = Color.White
             };
            Button btnCancel = new Button
            {
                Text = "İPTAL",
                CornerRadius = 20,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White,
                TextColor = Color.Accent
            };

            button.Clicked += (s, e) =>
            {
                tcs.SetResult(dPicker.Date);
                PopupNavigation.PopAsync();
            };
            btnCancel.Clicked += (s, e) =>
            {
                tcs.TrySetResult(default(DateTime));
                PopupNavigation.PopAsync();
            };

            Frame frame = new Frame
            {
                Margin = 25,
                BackgroundColor = Color.WhiteSmoke,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Content = new StackLayout
                {
                    Spacing = 12,
                    Children =
                            {
                                new Label{ Text= title,Margin=10, HorizontalOptions = LayoutOptions.Center, FontSize = Device.GetNamedSize(NamedSize.Large,typeof(Label))},
                                dPicker,
                                new StackLayout { Orientation = StackOrientation.Horizontal, Children={ button, btnCancel } }
                            }
                }
            };
            var popupPage = new PopupPage { Content = frame };

            return tcs.Task;
        }


        internal static Task<Thickness> GetThichnessAsync(string title, Thickness current = default(Thickness))
        {
            TaskCompletionSource<Thickness> tcs = new TaskCompletionSource<Thickness>();

            Entry txtTop = new Entry { Text = current.Top.ToString(), Keyboard = Keyboard.Numeric };
            Entry txtLeft = new Entry { Text = current.Left.ToString(), Keyboard = Keyboard.Numeric, HorizontalOptions= LayoutOptions.FillAndExpand };
            Entry txtRight = new Entry { Text = current.Right.ToString(), Keyboard = Keyboard.Numeric, HorizontalOptions = LayoutOptions.FillAndExpand };
            Entry txtBottom = new Entry { Text = current.Bottom.ToString(), Keyboard = Keyboard.Numeric };

            Button button = new Button
            {
                Text = "TAMAM",
                CornerRadius = 20,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Accent,
                TextColor = Color.White
            };
            Button btnCancel = new Button
            {
                Text = "İPTAL",
                CornerRadius = 20,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White,
                TextColor = Color.Accent
            };

            button.Clicked += (s, e) =>
            {
                tcs.SetResult(new Thickness(Convert.ToDouble(txtLeft.Text), Convert.ToDouble(txtTop.Text), Convert.ToDouble(txtRight.Text), Convert.ToDouble(txtBottom.Text)));
                PopupNavigation.PopAsync();
            };
            btnCancel.Clicked += (s, e) =>
            {
                tcs.TrySetResult(default(Thickness));
                PopupNavigation.PopAsync();
            };

            Frame frame = new Frame
            {
                Margin = 25,
                BackgroundColor = Color.WhiteSmoke,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Content = new StackLayout
                {
                    Spacing = 12,
                    Children =
                            {
                                new Label{ Text= title,Margin=10, HorizontalOptions = LayoutOptions.Center, FontSize = Device.GetNamedSize(NamedSize.Large,typeof(Label))},
                                txtTop,
                                new StackLayout{ Orientation = StackOrientation.Horizontal,
                                Children =
                                    {
                                        txtLeft, txtRight
                                    } },
                                txtBottom,
                                new StackLayout { Orientation = StackOrientation.Horizontal, Children={ button, btnCancel } }
                            }
                }
            };
            var popupPage = new PopupPage { Content = frame };
            PopupNavigation.PushAsync(popupPage);
            return tcs.Task;
        }



        /// <summary>
        /// Picker'ın içini doldurmak için.
        /// </summary>
        /// <returns>The picker result async.</returns>
        /// <param name="title">Title.</param>
        /// <param name="message">Message.</param>
        /// <param name="collection">Collection.</param>
        /// <param name="display">Display.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static Task<T> GetPickerResultAsync<T>(string title, string message, IList<T> collection, T selectedItem = default(T), string display = null)
        {
            TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();
            Picker picker;
            if (display == null)
            {
                picker = new Picker { ItemsSource = (IList)collection, BackgroundColor = Color.White, SelectedItem = selectedItem };
            }
            else
            {
                picker = new Picker { ItemsSource = (IList)collection, ItemDisplayBinding = new Binding(display), BackgroundColor = Color.White, SelectedItem = selectedItem };

            }

            Button button = new Button
            {
                Text = "TAMAM",
                CornerRadius = 20,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Accent,
                TextColor = Color.White
            };
            Button btnCancel = new Button
            {
                Text = "TEMİZLE",
                CornerRadius = 20,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White,
                TextColor = Color.Accent
            };
            button.Clicked += (s, e) =>
            {
                tcs.SetResult((T)picker.SelectedItem);
                PopupNavigation.PopAsync();
            };
            btnCancel.Clicked += (s, e) =>
            {
                tcs.TrySetResult(default(T));
                PopupNavigation.PopAsync();
            };
            Frame frame = new Frame
            {
                Margin = 25,
                BackgroundColor = Color.WhiteSmoke,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Content = new StackLayout
                {
                    Spacing = 12,
                    Children =
                            {
                                new Label{ Text= title,Margin=10, HorizontalOptions = LayoutOptions.Center, FontSize = Device.GetNamedSize(NamedSize.Large,typeof(Label))},
                                picker,
                                new StackLayout { Orientation = StackOrientation.Horizontal, Children={ button, btnCancel } }
                            }
                }
            };
            var popupPage = new PopupPage { Content = frame };
            popupPage.Disappearing += (s, e) =>
            {
                //tcs.SetResult((T)picker.SelectedItem);
            };


            PopupNavigation.PushAsync(popupPage);

            return tcs.Task;
        }

        /// <summary>
        /// Gets the radio button result.
        /// </summary>
        /// <returns>The radio button result.</returns>
        /// <param name="title">Title.</param>
        /// <param name="source">Source.</param>
        /// <param name="selectedItem">Selected item.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static Task<T> GetRadioButtonResult<T>(string title, IEnumerable<T> source, T selectedItem = default(T)) /*where T: new()*/
        {
            TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();
            RadioButtonGroupView rgv = new RadioButtonGroupView { Margin = new Thickness(30, 5), };
            Button btn = new Button { Text = "UYGULA", Margin = new Thickness(20, 0), CornerRadius = 20, BackgroundColor = Color.Accent, TextColor = Color.White, };
            foreach (var item in source)
                rgv.Children.Add(new RadioButton(item, null, item.Equals(selectedItem)) { Color = Color.Accent });

            btn.Clicked += (s, e) =>
            {
                tcs.TrySetResult((T)rgv.SelectedItem);
                PopupNavigation.PopAsync();
            };

            Frame frame = new Frame
            {
                BorderColor = Color.Transparent,
                BackgroundColor =Color.WhiteSmoke,
                Margin = 25,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Content = new StackLayout
                {
                    Padding = new Thickness(5),
                    Children =
                    {
                        new Label { Text=title, FontSize=18, HorizontalOptions = LayoutOptions.Center, FontAttributes=FontAttributes.Bold  },
                        new ScrollView { Content = rgv },
                        btn
                    }
                }
            };

            var popupPage = new PopupPage { Content = frame, CloseWhenBackgroundIsClicked = false };
            PopupNavigation.PushAsync(popupPage);
            popupPage.Disappearing += (s, e) => tcs.TrySetResult((T)rgv.SelectedItem);


            return tcs.Task;
        }
    }
}
