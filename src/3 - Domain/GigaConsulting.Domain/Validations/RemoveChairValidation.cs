using GigaConsulting.Domain.Commands;

namespace GigaConsulting.Domain.Validations
{
    public class RemoveChairValidation : ChairValidation<RemoveChairCommand>
    {
        public RemoveChairValidation()
        {
            ValidateId();
        }
    }
}
