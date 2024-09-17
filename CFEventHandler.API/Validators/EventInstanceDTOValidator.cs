using CFEventHandler.Models.DTO;
using FluentValidation;

namespace CFEventHandler.API.Validators
{
    /// <summary>
    /// Validator for EventInstanceDTO
    /// </summary>
    public class EventInstanceDTOValidator : AbstractValidator<EventInstanceDTO>
    {
        public EventInstanceDTOValidator()
        {
            RuleFor(x => x.EventClientId).NotEmpty().NotNull()
                .WithMessage(ValidationMessageFormatter.PropertyMustBeSet("Event Client Id"));

            RuleFor(x => x.EventTypeId).NotEmpty().NotNull()
                .WithMessage(ValidationMessageFormatter.PropertyMustBeSet("Event Type Id"));
        }
    }
}
