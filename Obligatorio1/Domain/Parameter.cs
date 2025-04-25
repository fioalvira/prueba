namespace Domain;
public class Parameter
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public AccessModifier? Access { get; set; }
    public DataType? DataType { get; set; }

    public Parameter()
    {
        Id = Guid.NewGuid();
    }
}
