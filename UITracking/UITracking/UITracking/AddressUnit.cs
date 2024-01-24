namespace UITracking;

public struct ForeignAddressUnit
{
    public string IP { get; }
    public bool IsBanned = false;

    public ForeignAddressUnit(string ip)
    {
        IP = ip;
    }
}