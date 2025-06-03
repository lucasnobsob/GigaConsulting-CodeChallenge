using GigaConsulting.Domain.Commands;

namespace GigaConsulting.Domain.Validations
{
    public class UpdateChairValidation : ChairValidation<UpdateChairCommand>
    {
        public UpdateChairValidation()
        {
            ValidateDescription();
            ValidateStatus();
        }
    }
}
