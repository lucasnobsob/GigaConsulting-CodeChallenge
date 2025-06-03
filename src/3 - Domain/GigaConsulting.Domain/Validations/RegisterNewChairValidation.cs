using GigaConsulting.Domain.Commands;

namespace GigaConsulting.Domain.Validations
{
    public class RegisterNewChairValidation : ChairValidation<RegisterNewChairCommand>
    {
        public RegisterNewChairValidation()
        {
            ValidateSerialNumber();
            ValidateDescription();
            ValidateModel();
            ValidateStatus();
            ValidateType();
        }
    }
}
