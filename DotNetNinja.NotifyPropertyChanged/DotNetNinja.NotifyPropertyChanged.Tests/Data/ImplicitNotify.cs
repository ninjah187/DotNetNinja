using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetNinja.NotifyPropertyChanged;

namespace DotNetNinja.NotifyPropertyChanged.Tests
{
    public class ImplicitNotify : PropertyChangedNotifier
    {
        public int Property
        {
            get { return _property; }
            set { SetProperty(ref _property, value); }
        }
        int _property;
    }
}
