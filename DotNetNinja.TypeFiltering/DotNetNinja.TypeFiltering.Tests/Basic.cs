using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using DotNetNinja.TypeFiltering;

namespace DotNetNinja.TypeFiltering.Tests
{
    public class Basic
    {
        [Fact]
        public void RecognizeSimpleClassSingleCheck()
        {
            var obj = new SimpleClass();

            var recognized = false;

            obj.When<SimpleClass>(o => recognized = true);

            Assert.True(recognized);
        }

        [Fact]
        public void RecognizeSimpleClassMultipleChecks()
        {
            var obj = new SimpleClass();

            var isNeverImplementedClass = false;
            var isSimpleClass = false;

            obj
                .When<NeverImplementedClass>(o => isNeverImplementedClass = true)
                .When<SimpleClass>(o => isSimpleClass = true);

            Assert.False(isNeverImplementedClass);
            Assert.True(isSimpleClass);
        }

        [Fact]
        public void ErrorIfSimpleClassNotRecognized()
        {
            var obj = new SimpleClass();

            Action testCode = () =>
            {
                obj
                    .When<SimpleOtherClass>(o => { })
                    .ThrowIfNotRecognized();
            };

            Assert.Throws<InvalidOperationException>(testCode);
        }

        [Fact]
        public void HitDefaultIfNotRecognized()
        {
            var obj = new SimpleClass();

            var defaultFired = false;

            obj
                .When<SimpleOtherClass>(o => { })
                .Default(() => defaultFired = true);

            Assert.True(defaultFired);
        }

        [Fact]
        public void DontHitDefaultIfRecognized()
        {
            var obj = new SimpleClass();

            var isSimpleClass = false;
            var defaultFired = false;

            obj
                .When<SimpleClass>(o => isSimpleClass = true)
                .Default(() => defaultFired = true);

            Assert.True(isSimpleClass);
            Assert.False(defaultFired);
        }

        [Fact]
        public void RecognizeInterface()
        {
            var obj = new SimpleImplementation();

            var isInterface = false;
            var isClass = false;

            obj
                .When<ISimpleAbstraction>(o => isInterface = true)
                .When<SimpleImplementation>(o => isClass = true);

            Assert.True(isInterface);
            Assert.True(isClass);
        }

        [Fact]
        public void BreakIfRecognized()
        {
            var obj = new SimpleImplementation();

            var isSimpleAbstraction = false;
            var isSimpleImplementation = false;

            obj
                .When<ISimpleAbstraction>(o => isSimpleAbstraction = true)
                .BreakIfRecognized()
                .When<SimpleImplementation>(o => isSimpleImplementation = true);

            Assert.True(isSimpleAbstraction);
            Assert.False(isSimpleImplementation);
        }

        [Fact]
        public void DontBreakIfNotRecognized()
        {
            var obj = new SimpleClass();

            var isOtherClass = false;
            var isClass = false;

            obj
                .When<SimpleOtherClass>(o => isOtherClass = true)
                .BreakIfRecognized()
                .When<SimpleClass>(o => isClass = true);

            Assert.False(isOtherClass);
            Assert.True(isClass);
        }

        [Fact]
        public void FilteringBreak()
        {
            var obj = new SimpleImplementation();

            var isSimpleAbstraction = false;
            var isSimpleImplementation = false;

            obj
                .When<ISimpleAbstraction>((o, filtering) =>
                {
                    isSimpleAbstraction = true;
                    filtering.Break();
                })
                .When<SimpleImplementation>(o => isSimpleImplementation = true);

            Assert.True(isSimpleAbstraction);
            Assert.False(isSimpleImplementation);
        }

        [Fact]
        public void DerivedRecognized()
        {
            BaseClass obj = new DerivingClass();
            var result = false;

            obj.When<DerivingClass>(o => result = true);

            Assert.True(result);
        }

        [Fact]
        public void DerivedNotRecognized()
        {
            BaseClass obj = new BaseClass();
            var result = false;

            obj.When<DerivingClass>(o => result = true);

            Assert.False(result);
        }

        [Fact]
        public void BaseTypeRecognized()
        {
            var obj = new DerivingClass();
            var result = false;

            obj.When<BaseClass>(o => result = true);

            Assert.True(result);
        }
    }
}
