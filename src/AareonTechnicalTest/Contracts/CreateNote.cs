using System.ComponentModel.DataAnnotations;

namespace AareonTechnicalTest.Contracts
{
    public record CreateNote([Required] string Content);
}