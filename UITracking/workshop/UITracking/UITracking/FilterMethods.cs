namespace UITracking;

public static class FilterMethods
{
    public static string GetForeignAddressFromLine(string cmdLine)
    {
        // works only for 'grep xray' filtered lines
        
        var parts = cmdLine.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        
        if (parts[3].Contains(":::")) 
        {
            return " ";
        }

        var addressPortPair = parts[4].Split(':');
        var address = addressPortPair[0];

        return address;
    }

    public static bool IsListeningOnAllPorts(string cmdLine)
    {
        return cmdLine.Contains("0.0.0.0") || 
               cmdLine.Contains(":::") ||
               cmdLine.Contains("*:*");
    }

    
    // add iptables filter methods...
}