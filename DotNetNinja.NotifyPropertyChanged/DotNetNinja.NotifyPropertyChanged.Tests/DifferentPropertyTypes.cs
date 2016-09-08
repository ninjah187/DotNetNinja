using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Xunit;

namespace DotNetNinja.NotifyPropertyChanged.Tests
{
    public class DifferentPropertyTypes
    {
        [Fact]
        public void NotifySimpleTypeProperty()
        {
            var obj = new DifferentProperties();
            var result = false;
            var newValue = 1;
            obj.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(obj.IntProperty) && obj.IntProperty == newValue)
                {
                    result = true;
                }
            };

            obj.IntProperty = newValue;

            Assert.True(result);
        }

        [Fact]
        public void DontNotifySimpleTypePropertyWhenTheSame()
        {
            var obj = new DifferentProperties();
            var result = false;
            var newValue = obj.IntProperty;
            obj.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(obj.IntProperty) && obj.IntProperty == newValue)
                {
                    result = true;
                }
            };

            obj.IntProperty = newValue;

            Assert.False(result);
        }

        [Fact]
        public void NotifyStringProperty()
        {
            var obj = new DifferentProperties();
            var result = false;
            var newValue = new StringBuilder()
                .Append("Test value")
                .ToString();
            obj.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(obj.StringProperty) && obj.StringProperty == newValue)
                {
                    result = true;
                }
            };

            obj.StringProperty = newValue;

            Assert.True(result);
        }

        [Fact]
        public void DontNotifyStringPropertyWhenTheSame()
        {
            var obj = new DifferentProperties();
            var result = false;
            var newValue = new StringBuilder()
                .Append("Test")
                .ToString();
            obj.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(obj.StringProperty) && obj.StringProperty == newValue)
                {
                    result = true;
                }
            };

            obj.StringProperty = newValue;

            Assert.False(result);
        }

        [Fact]
        public void NotifyValueTypeStructureProperty()
        {
            var obj = new DifferentProperties();
            var result = false;
            var newValue = new TestStructure { SomeValue = int.MaxValue };
            obj.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(obj.StructProperty) && obj.StructProperty.Equals(newValue))
                {
                    result = true;
                }
            };

            obj.StructProperty = newValue;

            Assert.True(result);
        }

        [Fact]
        public void DontNotifyValueTypeStructurePropertyWhenTheSame()
        {
            var obj = new DifferentProperties();
            var result = false;
            var newValue = new TestStructure { SomeValue = int.MinValue };
            obj.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(obj.StructProperty) && obj.StructProperty.Equals(newValue))
                {
                    result = true;
                }
            };

            obj.StructProperty = newValue;

            Assert.False(result);
        }

        [Fact]
        public void NotifyReferenceTypeClassProperty()
        {
            var obj = new DifferentProperties();
            var result = false;
            var newValue = new TestClass { SomeValue = int.MinValue };
            obj.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(obj.ClassProperty) && obj.ClassProperty == newValue)
                {
                    result = true;
                }
            };

            obj.ClassProperty = newValue;

            Assert.True(result);
        }

        [Fact]
        public void DontNotifyReferenceTypeClassPropertyWhenTheSame()
        {
            var obj = new DifferentProperties();
            var result = false;
            var newValue = obj.ClassProperty;
            obj.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(obj.ClassProperty) && obj.ClassProperty == newValue)
                {
                    result = true;
                }
            };

            obj.ClassProperty = newValue;

            Assert.False(result);
        }
    }
}
