using Domain;
using FluentAssertions;

namespace TestDomain;

[TestClass]
public class MethodTest
{
    [TestMethod]
    public void CreateMethodWithPropertiesSetTest()
    {
        var parameter = new Parameter { Name = "value" };
        var localVariable = new LocalVariable { Name = "temp" };
        var invokedMethod = new Method { Name = "Log" };

        var method = new Method
        {
            Name = "Calculate",
            ReturnType = "int",
            IsAbstract = false,
            IsSealed = true,
            Access = AccessModifier.ProtectedAccess,
            MethodParameters = [parameter],
            MethodLocalVariables = [localVariable],
            InvokedMethods = [invokedMethod]
        };

        method.Should().NotBeNull();
        method.Id.Should().NotBe(Guid.Empty);
        method.Name.Should().Be("Calculate");
        method.ReturnType.Should().Be("int");
        method.IsAbstract.Should().BeFalse();
        method.IsSealed.Should().BeTrue();
        method.Access.Should().Be(AccessModifier.ProtectedAccess);
        method.MethodParameters.Should().Contain(parameter);
        method.MethodLocalVariables.Should().Contain(localVariable);
        method.InvokedMethods.Should().Contain(invokedMethod);
    }
}
