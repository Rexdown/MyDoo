namespace MyDoo.Entities;

public class BusOptions
{
    public static string SectionName = "bus";
    
    public string HostName { get; set; }

    public string UserName { get; set; }
    
    public string Password { get; set; }

    public int Port { get; set; }
}