using GigaConsulting.Domain.Commands;

namespace GigaConsulting.Domain.Validations
{
    public class RegisterNewAllocationValidation : AllocationValidation<RegisterNewAllocationCommand>
    {
        public RegisterNewAllocationValidation()
        {
            ValidateTimeRange();
            ValidateRoom();
            ValidateChair();
        }
    }
}
