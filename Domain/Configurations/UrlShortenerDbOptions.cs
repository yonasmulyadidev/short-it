namespace Domain.Configurations;

public class UrlShortenerDbOptions
{
    public string Host { get; set; }

    public int Port { get; set; }

    public string Database { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string ConnectionString => $"Host={Host}; Port={Port}; Database={Database}; Username={Username}; Password={Password}; SearchPath=dbo";
}