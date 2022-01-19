using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AareonTechnicalTest.Models
{
    public class Ticket
    {
        public int Id { get; }

        public string Content { get; set; }

        public int PersonId { get; set; }

        public Person Person { get; set; }

        public ICollection<Note> Notes { get; set; }
    }
}
