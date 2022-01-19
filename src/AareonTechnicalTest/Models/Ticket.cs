namespace AareonTechnicalTest.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int PersonId { get; set; }

        public Person Person { get; set; }
    }
}
