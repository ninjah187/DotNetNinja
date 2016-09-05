using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetNinja.NotifyPropertyChanged;

namespace DotNetNinja.NotifyPropertyChanged.Samples
{
    public class SampleNotifier : PropertyChangedNotifier
    {
        // Choose your most convenient way of notifying property change.

        // - implicitly:
        public int Property
        {
            get { return _property; }
            set { SetProperty(ref _property, value); }
        }
        int _property;

        // - with expression tree selector:
        public int Property2
        {
            get { return _property2; }
            set { SetProperty(ref _property2, value, () => Property2); }
        }
        int _property2;

        // - with keyword "nameof":
        public int Property3
        {
            get { return _property3; }
            set { SetProperty(ref _property3, value, nameof(Property3)); }
        }
        int _property3;
    }
}
