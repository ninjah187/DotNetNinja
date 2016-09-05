# DotNetNinja

## Tools and utilities used in my day-to-day work.
These tools are emerging from my love towards simplicity, work automation and clean APIs.
---

### DotNetNinja.NotifyPropertyChanged:
Library giving access to abstract class PropertyChangedNotifier which implements INotifyPropertyChanged and provides convenient mechanisms for notifying properties change. The mechanisms are type-safe and allow using IntelliSense.
No more magic strings causing headache while debugging!

#### Sample usage:

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
```

---

### DotNetNinja.UserAccess:
Custom user management, authentication and authorization library for ASP.NET Core with Entity Framework Core projects.