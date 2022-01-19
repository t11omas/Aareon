using System.ComponentModel.DataAnnotations;

namespace AareonTechnicalTest.Contracts
{
    public class UpdateNote
    {
        [Required]
        public string Content { get; set; }
    }
}