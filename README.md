# Forms.RecurrenceToolkit

A toolkit for Xamarin.Forms, inspired by hours of hours reimplementation of the same navigation logic, converters, and the other tools needed to build a mobile app idea.

## banditoth.Forms.RecurrenceToolkit.Converters
![nuGet version](https://img.shields.io/nuget/vpre/banditoth.Forms.RecurrenceToolkit.Converters)
Handy XAML Value converter collection. See the repository for the full list.

-- Usage --

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
