using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ControlDesigner
{
    public class Configuration
    {
        public static readonly Type[] supportedControls = new[]
        {
            typeof(Label) ,
            typeof(Button),
            typeof(Slider),
            typeof(Entry),
            typeof(Editor),
            typeof(Frame),
            typeof(Stepper),
            typeof(SearchBar),
            typeof(ProgressBar),
            typeof(Picker),
            typeof(Image),
            typeof(DatePicker),
            typeof(BoxView),
        };
    }
}
