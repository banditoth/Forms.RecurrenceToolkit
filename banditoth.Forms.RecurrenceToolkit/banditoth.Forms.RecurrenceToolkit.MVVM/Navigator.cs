using System;
using Xamarin.Forms;

namespace banditoth.Forms.RecurrenceToolkit.MVVM
{
    public class Navigator
    {
        private static Navigator _instance;

        private INavigation _navigation;

        private static Page _lastMainPage;

        public static Navigator Instance =>
                      _instance ?? (_instance = new Navigator());

        public INavigation Navigation =>
                     _navigation ?? throw new Exception("Navigator is not initialized");

        private void SetNavigation(INavigation navigation)
        {
            _navigation = navigation;
        }

        public void SetRoot(Page pageToSet)
        {
            if (pageToSet == null)
                throw new Exception("Could not set null as MainPage");

            Xamarin.Essentials.MainThread.InvokeOnMainThreadAsync(() =>
            {
                Application.Current.MainPage = new NavigationPage(pageToSet);
                SetNavigation(Application.Current.MainPage.Navigation);
                _lastMainPage = Application.Current.MainPage;
            });
        }

        public Page GetRoot()
        {
            return _lastMainPage;
        }
    }

}
