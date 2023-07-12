namespace Sample.Api.Config;

[AttributeUsage(AttributeTargets.All)]
public class AuthAttribute : Attribute
{
    public string[]? Groups { get; }

    public AuthAttribute(params string[]? groups)
    {
        Groups = groups;
    }

    public AuthAttribute()
    {
    }
}
