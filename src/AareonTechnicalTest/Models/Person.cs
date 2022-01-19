using System.Collections;
using System.Collections.Generic;

namespace AareonTechnicalTest.Models
{
    public class Person
    {
        public int Id { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public bool IsAdmin { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
