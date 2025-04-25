namespace Domain;
public class Atribute
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DataType? DataType { get; set; }
    public AccessModifier? Access { get; set; }

    public Atribute()
    {
        Id = Guid.NewGuid();
    }
}
