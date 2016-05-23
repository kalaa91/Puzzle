using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Puzzle
{
    public class CustomViewCell : ViewCell
    {
        public CustomViewCell(List<View> Childerns)
        {
            StackLayout layout = new StackLayout();
            //layout.Padding = 10;// new Thickness(0, 2);
            layout.Spacing = 15;
            layout.Orientation = StackOrientation.Horizontal;
            layout.HorizontalOptions = LayoutOptions.FillAndExpand;
            layout.VerticalOptions = LayoutOptions.FillAndExpand;

            foreach (View child in Childerns)
            {
                layout.Children.Add(child);
            }
            View = layout;
        }
    }
}
