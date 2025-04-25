namespace Domain;

public class Class : Type
{
    public string? Name { get; set; }
    public bool? IsAbstract { get; set; }
    public bool? IsSealed { get; set; }
    public Class? BaseClass { get; set; }
    public List<Method>? ClassMethods { get; set; }
    public List<Atribute>? ClassAtributes { get; set; }
    public AccessModifier? Access { get; set; }

    // empy constructor so that can be handled by logic
    public Class()
    {
        Id = Guid.NewGuid();
    }
}
