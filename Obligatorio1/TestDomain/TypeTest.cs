using Domain;
using FluentAssertions;

namespace TestDomain;

[TestClass]
public class TypeTest
{
    [TestMethod]
    public void CreateTypeWithPropertiesSetTest()
    {
        var type = new Domain.Type
        {
            TypeData = DataType.IntegerType
        };

        type.Should().NotBeNull();
        type.Id.Should().NotBe(Guid.Empty);
        type.TypeData.Should().Be(DataType.IntegerType);
    }
}
