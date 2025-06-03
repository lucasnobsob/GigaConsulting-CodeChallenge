using GigaConsulting.Domain.Models.Enums;
using GigaConsulting.Domain.Validations;

namespace GigaConsulting.Domain.Commands
{
    public class UpdateChairCommand : ChairCommand
    {
        public UpdateChairCommand(Guid id, string description, Status status)
        {
            Id = id;
            Description = description;
            Status = status;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateChairValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
