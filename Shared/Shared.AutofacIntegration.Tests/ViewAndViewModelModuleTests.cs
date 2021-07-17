using Autofac;
using Autofac.Core.Registration;
using NUnit.Framework;

namespace Shared.AutofacIntegration.Tests
{
    [TestFixture]
    public class ViewAndViewModelModuleTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Container_With_Registration_Can_Resolve_View_AndViewModel()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ViewAndViewModelModule(typeof(TestViewModel).Assembly));
            var sut = builder.Build();


            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => sut.Resolve<TestView>());
                Assert.DoesNotThrow(() => sut.Resolve<TestViewModel>());
            });
        }

        [Test]
        public void Container_With_Registration_Cant_Resolve_Misspelled_Classes()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ViewAndViewModelModule(typeof(TestView).Assembly));
            var sut = builder.Build();

            Assert.Multiple(() =>
            {
                Assert.Throws<ComponentNotRegisteredException>(() => sut.Resolve<TestViwModel>());
                Assert.Throws<ComponentNotRegisteredException>(() => sut.Resolve<TestMdel>());
            });
        }

        [Test]
        public void Container_Without_Registration_Cant_Resolve()
        {
            var builder = new ContainerBuilder();
            var sut = builder.Build();
            Assert.Multiple(() =>
            {
                Assert.Throws<ComponentNotRegisteredException>(() => sut.Resolve<TestViewModel>());
                Assert.Throws<ComponentNotRegisteredException>(() => sut.Resolve<TestView>());
            });
        }
    }

    public class TestView
    {
    }

    public class TestViewModel
    {
    }

    public class TestViwModel
    {
    }

    public class TestMdel
    {
    }
}