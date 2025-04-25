using Domain;
using FluentAssertions;

namespace TestDomain;

[TestClass]
public class AtributeTest
{
    [TestMethod]
    public void CreateAtributeWithPropertiesSetTest()
    {
        var attribute = new Atribute
        {
            Name = "Hello",
            DataType = DataType.StringType,
            Access = AccessModifier.PrivateAccess
        };

        attribute.Should().NotBeNull();
        attribute.Id.Should().NotBe(Guid.Empty);
        attribute.Name.Should().Be("Hello");
        attribute.DataType.Should().Be(DataType.StringType);
        attribute.Access.Should().Be(AccessModifier.PrivateAccess);
    }
}
