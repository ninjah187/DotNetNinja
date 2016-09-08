using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetNinja.NotifyPropertyChanged;

namespace DotNetNinja.NotifyPropertyChanged.Tests
{
    public class DifferentProperties : PropertyChangedNotifier
    {
        public int IntProperty
        {
            get { return _intProperty; }
            set { SetProperty(ref _intProperty, value); }
        }
        int _intProperty;

        public string StringProperty
        {
            get { return _stringProperty; }
            set { SetProperty(ref _stringProperty, value); }
        }
        string _stringProperty;

        public TestStructure StructProperty
        {
            get { return _structProperty; }
            set { SetProperty(ref _structProperty, value); }
        }
        TestStructure _structProperty;

        public TestClass ClassProperty
        {
            get { return _classProperty; }
            set { SetProperty(ref _classProperty, value); }
        }
        TestClass _classProperty;

        public DifferentProperties()
        {
            IntProperty = int.MinValue;
            StringProperty = "Test";
            StructProperty = new TestStructure { SomeValue = int.MinValue };
            ClassProperty = new TestClass { SomeValue = int.MinValue };
        }
    }
}
