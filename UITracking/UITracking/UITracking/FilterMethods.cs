namespace UITracking;

public static class FilterMethods
{
    public static string GetForeignAddressFromLine(string line)
    {
        return (line.Split(" ", StringSplitOptions.TrimEntries)[4]).Split(':')[0]; // works only for 'grep xray' filtered lines
    }
    
    // add iptables filter methods...
}