using Autofac;
using Autofac.Core.Registration;
using NUnit.Framework;

namespace Shared.AutofacIntegration.Tests
{
    [TestFixture]
    public class InterfaceConventionModuleTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Container_With_Registration_Can_Resolve_ITest()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new InterfaceConventionModule(typeof(Test).Assembly));
            var sut = builder.Build();

            Assert.DoesNotThrow(() => sut.Resolve<ITest>());
        }

        [Test]
        public void Container_With_Registration_Cant_Resolve_ITest2()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new InterfaceConventionModule(typeof(Test).Assembly));
            var sut = builder.Build();

            Assert.Throws<ComponentNotRegisteredException>(() => sut.Resolve<ITest2>());
        }

        [Test]
        public void Container_With_Registration_Cant_Resolve_Test()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new InterfaceConventionModule(typeof(Test).Assembly));
            var sut = builder.Build();

            Assert.Throws<ComponentNotRegisteredException>(() => sut.Resolve<Test>());
        }

        [Test]
        public void Container_Without_Registration_Cant_Resolve()
        {
            var builder = new ContainerBuilder();
            var sut = builder.Build();
            Assert.Throws<ComponentNotRegisteredException>(() => sut.Resolve<ITest>());
        }
    }

    public interface ITest
    {
    }

    public interface ITest2
    {
    }

    public class Test : ITest, ITest2
    {
    }
}