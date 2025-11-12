namespace MuniApp.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime DateReported { get; set; } = DateTime.Now;
        public int Priority { get; set; } 

    }
}
