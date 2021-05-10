using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace banditoth.Forms.RecurrenceToolkit.Multilanguage
{
    public class TranslationBinder : INotifyPropertyChanged
    {
        public string this[string text] => TranslationProvider.GetTranslation(text);

        public static TranslationBinder Instance { get; } = new TranslationBinder();

        public event PropertyChangedEventHandler PropertyChanged;

        public void Invalidate()
        {
            Device.BeginInvokeOnMainThread(() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null)));
        }
    }
}
