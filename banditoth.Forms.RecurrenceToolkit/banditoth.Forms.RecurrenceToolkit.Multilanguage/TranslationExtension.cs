using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace banditoth.Forms.RecurrenceToolkit.Multilanguage
{
    [ContentProperty(nameof(Key))]
    public class TranslationExtension : IMarkupExtension<BindingBase>
    {
        public TranslationExtension()
        {

        }

        public string Key { get; set; }

        public BindingBase ProvideValue(IServiceProvider serviceProvider)
        {
            var binding = new Binding
            {
                Mode = BindingMode.OneWay,
                Path = $"[{Key}]",
                Source = TranslationBinder.Instance,
            };
            return binding;
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return ProvideValue(serviceProvider);
        }
    }

}
