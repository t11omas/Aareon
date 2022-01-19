using System.ComponentModel.DataAnnotations;

namespace AareonTechnicalTest.Contracts
{
    public class UpdateTicket
    {
        [Required]
        public string Content { get; set; }
    }
}