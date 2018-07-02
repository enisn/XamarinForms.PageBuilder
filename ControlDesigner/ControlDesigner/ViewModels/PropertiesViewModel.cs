using ControlDesigner.Helpers;
using ControlDesigner.ViewModels.InnerViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ControlDesigner.ViewModels
{
    public class PropertiesViewModel : BaseViewModel
    {
        private PropertyInfo _propChildren, _propContent;
        private View _control;
        private IList<PropertyViewModel> _propertiesList;
        private PropertyViewModel _selectedProperty;

        public PropertiesViewModel()
        {
            PropertiesList = new ObservableCollection<PropertyViewModel>();
            InitCommands();
        }

        public PropertiesViewModel(object context) : this()
        {
            if (context is View)
            {
                this.Control = context as View;
                InitProeprties();

                _propChildren = context.GetType().GetProperty("Children", typeof(IList<View>));
                _propContent = context.GetType().GetProperty("Content", typeof(View));
                OnPropertyChanged(nameof(DoesHaveChildren));
                OnPropertyChanged(nameof(DoesHaveContent));
            }

        }
        private void InitCommands()
        {
            RemoveControlCommand = new Command(RemoveControl);
            AddChildrenCommand = new Command(AddChildren);
            ClearChildrenCommand = new Command(ClearChildren);
            SetContentCommand = new Command(SetContent);
            ClearContentCommand = new Command(ClearContent);
        }

        public IList<PropertyViewModel> PropertiesList { get => _propertiesList; set { _propertiesList = value; OnPropertyChanged(); } }
        public PropertyViewModel SelectedProperty { get => _selectedProperty; set { _selectedProperty = value; PropertySelected(value); OnPropertyChanged(); } }

        public bool DoesHaveChildren { get => _propChildren != null; }
        public bool DoesHaveContent { get => _propContent != null; }

        public Command RemoveControlCommand { get; set; }
        public Command AddChildrenCommand { get; set; }
        public Command ClearChildrenCommand { get; set; }
        public Command SetContentCommand { get; set; }
        public Command ClearContentCommand { get; set; }
        public View Control
        {
            get { return _control; }
            set { _control = value; OnPropertyChanged(); }
        }

        void InitProeprties()
        {
            var reflectedProps = Control.GetType().GetProperties();
            foreach (var property in reflectedProps)
            {
                if (property.CanRead && property.CanWrite)
                    PropertiesList.Add(new PropertyViewModel
                    {
                        Name = property.Name,
                        ReflectedProperty = property,
                        PropertyName = property.Name,
                        Type = property.PropertyType,
                        Value = property.GetValue(this.Control)?.ToString(),
                    });
            }
        }

        async void PropertySelected(PropertyViewModel prop)
        {
            if (prop == null) return;

            try
            {
                if (prop.Type == typeof(Boolean))
                {
                    bool newVal = await DialogHelper.GetRadioButtonResult("Choose new value:", new[] { true, false }, (bool)prop.ReflectedProperty.GetValue(this.Control));
                    prop.ReflectedProperty.SetValue(this.Control, newVal);
                    prop.Value = newVal.ToString();
                    return;
                }

                if (prop.Type == typeof(String))
                {
                    var _newVal = await DialogHelper.GetUserInputAsync("Type new value:", "", prop.ReflectedProperty.GetValue(this.Control)?.ToString());
                    if (_newVal == null) return;
                    prop.ReflectedProperty.SetValue(this.Control, _newVal);
                    prop.Value = _newVal;
                    return;
                }

                if (prop.Type == typeof(Color))
                {
                    Color? newVal = await Plugin.DialogKit.CrossDiaglogKit.Current.GetColorAsync("Choose new color:", "", typeof(Color).GetFields().Select(s => (Color)s.GetValue(null)).ToArray());
                    if (newVal != null)
                    {
                        prop.ReflectedProperty.SetValue(this.Control, newVal);
                        prop.Value = newVal.ToString();
                    }
                    return;
                }
                if (prop.Type == typeof(LayoutOptions))
                {
                    string s_newVal = await DialogHelper.GetRadioButtonResult("Choose new value:", typeof(LayoutOptions).GetFields().Select(s => s.Name).ToList(), prop.ReflectedProperty.Name);
                    prop.ReflectedProperty.SetValue(this.Control, (LayoutOptions)typeof(LayoutOptions).GetField(s_newVal)?.GetValue(null));
                    prop.Value = s_newVal.ToString();
                    return;
                }

                if (prop.Type == typeof(Keyboard))
                {
                    string s_newVal = await DialogHelper.GetRadioButtonResult("Choose new value:", typeof(Keyboard).GetFields().Select(s => s.Name).ToList(), prop.ReflectedProperty.Name);
                    prop.ReflectedProperty.SetValue(this.Control, (Keyboard)typeof(Keyboard).GetField(s_newVal)?.GetValue(null));
                    prop.Value = s_newVal.ToString();
                    return;
                }
                #region Enums
                if (prop.Type.IsEnum)
                {
                    var newVal = Enum.Parse(prop.Type,
                        await DialogHelper.GetRadioButtonResult("Choose new value:", prop.Type.GetEnumNames(), prop.ReflectedProperty.GetValue(this.Control).ToString())
                        );
                    prop.ReflectedProperty.SetValue(this.Control, newVal);
                    prop.Value = newVal.ToString();
                    return;
                }


                //if (prop.Type == typeof(TextAlignment))
                //{
                //    TextAlignment newVal = (TextAlignment)Enum.Parse(typeof(TextAlignment),
                //        await DialogHelper.GetRadioButtonResult("Choose new value:", typeof(TextAlignment).GetEnumNames(), prop.ReflectedProperty.GetValue(this.Control).ToString())
                //        );
                //    prop.ReflectedProperty.SetValue(this.Control, newVal);
                //    prop.Value = newVal.ToString();
                //    return;
                //}
                //if (prop.Type == typeof(Aspect))
                //{
                //    Aspect newVal = (Aspect)Enum.Parse(typeof(Aspect),
                //        await DialogHelper.GetRadioButtonResult("Choose new value:", typeof(Aspect).GetEnumNames(), prop.ReflectedProperty.GetValue(this.Control).ToString())
                //        );
                //    prop.ReflectedProperty.SetValue(this.Control, newVal);
                //    prop.Value = newVal.ToString();
                //    return;
                //}
                //if (prop.Type == typeof(FontAttributes))
                //{
                //    FontAttributes newVal = (FontAttributes)Enum.Parse(typeof(FontAttributes),
                //        await DialogHelper.GetRadioButtonResult("Choose new value:", typeof(FontAttributes).GetEnumNames(), prop.ReflectedProperty.GetValue(this.Control).ToString())
                //        );
                //    prop.ReflectedProperty.SetValue(this.Control, newVal);
                //    return;
                //}
                #endregion


                if (prop.Type == typeof(Thickness))
                {
                    Thickness newVal = await DialogHelper.GetThichnessAsync("Choose new DateTime", (Thickness)prop.ReflectedProperty.GetValue(this.Control));
                    prop.ReflectedProperty.SetValue(this.Control, newVal);
                    return;
                }

                if (prop.Type == typeof(DateTime))
                {
                    DateTime newVal = await DialogHelper.GetDateTimeAsync("Choose new DateTime", (DateTime)prop.ReflectedProperty.GetValue(this.Control));
                    prop.ReflectedProperty.SetValue(this.Control, newVal);
                    return;
                }

                if (prop.Type.IsValueType)
                {
                    float _newVal = Convert.ToSingle(await DialogHelper.GetUserInputAsync("Type new value", "", prop.ReflectedProperty.GetValue(this.Control).ToString()));
                    prop.ReflectedProperty.SetValue(this.Control, Convert.ChangeType(_newVal, prop.Type));
                    prop.Value = _newVal.ToString();

                    return;
                }
                else
                {
                    var _newVal = await DialogHelper.GetUserInputAsync("Type new value:", "", prop.ReflectedProperty.GetValue(this.Control)?.ToString());
                    prop.ReflectedProperty.SetValue(this.Control, Convert.ChangeType(_newVal, prop.Type));
                    prop.Value = _newVal;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

        }



        #region LayoutOptions


        private void ClearContent(object obj)
        {
            _propContent.SetValue(this.Control, null);
        }

        private async void SetContent(object obj)
        {
            Type controlToAdd = await DialogHelper.PickControlAsync(); //PICK CONTROL
            var newControl = (View)Activator.CreateInstance(controlToAdd);
            _propContent.SetValue(this.Control, newControl);
            Navigation.PopPopuopToRoot();
            Navigation.PushPropertiesPopup(newControl);
        }

        private void ClearChildren(object obj)
        {
            (_propChildren.GetValue(this.Control) as IList<View>).Clear();
        }

        private async void AddChildren(object obj)
        {
            Type controlType = await DialogHelper.PickControlAsync(); //PICK CONTROL

            var newControl = Activator.CreateInstance(controlType);
            (_propChildren.GetValue(this.Control) as IList<View>).Add(newControl as View);

            Navigation.PopPopuopToRoot();
            Navigation.PushPropertiesPopup(newControl);
        }

        private void RemoveControl(object obj)
        {
            try
            {
                var parent = this.Control.GetType().GetProperty("Parent")?.GetValue(this.Control);
                (parent?.GetType().GetProperty("Children", typeof(IList<View>))?.GetValue(parent) as IList<View>).Remove(this.Control);
                Navigation.PopPopup();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }
        #endregion

    }
}
