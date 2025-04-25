using Domain;
using FluentAssertions;

namespace TestDomain;

[TestClass]
public class ClassTest
{
    [TestMethod]
    public void CreateClassWithPropertiesSetTest()
    {
        var baseClass = new Class { Name = "Base" };
        var method = new Method { Name = "DoSomething" };
        var attribute = new Atribute { Name = "Field1" };

        var cls = new Class
        {
            Name = "MyClass",
            IsAbstract = true,
            IsSealed = false,
            BaseClass = baseClass,
            ClassMethods = [method],
            ClassAtributes = [attribute],
            Access = AccessModifier.PublicAccess
        };

        cls.Should().NotBeNull();
        cls.Id.Should().NotBe(Guid.Empty);
        cls.Name.Should().Be("MyClass");
        cls.IsAbstract.Should().BeTrue();
        cls.IsSealed.Should().BeFalse();
        cls.BaseClass.Should().Be(baseClass);
        cls.ClassMethods.Should().Contain(method);
        cls.ClassAtributes.Should().Contain(attribute);
        cls.Access.Should().Be(AccessModifier.PublicAccess);
    }
}
