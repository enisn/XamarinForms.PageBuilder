using ControlDesigner.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ControlDesigner.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NestedView : ContentView
    {
        public NestedView()
        {
            InitializeComponent();
        }
        public NestedView(object context)
        {
            this.BindingContext = context;
            InitializeComponent();
            if (context is Layout<View>)
            {
                (context as Layout<View>).ChildAdded -= NestedView_ChildAdded;
                (context as Layout<View>).ChildAdded += NestedView_ChildAdded;
                (context as Layout<View>).ChildRemoved -= NestedView_ChildRemoved;
                (context as Layout<View>).ChildRemoved += NestedView_ChildRemoved;
            }
            if (context is ContentView)
            {
                (context as ContentView).ChildAdded -= NestedView_ChildAdded;
                (context as ContentView).ChildAdded += NestedView_ChildAdded;
                (context as ContentView).ChildRemoved -= NestedView_ChildRemoved;
                (context as ContentView).ChildRemoved += NestedView_ChildRemoved;
            }
        }
        

        private void NestedView_ChildAdded(object sender, ElementEventArgs e)
        {
            try
            {
                slChildren.Children.Add(new NestedView(e.Element));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void NestedView_ChildRemoved(object sender, ElementEventArgs e)
        {
            try
            {
                slChildren.Children.Remove(slChildren.Children.FirstOrDefault(x => x.BindingContext == e.Element));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void Element_Tapped(object sender, EventArgs e)
        {
            PropertiesViewModel.OnTappedAControl(this.BindingContext);
        }
    }
}