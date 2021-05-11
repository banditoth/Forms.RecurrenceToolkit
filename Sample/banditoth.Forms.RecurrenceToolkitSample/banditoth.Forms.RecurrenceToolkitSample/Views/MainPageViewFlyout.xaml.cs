using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace banditoth.Forms.RecurrenceToolkitSample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageViewFlyout : ContentPage
    {
        public ListView ListView;

        public MainPageViewFlyout()
        {
            InitializeComponent();
        }
    }
}