# DotNetNinja

## Tools and utilities used in my day-to-day work.

__These tools are emerging from my love towards simplicity, work automation and clean APIs.__

---

### DotNetNinja.ImageResizer:

__Project aiming ASP.NET Core 1.0.__

__Dependencies:__
- [ImageProcessorCore](https://github.com/JimBobSquarePants/ImageProcessor/)

`DotNetNinja.ImageResizer` contains few features:

`Size` structure:
- which contains properties `int? Width` and `int? Height` (thanks to it you can give only one, desired dimension and auto-scale the other).

`IImageResizer` with `ImageResizer` service implementation:
- it takes image from input path, resizes it to target `Size` and saves to output path.

`IImagePathGenerator` with `ImagePathGenerator` service implementation:
- which produces output path for images of given `Size`.

`ImageResizerMiddleware`:
- thanks to interrupting HTTP pipeline, it gives you ability to resize images on the fly and use `<img>` element like this:
```html
<img src="some-image.jpg"> <!-- some-image.jpg in original size -->
<img src="some-image.jpg?w=200"> <!-- some-image.jpg resized to 200px width and proportional height -->
<img src="some-image.jpg?w=200&h=100"> <!-- some-image.jpg resized to 200px width and 100 px height -->
<img src="some-image.jpg?h=100"> <!-- some-image.jpg resized to 100 px height -->
```

`ImageServerMiddleware`:
- it only serves already resized images (gives 404 HTTP response if no particular size was found).

In order to use default implementations just add following to your Startup.cs:

```csharp
using DotNetNinja.ImageResizer;

public class Startup
{
	public void ConfigureServices(IServiceCollection services)
	{
		// adds ImageResizer and ImagePathGenerator singleton services implementations
		services.AddImageResizer();
	}

	public void Configure(IApplicationBuilder app)
	{
		app.UseImageResizer(); // for ImageResizerMiddleware

		// or:

		app.UseImageServer(); // for ImageServerMiddleware
	}
}
```

---

### DotNetNinja.NotifyPropertyChanged:
Library giving access to abstract class ```PropertyChangedNotifier``` which implements ```INotifyPropertyChanged``` and provides convenient mechanisms for notifying properties change. The mechanisms are type-safe and allow using IntelliSense.

No more magic strings causing headache while debugging!

__Sample usage:__

Derive from PropertyChangedNotifier:

```csharp
using DotNetNinja.NotifyPropertyChanged;

public class SampleObject : PropertyChangedNotifier 
{
	// ...
}
```

There are 3 ways you can notify property change:

- implicitly (my personal favorite)
```csharp
public int Property 
{
	get { return _property; }
	set { SetProperty(ref _property, value); }
}
int _property;
```

- with expression tree selector
```csharp
public int Property
{
	get { return _property; }
	set { SetProperty(ref _property, value, () => Property); }
}
int _property;
```

- with `nameof` keyword
```csharp
public int Property
{
	get { return _property; }
	set { SetProperty(ref _property, value, nameof(Property)); }
}
int _property;
```

---

### DotNetNinja.UserAccess:
Custom user management, authentication and authorization library for ASP.NET Core with Entity Framework Core projects.

---

### DotNetNinja.Wpf.ConfirmDialog:

Simple WPF yes/no dialog designed for both vanilla and `model-view-viewModel` with `dependency injection` scenarios.

__Sample usage:__

Via static method:

```csharp
using DotNetNinja.Wpf.ConfirmDialog;

if (await ConfirmDialog.ConfirmAsync("Do you really wanna do this?"))
{
	// confirmed
}
else 
{
	// declined
}
```

In MVVM with DI (assuming your IoC container resolves `IConfirmator` to `ConfirmDialogConfirmator`):

```csharp
using DotNetNinja.Wpf.ConfirmDialog;

public class ViewModel
{
	// commands, services and stuff

	IConfirmator _confirmator;

	public ViewModel(IConfirmator confirmator)
	{
		_confirmator = confirmator;
	}

	public async Task DoSomethingAsync()
	{
		if (await _confirmator.ConfirmAsync("Are you sure?"))
		{
			// confirmed
		}
		else
		{
			// declined
		}
	}
}
```
Lib provides also `AlwaysYesConfirmator` and `AlwaysNoConfirmator` types for mocking purposes.