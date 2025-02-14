namespace Data;

public class DbSettings : IDbSettings
{
    public string ConnectionString { get; set; } = string.Empty;
}

public interface IDbSettings
{
    string ConnectionString { get; set; }
}