using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace banditoth.Forms.RecurrenceToolkit.MVVM
{
    public class BindableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        internal void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void SetProperty<T>(ref T backfield, T value, [CallerMemberName] string propertyName = null)
        {
            if (backfield?.Equals(value) == true)
                return;

            backfield = value;
            NotifyPropertyChanged(propertyName);
        }
    }
}
