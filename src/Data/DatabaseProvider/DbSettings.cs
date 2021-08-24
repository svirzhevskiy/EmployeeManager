namespace Data.DatabaseProvider
{
    public class DbSettings : IDbSettings
    {
        public string ConnectionString { get; set; } = "";
    }

    public interface IDbSettings
    {
        string ConnectionString { get; set; }
    }
}