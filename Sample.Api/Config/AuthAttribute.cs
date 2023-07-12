namespace Sample.Api.Config;

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
