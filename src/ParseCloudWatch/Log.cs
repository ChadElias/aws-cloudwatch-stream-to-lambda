using System.Collections.Generic;
public class Log
{
    public Log()
    {
        this.logEvents = new List<LogEvent>();
    }
    public string messageType { get; set; }
    public string owner { get; set; }
    public string logGroup { get; set; }
    public string logStream { get; set; }
    public List<string> subscriptionFilters { get; set; }
    public List<LogEvent> logEvents { get; set; }
}