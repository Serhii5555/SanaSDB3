namespace SanaSDB3
{
    public class Tasks
    {
        public int Id { get; set; }
        public bool Completed { get; set; }
        public string Name { get; set; }
        public DateTime? DueDate { get; set; }

        public int CategoryId { get; set; }
        public Categories Category { get; set; }

    }
}
