using System.ComponentModel.DataAnnotations;

namespace AareonTechnicalTest.Contracts
{
    public class CreateNote
    {
        [Required]
        public string Content { get; set; }
    }
}