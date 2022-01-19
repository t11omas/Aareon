using System.ComponentModel.DataAnnotations;

namespace AareonTechnicalTest.Contracts
{
    public record UpdateTicket([Required] string Content);
}