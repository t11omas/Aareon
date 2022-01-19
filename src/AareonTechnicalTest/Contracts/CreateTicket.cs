using System.ComponentModel.DataAnnotations;

namespace AareonTechnicalTest.Contracts
{
    public class CreateTicket
    {
        [Required]
        public string Content { get; set; }
    }
}
