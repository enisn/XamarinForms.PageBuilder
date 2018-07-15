using ControlDesigner.Controls;
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
    public partial class HierarchyPage : ContentPage
    {
        public HierarchyPage()
        {
            InitializeComponent();

        }
        public HierarchyPage(object context)
        {
            InitializeComponent();
            this.Content = new StackLayout
            {
                Children =
                {
                    new NestedView(context),
                }
            };
        }

        private void SwipeNextPage(object sender, EventArgs e)
        {

            if (this.Parent is CarouselPage)
                (this.Parent as CarouselPage).CurrentPage = (this.Parent as CarouselPage).Children[0];

        }
    }
}