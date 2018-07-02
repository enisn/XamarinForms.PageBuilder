using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ControlDesigner.ViewModels.InnerViewModel
{
    public class PropertyViewModel : BaseViewModel
    {
        private string _value;

        public PropertyInfo ReflectedProperty { get; set; }
        public object Property { get; set; }
        public string PropertyName { get; set; }
        public Type Type { get; set; }
        public string Name { get; set; }
        public string Value { get => _value; set { _value = value; OnPropertyChanged(); } }
    }
}
