using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ControlDesigner.Views
{
    class EditingPage : CarouselPage
    {
        public EditingPage()
        {
            this.Children.Add(new PreviewPage());
            this.Children.Add(new ContentPage());
        }
        public EditingPage(object context)
        {
            this.Children.Add(new PreviewPage(context));
            this.Children.Add(new HierarchyPage(context));
        }
    }
}
