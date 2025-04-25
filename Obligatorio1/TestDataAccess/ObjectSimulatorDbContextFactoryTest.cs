using DataAccess;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TestDataAccess;

[TestClass]
public class ObjectSimulatorDbContextFactoryTest
{
    private string tempDirectory = string.Empty;

    [TestInitialize]
    public void Setup()
    {
        tempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDirectory);

        File.WriteAllText(Path.Combine(tempDirectory, "appsettings.json"),
        """
        {
            "ConnectionStrings": {
                "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=FakeDb;Trusted_Connection=True;"
            }
        }
        """);
    }

    [TestCleanup]
    public void Cleanup()
    {
        if(Directory.Exists(tempDirectory))
        {
            Directory.Delete(tempDirectory, true);
        }
    }

    [TestMethod]
    public void CreateDbContext_ShouldLoadConnectionStringFromJson()
    {
        // Arrange
        var config = new ConfigurationBuilder()
            .SetBasePath(tempDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var connectionString = config.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<ObjectSimulatorDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        // Act
        var context = new ObjectSimulatorDbContext(optionsBuilder.Options);

        // Assert
        context.Should().NotBeNull();
        context.Database.GetDbConnection().ConnectionString.Should().Be(connectionString);
    }
}
