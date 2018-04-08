using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace com.company.todo.CustomRenders
{
    public class CustomSearchBar : SearchBar
    {
        // Use Bindable properties to maintain XAML binding compatibility

        public static readonly BindableProperty BarTintProperty = BindableProperty.Create<CustomSearchBar, Color?>(p => p.BarTint, null);
        public Color? BarTint
        {
            get { return (Color?)GetValue(BarTintProperty); }
            set { SetValue(BarTintProperty, value); }
        }

        public static readonly BindableProperty SearchStyleProperty = BindableProperty.Create<CustomSearchBar, string>(p => p.SearchStyle, "Default");
        public string SearchStyle
        {
            get { return (string)GetValue(SearchStyleProperty); }
            set { SetValue(SearchStyleProperty, value); }
        }

        public static readonly BindableProperty BarStyleProperty = BindableProperty.Create<CustomSearchBar, string>(p => p.BarStyle, "Default");
        public string BarStyle
        {
            get { return (string)GetValue(BarStyleProperty); }
            set { SetValue(BarStyleProperty, value); }
        }

    }
}
