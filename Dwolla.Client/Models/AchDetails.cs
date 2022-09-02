namespace Dwolla.Client.Models
{
    public class AchDetailsTrace
    {
        public string TraceId { get; set; }
    }
    
    public class AchDetails
    {
        public AchDetailsTrace Source { get; set; }
        public AchDetailsTrace Destination { get; set; }
    }
}
