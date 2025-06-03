using FluentValidation;
using GigaConsulting.Domain.Commands;

namespace GigaConsulting.Domain.Validations
{
    public abstract class AllocationValidation<T> : AbstractValidator<T> where T : AllocationCommand
    {
        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("A alocação deve ter um identificador válido");
        }

        protected void ValidateTimeRange()
        {
            RuleFor(x => x.From)
                .LessThan(x => x.To)
                .WithMessage("'From' deve ser anterior a 'To'.");

            RuleFor(x => x.To)
                .GreaterThan(x => x.From)
                .WithMessage("'To' deve ser posterior a 'From'.");
        }

        protected void ValidateRoom()
        {
            RuleFor(x => x.RoomId)
                .NotEqual(Guid.Empty)
                .WithMessage("Room não pode ser nulo.");
        }

        protected void ValidateChair()
        {
            RuleFor(x => x.ChairId)
                .NotEqual(Guid.Empty)
                .WithMessage("Chair não pode ser nulo.");
        }
    }
}
