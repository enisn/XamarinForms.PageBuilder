using ControlDesigner.Pages;
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
	public partial class ControlDetailPage : EditorPage
    {
		public ControlDetailPage()
		{
			InitializeComponent ();
		}
        public ControlDetailPage(object control) : this()
        {
            this.BindingContext = new ControlDetailViewModel(control);
        }
	}
}