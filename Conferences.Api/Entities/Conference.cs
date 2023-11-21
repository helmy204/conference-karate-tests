namespace Conferences.Api.Entities
{
    public class Conference
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Overview { get; set; }
        public string Location { get; set; }

        public int UserId { get; set; } 
    }
}