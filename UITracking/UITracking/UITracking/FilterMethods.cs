namespace UITracking;

public static class FilterMethods
{
    public static string GetForeignAddressFromLine(string line)
    {
        return line.Split()[4]; // works only for 'grep xray' filtered lines
    }
    
    
}