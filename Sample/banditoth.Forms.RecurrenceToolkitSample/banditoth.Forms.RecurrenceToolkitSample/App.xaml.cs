using banditoth.Forms.RecurrenceToolkit.MVVM;
using banditoth.Forms.RecurrenceToolkit.Multilanguage;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Globalization;
using banditoth.Forms.RecurrenceToolkitSample.Resources;

namespace banditoth.Forms.RecurrenceToolkitSample
{
    public partial class App : Application
    {
        private static bool _isAppInitalized = false;

        public App()
        {
            InitializeComponent();

            if (_isAppInitalized == false)
            {
                TranslationProvider.Initalize(new CultureInfo("en"), Translations.ResourceManager);
                Connector.Register(typeof(ViewModels.MainPageViewModel), typeof(Views.MainPageView));

                _isAppInitalized = true;
                Navigator.Instance.SetRoot(Connector.CreateInstance<ViewModels.MainPageViewModel>((vm, v) => { vm.Initalize(); }));
            }
            else
            {
                Navigator.Instance.SetRoot(Navigator.Instance.GetRoot());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
