using GigaConsulting.Domain.Models;
using GigaConsulting.Domain.Validations;

namespace GigaConsulting.Domain.Commands
{
    public class RegisterNewAllocationCommand : AllocationCommand
    {
        public RegisterNewAllocationCommand(DateTime from, DateTime to, Guid roomId, Guid chairId)
        {
            From = from;
            To = to;
            RoomId = roomId;
            ChairId = chairId;
        }
        public override bool IsValid()
        {
            ValidationResult = new RegisterNewAllocationValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
