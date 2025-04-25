using DataAccess;
using Domain;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace TestDataAccess;

[TestClass]
public class RepositoryTests
{
    private ObjectSimulatorDbContext? _context;
    private Repository<LocalVariable>? _repository;
    private SqliteConnection? _connection;

    [TestInitialize]
    public void Setup()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<ObjectSimulatorDbContext>()
            .UseSqlite(_connection)
            .Options;

        _context = new ObjectSimulatorDbContext(options);
        _context.Database.EnsureCreated();

        _repository = new Repository<LocalVariable>(_context);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context?.Database.EnsureDeleted();
        _context?.Dispose();
        _connection?.Close();
    }

    [TestMethod]
    public void Add_ShouldAddEntityToDatabase()
    {
        var variable = new LocalVariable { Name = "var2", Type = DataType.StringType };
        var result = _repository!.Add(variable);

        result.Should().NotBeNull();
        _context!.LocalVariables.Count().Should().Be(1);
        result.Name.Should().Be("var2");
    }

    [TestMethod]
    public void Find_ShouldReturnEntityWhenExists()
    {
        var variable = new LocalVariable { Name = "existing", Type = DataType.IntegerType };
        _context!.LocalVariables.Add(variable);
        _context.SaveChanges();

        var result = _repository!.Find(v => v.Name == "existing");

        result.Should().NotBeNull();
        result!.Type.Should().Be(DataType.IntegerType);
    }

    [TestMethod]
    public void Find_ShouldReturnNullWhenEntityDoesNotExist()
    {
        var result = _repository!.Find(v => v.Name == "nonexistent");
        result.Should().BeNull();
    }

    [TestMethod]
    public void FindAll_ShouldReturnAllEntities()
    {
        _context!.LocalVariables.AddRange(
            new LocalVariable { Name = "v1", Type = DataType.StringType },
            new LocalVariable { Name = "v2", Type = DataType.IntegerType });
        _context.SaveChanges();

        var result = _repository!.FindAll();

        result.Should().HaveCount(2);
        result.Should().Contain(v => v.Name == "v1");
        result.Should().Contain(v => v.Name == "v2");
    }

    [TestMethod]
    public void Update_ShouldModifyEntity()
    {
        var variable = new LocalVariable { Name = "toUpdates", Type = DataType.StringType };
        _context!.LocalVariables.Add(variable);
        _context.SaveChanges();

        variable.Name = "updated";
        var result = _repository!.Update(variable);

        result.Should().NotBeNull();
        result.Name.Should().Be("updated");
    }

    [TestMethod]
    public void Delete_ShouldRemoveEntity()
    {
        var variable = new LocalVariable { Name = "toDelete", Type = DataType.IntegerType };
        _context!.LocalVariables.Add(variable);
        _context.SaveChanges();

        _repository!.Delete(variable.Id);

        _context.LocalVariables.Should().BeEmpty();
    }

    [TestMethod]
    public void Repository_ShouldAddMethodWithParameters()
    {
        var method = new Method
        {
            Name = "TestMethod",
            MethodParameters =
            [
            new() { Name = "param1", DataType = DataType.StringType }
            ]
        };

        var repo = new Repository<Method>(_context!);
        var result = repo.Add(method);

        result.Should().NotBeNull();
        result.MethodParameters.Should().HaveCount(1);
    }

    [TestMethod]
    public void Find_ShouldIncludeNavigationalProperties()
    {
        var classEntity = new Class
        {
            Name = "TestClass",
            ClassMethods =
            [
                new Method { Name = "M1" }
            ],
            ClassAtributes =
            [
                new Atribute { Name = "Attr1", DataType = DataType.StringType }
            ]
        };

        _context!.Classes.Add(classEntity);
        _context.SaveChanges();

        var repo = new Repository<Class>(_context);
        var result = repo.Find(c => c.Name == "TestClass");

        result.Should().NotBeNull();
        result!.ClassMethods.Should().HaveCount(1);
        result.ClassAtributes.Should().HaveCount(1);
    }

    [TestMethod]
    public void ObjectSimulatorDbContext_ShouldBuildModelWithClassRelationships()
    {
        var entity = _context!.Model.FindEntityType(typeof(Class));

        entity.Should().NotBeNull();
        entity!.GetNavigations().Should().ContainSingle(n => n.Name == "ClassMethods");
    }
}
