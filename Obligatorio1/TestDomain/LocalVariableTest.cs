using Domain;
using FluentAssertions;

namespace TestDomain;

[TestClass]
public class LocalVariableTest
{
    [TestMethod]
    public void CreateLocalVariableWithPropertiesSetTest()
    {
        var localVar = new LocalVariable
        {
            Name = "counters",
            Type = DataType.IntegerType
        };

        localVar.Should().NotBeNull();
        localVar.Id.Should().NotBe(Guid.Empty);
        localVar.Name.Should().Be("counters");
        localVar.Type.Should().Be(DataType.IntegerType);
    }
}
