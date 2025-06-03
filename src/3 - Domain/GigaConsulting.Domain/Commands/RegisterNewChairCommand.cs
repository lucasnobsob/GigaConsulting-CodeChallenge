using GigaConsulting.Domain.Models.Enums;
using GigaConsulting.Domain.Validations;

namespace GigaConsulting.Domain.Commands
{
    public class RegisterNewChairCommand : ChairCommand
    {
        public RegisterNewChairCommand(Guid id, string serialNumber, string description, string model, Status status, ChairType chairType)
        {
            Id = id;
            SerialNumber = serialNumber;
            Description = description;
            Model = model;
            Status = status;
            ChairType = chairType;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewChairValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
