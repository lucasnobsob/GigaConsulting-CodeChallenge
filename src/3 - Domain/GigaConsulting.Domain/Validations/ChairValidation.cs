using FluentValidation;
using GigaConsulting.Domain.Commands;

namespace GigaConsulting.Domain.Validations
{
    public abstract class ChairValidation<T> : AbstractValidator<T> where T : ChairCommand
    {
        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("A cadeira deve ter um identificador válido");
        }

        protected void ValidateSerialNumber()
        {
            RuleFor(x => x.SerialNumber)
                .NotEmpty()
                .WithMessage("SerialNumber é obrigatório.")
                .MaximumLength(100);
        }

        protected void ValidateDescription() 
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description é obrigatória.")
                .MaximumLength(255);
        }
        protected void ValidateModel() 
        {
            RuleFor(x => x.Model)
                .NotEmpty()
                .WithMessage("Model é obrigatório.")
                .MaximumLength(100);
        }
        protected void ValidateStatus() 
        {
            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Status inválido.");
        }
        protected void ValidateType() 
        {
            RuleFor(x => x.ChairType)
                .IsInEnum()
                .WithMessage("Tipo de cadeira inválido.");
        }

    }
}
