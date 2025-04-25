namespace Domain;
public class Type
{
    public Guid Id { get; set; }
    public DataType? TypeData { get; set; }

    public Type()
    {
        Id = Guid.NewGuid();
    }
}
