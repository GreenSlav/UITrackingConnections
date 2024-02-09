namespace UITracking.Models;

public class ForeignAddress
{
    public string Host { get; }
    public bool IsBanned { get; set; }

    public ForeignAddress(string host)
    {
        Host = host;
    }
}