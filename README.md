# Forms.RecurrenceToolkit

A toolkit for Xamarin.Forms, inspired by hours of hours reimplementation of the same navigation logic, converters, and the other tools needed to build a mobile app idea.

## banditoth.Forms.RecurrenceToolkit.Multilanguage
![nuGet version](https://img.shields.io/nuget/vpre/banditoth.Forms.RecurrenceToolkit.Multilanguage)

A multilanguage translation provider for XAML, and for code behind.

**Usage**

Create your resx files in your solution.
For example if your applications default language is English, create a Translations.resx file, which contains the english translations, and if you want to support Hungarian language, you need to create a Translations.hu.resx file, which will contain the hungarian key value pairs.

Initalize the tool in your App.xaml.cs file.

```cs
banditoth.Forms.RecurrenceToolkit.Multilanguage.TranslationProvider.Initalize(new CultureInfo("en"), Resources.Translations.ResourceManager);
```

You can add different resx files, also from different assembly. Just call the Initalize procedure with passing more and more resource managers to it.

You can get the translation values in code behind by:
```cs
banditoth.Forms.RecurrenceToolkit.Multilanguage.TranslationProvider.GetTranslation("TranslationKey");
```

And in XAML by adding reference to the clr-namespace and using the markup extension:

```XAML
xmlns:multilanguage="clr-namespace:banditoth.Forms.RecurrenceToolkit.Multilanguage"
```

```XAML
Text="{multilanguage:Translation Key=TranslationKey}"
```
If you refer to ```General_Continue``` key in the code, each of the same resx files should need to contain translation for it.
If a translation is not found for example in the Hungarian version of resx file, but the english version contains it, it will return the english translation.
If the translation is not found in the 1st Resource manager, the code will look up in the further resource managers
If a translation could not be found in any resx files, the result will be TranslationMissing_ + the translation key.
If an error occurs, TranslationError_ + translation key will be returned.


## banditoth.Forms.RecurrenceToolkit.Converters
![nuGet version](https://img.shields.io/nuget/vpre/banditoth.Forms.RecurrenceToolkit.Converters)

Handy XAML Value converter collection. See the repository for the full converter list.

**Usage**

Declare the clr-namespace:
```XAML
xmlns:converters="clr-namespace:banditoth.Forms.RecurrenceToolkit.Converters"
```
Use the converters on bindings:
```XAML
IsVisible="{Binding Path=NullableData, Converter={converters:NullToFalseConverter}}"
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## Third party
Icon made by inipagistudio from www.flaticon.com
