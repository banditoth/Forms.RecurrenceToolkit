using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace banditoth.Forms.RecurrenceToolkit.MVVM
{
    public static class Connector
    {
        private static readonly Dictionary<Type, Type> _connections = new Dictionary<Type, Type>();

        public static void Register(Type viewModelType, Type viewType)
        {
            if (viewType == null)
                throw new Exception($"The {nameof(viewType)} could not be null!");

            if (viewModelType == null)
                throw new Exception($"The {nameof(viewModelType)} could not be null!");

            if (_connections.ContainsKey(viewModelType))
                throw new Exception($"This type has been already registered.: {viewModelType.FullName}");

            _connections.Add(viewModelType, viewType);
        }

        public static Page CreateInstance<TViewmodel>(Action<TViewmodel, Page> initialiser = null) where TViewmodel : BaseViewModel
        {
            if (_connections.ContainsKey(typeof(TViewmodel)) == false)
                throw new Exception($"The ({typeof(TViewmodel)}) is not registered. Use the {nameof(Register)} method to register it");

            Type pageType = _connections[typeof(TViewmodel)];
            Page pageInstance = (Page)Activator.CreateInstance(pageType);
            TViewmodel viewModelInstance = (TViewmodel)Activator.CreateInstance(typeof(TViewmodel));

            pageInstance.BindingContext = viewModelInstance;

            if (initialiser != null)
            {
                initialiser.Invoke(viewModelInstance, pageInstance);
            }

            return pageInstance;
        }
    }

}
