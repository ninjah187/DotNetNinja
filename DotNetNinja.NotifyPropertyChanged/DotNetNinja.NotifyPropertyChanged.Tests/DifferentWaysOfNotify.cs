using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DotNetNinja.NotifyPropertyChanged.Tests
{
    public class DifferentWaysOfNotify
    {
        [Fact]
        public void ImplicitNotifyWhenDifferentValues()
        {
            var obj = new ImplicitNotify();
            var result = false;
            var newValue = 1;
            obj.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(obj.Property) && obj.Property == newValue)
                {
                    result = true;
                }
            };

            obj.Property = newValue;

            Assert.True(result);
        }

        [Fact]
        public void ImplicitDontNotifyWhenTheSameValues()
        {
            var obj = new ImplicitNotify();
            var result = false;
            var newValue = obj.Property;
            obj.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(obj.Property) && obj.Property == newValue)
                {
                    result = true;
                }
            };

            obj.Property = newValue;

            Assert.False(result);
        }

        [Fact]
        public void SelectorNotifyWhenDifferentValues()
        {
            var obj = new SelectorNotify();
            var result = false;
            var newValue = 1;
            obj.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(obj.Property) && obj.Property == newValue)
                {
                    result = true;
                }
            };

            obj.Property = newValue;

            Assert.True(result);
        }

        [Fact]
        public void SelectorDontNotifyWhenTheSameValues()
        {
            var obj = new SelectorNotify();
            var result = false;
            var newValue = obj.Property;
            obj.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(obj.Property) && obj.Property == newValue)
                {
                    result = true;
                }
            };

            obj.Property = newValue;

            Assert.False(result);
        }

        [Fact]
        public void NameofNotifyWhenDifferentValues()
        {
            var obj = new NameofNotify();
            var result = false;
            var newValue = 1;
            obj.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(obj.Property) && obj.Property == newValue)
                {
                    result = true;
                }
            };

            obj.Property = newValue;

            Assert.True(result);
        }

        [Fact]
        public void NameofDontNotifyWhenTheSameValues()
        {
            var obj = new SelectorNotify();
            var result = false;
            var newValue = obj.Property;
            obj.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(obj.Property) && obj.Property == newValue)
                {
                    result = true;
                }
            };

            obj.Property = newValue;

            Assert.False(result);
        }
    }
}
