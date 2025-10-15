namespace MuniApp.Models
{
    public class Update
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? EventDate { get; set; } 
    }
}
