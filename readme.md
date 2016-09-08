# DotNetNinja

## Tools and utilities used in my day-to-day work.

__These tools are emerging from my love towards simplicity, work automation and clean APIs.__

---

### DotNetNinja.ImageResizer:

__Project aiming ASP.NET Core 1.0.__

__Dependencies:__
- [ImageProcessorCore](https://github.com/JimBobSquarePants/ImageProcessor/)

__What it is:__

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

__Sample usage:__

In order to use default implementations just add following to your `Startup.cs`:

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

### DotNetNinja.TypeFiltering:

It's simple, fluent API that provides little bits of C# 7's pattern-matching-like functionality. Functional programming features are cool!

__Sample usage:__

```csharp
interface IFoo
{
	void Foo();
}

interface IGoo
{
	bool IsValid { get; }
	void Goo();
}

interface IHoo
{
	void Hoo();
}

void SampleGeneric<T>(T item)
{
	item
		.When<IFoo>(i => i.Foo())		// lambda calling i.Foo() fires if item implements IFoo
		.When<IGoo>((i, filtering) => 	// lambda fires if item implements IGoo
		{
			if (i.IsValid)
			{
				i.Goo();
				filtering.Break();		// if control flow reaches this point, no further type tests will be performed
			}
		})
		.When<IHoo>(i => i.Hoo())		// this is reached only if filtering.Break() wasn't called
		.ThrowIfNotRecognized();		// if no type was recognized, InvalidOperationException is thrown
}

// note: API doesn't automatically break after successful type test unless broken explicitly by user (just like in IGoo case or with BreakIfRecognized() method).
```

---

### DotNetNinja.UserAccess:

__Project aiming ASP.NET Core 1.0.__

__Dependencies:__
* ASP.NET Core 1.0 MVC
* Entity Framework Core

__What it is:__

Custom user management, authentication and authorization (the latter coming in the future) library for ASP.NET Core with Entity Framework Core projects.

`DotNetNinja.UserAccess` gives you:

`User` class:
- model representing user of application.

`IHashManager` with `HashManager` implementation:
- service used to generate and verify password hashes (PBKDF2 by default).

`IUserService` with `UserService` implementation:
- service providing basic users operations like create, delete, log in, log out.

`UserAccessFilter`:
- MVC authentication filter that lets you control access to your controllers and actions in convenient way.

`UnauthorizedRedirectionMiddleware`:
- middleware that lets you redirect all HTTP 401 unauthorized responses (which are returned by default if authentication fails).

__Sample usage:__

Add `DbSet<User>` to your app's `DbContext`.

```csharp
using DotNetNinja.UserAccess;

public class AppDbContext : DbContext
{
	public DbSet<User> Users { get; set; }

	// ...
}
```

Configure your `Startup.cs`:

```csharp
using DotNetNinja.UserAccess;

public class Startup
{
	public void ConfigureServices(IServiceCollection services)
	{
		// ...

		// you also have to register your DbContext and Mvc services

		services.AddUserAccess<AppDbContext>();

		// ...
	}

	public void Configure(IApplicationBuilder app)
	{
		// ...

		// optional:
		app.UseUnauthorizedRedirection("/users/login"); // unauthorized requests will be redirected to /users/login route

		// ...
	}
}
```

Now you can restrict access to your controllers and actions by adding `[UserAccess]` attribute to those:

```csharp
using DotNetNinja.UserAccess;

public class HomeController : Controller
{
	// this action can be accessed by anyone:
	public IActionResult Index()
	{
		return View();
	}

	// this action can be accessed only if request contains cookie with valid access token:
	[UserAccess]
	public IActionResult RestrictedAction()
	{
		return View();
	}
}

// every action in this controller can be accessed only if request contains cookie with valid access token:
[UserAccess]
public class RestrictedController : Controller
{
	public IActionResult Index()
	{
		return View();
	}

	public IActionResult DoSomething()
	{
		// ...

		return View();
	}
}
```

In order to access basic user management functionality you can use `IUserService`.  
Sample of log in/log out controller with the injected service:

```csharp
using DotNetNinja.UserAccess;

public class UsersController : Controller
{
	IUserService _userService;

	public UsersController(IUserService userService)
	{
		_userService = userService;
	}

	public IActionResult LogIn()
		=> View();

	[HttpPost]
	public async Task<IActionResult> LogIn(string login, string password)
	{
		var token = await _userService.LogInAsync(login, password);

		if (token == null)
		{
			return Unauthorized();
		}

		return RedirectToAction("Index", "Home");
	}

	[UserAccess]
	public async Task<IActionResult> LogOut()
	{
		await _userService.LogOutAsync();

		return RedirectToAction("Index", "Home");
	}
}
```

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