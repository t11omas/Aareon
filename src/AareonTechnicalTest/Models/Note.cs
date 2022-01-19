namespace AareonTechnicalTest.Models
{
    public class Note
    {
        public int Id { get; }

        public string Content { get; set; }

        public int TicketId { get; set; }

        public Ticket Ticket { get; set; }

        public int PersonId { get; set; }

        public Person Person { get; set; }
    }
}