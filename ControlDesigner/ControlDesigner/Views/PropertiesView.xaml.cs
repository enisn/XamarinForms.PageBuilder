using ControlDesigner.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ControlDesigner.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PropertiesView : ContentView
	{
		public PropertiesView ()
		{
			InitializeComponent ();
		}
        public PropertiesView(object param) : this()
        {
            this.BindingContext = new PropertiesViewModel(param);
        }

        private void Eye_Clicked(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity == 1 ? 0.5 : 1;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            (sender as ListView).SelectedItem = null;
        }
    }
}