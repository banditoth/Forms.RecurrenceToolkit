<img src="https://raw.githubusercontent.com/banditoth/Forms.RecurrenceToolkit/main/Files/toolkit_icon.png" width="120" height="120"/>

# Forms.RecurrenceToolkit

A toolkit for Xamarin.Forms, inspired by hours of hours reimplementation of the same navigation logic, converters, and the other tools needed to build a mobile app idea.

__Discontinued__

Just as Xamarin.Forms is changing to a .NET MAUI, banditoth.Forms.RecurrenceToolkit will also undergo a change. As of today, I do not plan to develop any new functionality in the aforementioned package. Some parts of the package will be developed for .NET MAUI compatibility. You will be able to find the new packages in the future with the banditoth.MAUI prefix, which will also be available on GitHub.
I would like to thank you for the more than 2700 downloads.
Bug fixes will continue to be made, so if someone actually uses this for a live application, in that case no worries.

Follow my .NET MAUI and Xamarin development experiences @ https://www.banditoth.net/

**Azure DevOps**

![Azure DevOps builds](https://img.shields.io/azure-devops/build/bitfoxhungary/RecurrenceToolkit/5?label=Build%20status)

[View Build pipeline on AzureDevOps](https://dev.azure.com/bitfoxhungary/RecurrenceToolkit/_build)

**Packages**

| Package name | NuGet status |
| --- | --- |
| banditoth.Forms.RecurrenceToolkit.MVVM | ![nuGet version](https://img.shields.io/nuget/vpre/banditoth.Forms.RecurrenceToolkit.MVVM) |
| banditoth.Forms.RecurrenceToolkit.AOP | ![nuGet version](https://img.shields.io/nuget/vpre/banditoth.Forms.RecurrenceToolkit.AOP) |
| banditoth.Forms.RecurrenceToolkit.Multilanguage | ![nuGet version](https://img.shields.io/nuget/vpre/banditoth.Forms.RecurrenceToolkit.Multilanguage) |
| banditoth.Forms.RecurrenceToolkit.Converters | ![nuGet version](https://img.shields.io/nuget/vpre/banditoth.Forms.RecurrenceToolkit.Converters) |
| banditoth.Forms.RecurrenceToolkit.Identifiers | ![nuGet version](https://img.shields.io/nuget/vpre/banditoth.Forms.RecurrenceToolkit.Identifiers) |
| banditoth.Forms.RecurrenceToolkit.Logging | ![nuGet version](https://img.shields.io/nuget/vpre/banditoth.Forms.RecurrenceToolkit.Logging) |
| banditoth.Forms.RecurrenceToolkit.Logging.Console | ![nuGet version](https://img.shields.io/nuget/vpre/banditoth.Forms.RecurrenceToolkit.Logging.Console) |
| banditoth.Forms.RecurrenceToolkit.Logging.SQLite | ![nuGet version](https://img.shields.io/nuget/vpre/banditoth.Forms.RecurrenceToolkit.Logging.SQLite) |

## banditoth.Forms.RecurrenceToolkit.MVVM
![nuGet version](https://img.shields.io/nuget/vpre/banditoth.Forms.RecurrenceToolkit.MVVM)
![Nuget](https://img.shields.io/nuget/dt/banditoth.Forms.RecurrenceToolkit.MVVM)
[View package on NuGet.org](https://www.nuget.org/packages/banditoth.Forms.RecurrenceToolkit.MVVM/)

A ViewModel first driven MVVM Library.

**Usage**

Register your view and viewmodel connections in ```Connector```.

```cs
banditoth.Forms.RecurrenceToolkit.MVVM.Connector.Register(typeof(CameraViewModel), typeof(CameraView));
banditoth.Forms.RecurrenceToolkit.MVVM.Connector.Register(typeof(MainPageViewModel), typeof(MainPageView));
```

Get rid of using the ```Application.Current.MainPage``` property. Go to your ```app.xaml.cs``` file, and replace ```MainPage = new MainPage();``` code with the following

```cs
banditoth.Forms.RecurrenceToolkit.MVVM.Navigator.Instance.SetRoot(Connector.CreateInstance<MainPageViewModel>());
```

If you want to pass parameters to the view or the viewmodels, you can do by adding init action to the ```CreateInstance``` method.

```cs
Connector.CreateInstance<MainPageViewModel>((viewModel, view) => { viewModel.Foo(); view.Bar(); });
```

To access Xamarin Forms ```Navigation```, use:

```cs
Navigator.Navigation.PushModalAsync(Connector.CreateInstance<CameraViewModel>());
Navigator.Navigation.PushAsync(Connector.CreateInstance<CameraViewModel>());
Navigator.Navigation.PopModalAsync();
Navigator.Navigation.PopAsync();
// And so on.
```

If your application has been trayed, and get back, you can return the user to the last View, by calling:

```cs
banditoth.Forms.RecurrenceToolkit.MVVM.Navigator.Instance.GetRoot();
```

## banditoth.Forms.RecurrenceToolkit.AOP
![nuGet version](https://img.shields.io/nuget/vpre/banditoth.Forms.RecurrenceToolkit.AOP)
![Nuget](https://img.shields.io/nuget/dt/banditoth.Forms.RecurrenceToolkit.AOP)
[View package on NuGet.org](https://www.nuget.org/packages/banditoth.Forms.RecurrenceToolkit.AOP/)

A pre-release Aspect Orientated tool for Xamarin.

This tool uses Mono.Cecil to modify the builded assembly. 
Please be careful when using Assembly Signing or Code obfuscation with it.

**Usage**

Edit your csproj (basically the Forms project file), add the following code. 
This will let MSBuild to execute the tool after the project is built.
Change the Path to the build result assembly, in order to make it work.

```xm
	<UsingTask TaskName="AssemblyBuilder" AssemblyFile=".\bin\Debug\netcoreapp3.1\banditoth.Forms.RecurrenceToolkit.AOP.dll" />
	<Target Name="AssemblyBuilder" AfterTargets="AfterBuild">
		<AssemblyBuilder AssemblyFileName="<<<<<<< PATH TO YOUR ASSEMBLY >>>>>>>>" />
	</Target>
```

Implement your own method-decorator, implementing IMethodDecorator, for example:

```cs
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class ConsoleYaay : Attribute, IMethodDecorator
    {
        public ConsoleYaay()
        {

        }

        public void OnEnter()
        {
            Console.WriteLine("yaaay on enter!");
        }

        public void OnExit()
        {
            Console.WriteLine("yaaaay on exit");
        }
    }
```

And use it on your methods, like:

```cs
    [ConsoleYaay]
    public void BoringMethod()
    {
        Console.WriteLine(DateTime.Now);
    }
```

The result should be the following:
yaaay on enter!
2021. 06. 23. 21:00
yaaay on exit

**Important**
This package is in pre-release state.
Right now only parameterless constructors can be used for Method decorator attributes.
Class decorators, field decorators, and assembly decorators will be implemented in future versions.
Automatic csproj file modification (Adding the target) can be implemented too.

## banditoth.Forms.RecurrenceToolkit.Multilanguage
![nuGet version](https://img.shields.io/nuget/vpre/banditoth.Forms.RecurrenceToolkit.Multilanguage)
![Nuget](https://img.shields.io/nuget/dt/banditoth.Forms.RecurrenceToolkit.Multilanguage)
[View package on NuGet.org](https://www.nuget.org/packages/banditoth.Forms.RecurrenceToolkit.Multilanguage/)

A multilanguage translation provider for XAML, and for code behind.

**Usage**

Create your resx files in your solution.
For example if your applications default language is English, create a ```Translations.resx``` file, which contains the english translations, and if you want to support Hungarian language, you need to create a ```Translations.hu.resx``` file, which will contain the hungarian key value pairs.

Initalize the tool in your ```App.xaml.cs``` file.

```cs
banditoth.Forms.RecurrenceToolkit.Multilanguage.TranslationProvider.Initalize(new CultureInfo("en"), Resources.Translations.ResourceManager);
```

You can add different resx files, also from different assembly. Just call the Initalize procedure with passing more and more resource managers to it.

You can get the translation values in code behind by:
```cs
banditoth.Forms.RecurrenceToolkit.Multilanguage.TranslationProvider.GetTranslation("TranslationKey");
```

And in XAML by adding reference to the ```clr-namespace``` and using the markup extension:

```XAML
xmlns:multilanguage="clr-namespace:banditoth.Forms.RecurrenceToolkit.Multilanguage"
```

```XAML
Text="{multilanguage:Translation Key=TranslationKey}"
```
If you refer to ```General_Continue``` key in the code, each of the same resx files should need to contain translation for it.
If a translation is not found for example in the Hungarian version of resx file, but the english version contains it, it will return the english translation.
If the translation is not found in the 1st Resource manager, the code will look up in the further resource managers
If a translation could not be found in any resx files, the result will be ```TranslationMissing_ + the translation key```.
If an error occurs, ```TranslationError_ + translation key``` will be returned.


## banditoth.Forms.RecurrenceToolkit.Logging

| Package name | NuGet status | Downloads | Link |
| --- | --- | --- | --- |
| banditoth.Forms.RecurrenceToolkit.Logging | ![nuGet version](https://img.shields.io/nuget/vpre/banditoth.Forms.RecurrenceToolkit.Logging) | ![Nuget](https://img.shields.io/nuget/dt/banditoth.Forms.RecurrenceToolkit.Logging) | [View package on NuGet.org](https://www.nuget.org/packages/banditoth.Forms.RecurrenceToolkit.Logging/) | 
| banditoth.Forms.RecurrenceToolkit.Logging.Console | ![nuGet version](https://img.shields.io/nuget/vpre/banditoth.Forms.RecurrenceToolkit.Logging.Console) | ![Nuget](https://img.shields.io/nuget/dt/banditoth.Forms.RecurrenceToolkit.Logging.Console) | [View package on NuGet.org](https://www.nuget.org/packages/banditoth.Forms.RecurrenceToolkit.Logging.Console/) |
| banditoth.Forms.RecurrenceToolkit.Logging.SQLite | ![nuGet version](https://img.shields.io/nuget/vpre/banditoth.Forms.RecurrenceToolkit.Logging.SQLite) | ![Nuget](https://img.shields.io/nuget/dt/banditoth.Forms.RecurrenceToolkit.Logging.SQLite) | [View package on NuGet.org](https://www.nuget.org/packages/banditoth.Forms.RecurrenceToolkit.Logging.SQLite/) |

Logging functionnality with Console and SQLite, even your custom implemented logger ability.

**Usage**


In your ```App.xaml.cs```, initalize the logger like:

```cs
LoggingProvider.Initalize(
	// If you have installed the console logger:
	new ConsoleLogger(),
	// If you have installed SQLite Logger:
	new SQLiteLogger()
	);
``` 

The logger is including the calling method's name, and the .cs file name in the logs.
You can access the logger from anywhere by calling these methods:

```cs
LoggingProvider.LogCritical("It's a critical message");
LoggingProvider.LogDebug("It's a debug message");
LoggingProvider.LogError("It's an error message");
LoggingProvider.LogException(new Exception(), "It's an exception");
LoggingProvider.LogInformation("It's an information message");
LoggingProvider.LogTrace("It's a trace message");
LoggingProvider.LogTrace(new StackTrace());
LoggingProvider.LogWarning("It's a warning message");
``` 

By default, the console and the SQLite logger logs exceptions in error level.

You can implement your own logger by deriving from ```BaseLogger``` class, like:

```cs
	public class CustomLogger : BaseLogger
	{
		public CustomLogger() : base(new LoggerOptions()
		{
			IncludeCallerSourceFullFileName = true, // This will print C:/Users/Path/AssemblyFile.cs
			IncludeCallerSourceShortFileName = false, // This will print AssemblyFile.cs
			ExceptionLevel = Enumerations.LogLevel.Error, // The LogExceptions calls routed to log to the loglevel set.
			IncludeCallerMethodName = true // This can print the calling method's name
		})
		{

		}
		
		public override void LogCritical(string criticalMessage, string callerMethod, string filePath)
		{
			// Your own method
		}

		// .. File continues
``` 

## banditoth.Forms.RecurrenceToolkit.Converters
![nuGet version](https://img.shields.io/nuget/vpre/banditoth.Forms.RecurrenceToolkit.Converters)
![Nuget](https://img.shields.io/nuget/dt/banditoth.Forms.RecurrenceToolkit.Converters)
[View package on NuGet.org](https://www.nuget.org/packages/banditoth.Forms.RecurrenceToolkit.Converters/)

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

## banditoth.Forms.RecurrenceToolkit.Identifiers
![nuGet version](https://img.shields.io/nuget/vpre/banditoth.Forms.RecurrenceToolkit.Identifiers)
![Nuget](https://img.shields.io/nuget/dt/banditoth.Forms.RecurrenceToolkit.Identifiers)
[View package on NuGet.org](https://www.nuget.org/packages/banditoth.Forms.RecurrenceToolkit.Identifiers/)

Generates an unique identifier for the application, which will be stored until the application is being reinstalled, or the application's data being erased.
Further improvements: - Read existing permanent identifiers from the OS.

**Usage**

```cs
string id = banditoth.Forms.RecurrenceToolkit.Identifiers.IdentifierProvider.UniqueId;
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## Third party
Icon made by inipagistudio from www.flaticon.com
