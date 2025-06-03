using GigaConsulting.Domain.Validations;

namespace GigaConsulting.Domain.Commands
{
    public class RemoveChairCommand : ChairCommand
    {
        public RemoveChairCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveChairValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
