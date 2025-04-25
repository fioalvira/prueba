using Domain;
using FluentAssertions;

namespace TestDomain;

[TestClass]
public class ParameterTest
{
    [TestMethod]
    public void CreateParameterWithPropertiesSetTest()
    {
        var parameter = new Parameter
        {
            Name = "inputs",
            Access = AccessModifier.PublicAccess,
            DataType = DataType.StringType
        };

        parameter.Should().NotBeNull();
        parameter.Id.Should().NotBe(Guid.Empty);
        parameter.Name.Should().Be("inputs");
        parameter.Access.Should().Be(AccessModifier.PublicAccess);
        parameter.DataType.Should().Be(DataType.StringType);
    }
}
