namespace Domain;
public class LocalVariable
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DataType? Type { get; set; }

    public LocalVariable()
    {
        Id = Guid.NewGuid();
    }
}
