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
	public partial class LayoutSelectionPage : ContentPage
	{
		public LayoutSelectionPage ()
		{
			InitializeComponent ();
		}

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            (sender as ListView).SelectedItem = null;
        }
    }
}