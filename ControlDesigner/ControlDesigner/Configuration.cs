using Plugin.InputKit.Shared.Controls;
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
            typeof(ActivityIndicator),
            typeof(BoxView),
            typeof(Button),
            typeof(DatePicker),
            typeof(Editor),
            typeof(Entry),
            typeof(Image),
            typeof(Label),
            typeof(OpenGLView),
            typeof(Picker),
            typeof(ProgressBar),
            typeof(SearchBar),
            typeof(Slider),
            typeof(Stepper),
            typeof(Switch),
            typeof(TimePicker),
            typeof(WebView),

            typeof(ContentView),
            typeof(ScrollView),
            typeof(Frame),
            typeof(StackLayout),
            typeof(FlexLayout),
            typeof(Grid),

            typeof(CheckBox),
            typeof(RadioButtonGroupView),
            typeof(RadioButton),
            typeof(AdvancedEntry),
            typeof(AdvancedSlider),
            typeof(FormView),
        };
    }
}
