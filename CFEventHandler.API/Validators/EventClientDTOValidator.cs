using CFEventHandler.Models.DTO;
using FluentValidation;

namespace CFEventHandler.API.Validators
{
    /// <summary>
    /// Validator for EventClientDTO
    /// </summary>
    public class EventClientDTOValidator : AbstractValidator<EventClientDTO>
    {
        public EventClientDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull()
                .WithMessage(ValidationMessageFormatter.PropertyMustBeSet("Name"));
        }
    }
}
