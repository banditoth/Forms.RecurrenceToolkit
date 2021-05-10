using System;
using System.Collections.Generic;
using System.Text;

namespace banditoth.Forms.RecurrenceToolkit.MVVM
{
    public class BaseViewModel : BindableObject
    {
        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value, nameof(IsLoading));
        }
    }

}
