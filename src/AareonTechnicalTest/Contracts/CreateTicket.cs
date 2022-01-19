using System.ComponentModel.DataAnnotations;

namespace AareonTechnicalTest.Contracts
{
    public record CreateTicket([Required] string Content);
}
