using System.ComponentModel.DataAnnotations;

namespace AareonTechnicalTest.Contracts
{
    public record UpdateNote([Required] string Content);
}