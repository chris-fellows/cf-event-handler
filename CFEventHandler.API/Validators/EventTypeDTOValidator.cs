using CFEventHandler.Models.DTO;
using FluentValidation;

namespace CFEventHandler.API.Validators
{
    /// <summary>
    /// Validator for EventTypeDTO
    /// </summary>
    public class EventTypeDTOValidator : AbstractValidator<EventTypeDTO>
    {
        public EventTypeDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull()
                .WithMessage(ValidationMessageFormatter.PropertyMustBeSet("Name"));
        }
    }
}
