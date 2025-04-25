namespace Domain;
public class Method
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? ReturnType { get; set; }
    public bool? IsAbstract { get; set; }
    public bool? IsSealed { get; set; }
    public AccessModifier? Access { get; set; }
    public List<Parameter>? MethodParameters { get; set; }
    public List<LocalVariable>? MethodLocalVariables { get; set; }
    public List<Method>? InvokedMethods { get; set; }

    public Method()
    {
        Id = Guid.NewGuid();
    }
}
