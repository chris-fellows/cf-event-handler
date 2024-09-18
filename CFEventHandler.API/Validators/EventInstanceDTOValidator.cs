using CFEventHandler.Interfaces;
using CFEventHandler.Models.DTO;
using FluentValidation;

namespace CFEventHandler.API.Validators
{
    /// <summary>
    /// Validator for EventInstanceDTO
    /// </summary>
    public class EventInstanceDTOValidator : AbstractValidator<EventInstanceDTO>
    {        
        public EventInstanceDTOValidator(IEventClientService eventClientService,
                                        IEventTypeService eventTypeService)
        {
            RuleFor(x => x.EventClientId).NotEmpty().NotNull()
                .WithMessage(ValidationMessageFormatter.PropertyMustBeSet("Event Client Id"))
                .Must((x, _) =>
                {
                    return eventClientService.GetByIdAsync(x.EventClientId).Result != null;                    
                })
                .WithMessage(ValidationMessageFormatter.PropertyDoesNotReferToValidEntity("Event Client Id", "event client"));

            RuleFor(x => x.EventTypeId).NotEmpty().NotNull()
                .WithMessage(ValidationMessageFormatter.PropertyMustBeSet("Event Type Id"))
                .Must((x, _) =>
                {
                    return eventTypeService.GetByIdAsync(x.EventTypeId).Result != null;
                })
                .WithMessage(ValidationMessageFormatter.PropertyDoesNotReferToValidEntity("Event Type Id", "event type"));
        }
    }
}
