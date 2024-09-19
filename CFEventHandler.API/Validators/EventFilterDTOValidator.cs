using CFEventHandler.Interfaces;
using CFEventHandler.Models.DTO;
using FluentValidation;

namespace CFEventHandler.API.Validators
{
    /// <summary>
    /// Validator for EventFilterDTO
    /// </summary>
    public class EventFilterDTOValidator : AbstractValidator<EventFilterDTO>
    {
        public EventFilterDTOValidator(IEventClientService eventClientService,
                                        IEventTypeService eventTypeService)
        {
            RuleFor(x => x.PageItems).GreaterThanOrEqualTo(1)
                    .WithMessage(ValidationMessageFormatter.PropertyMustBeGreaterThan("Page Items", "1", true))
                .LessThanOrEqualTo(10000)
                    .WithMessage(ValidationMessageFormatter.PropertyMustBeLessThan("Page Items", "10000", true));  // TODO: Config setting?

            RuleFor(x => x.PageNo).GreaterThanOrEqualTo(1)
                .WithMessage(ValidationMessageFormatter.PropertyMustBeGreaterThan("Page No", "1", true));

            // Check EventClientIds either not set or refer to valid Ids
            RuleFor(x => x.EventClientIds)                
                .Must((x, _) =>
                {
                    // TODO: Consider using caching
                    if (x.EventClientIds == null || !x.EventClientIds.Any()) return true;
                    var eventClientIds = eventClientService.GetAll().Select(ec => ec.Id).ToList();
                    return !x.EventClientIds.Except(eventClientIds).Any();                    
                })
                .WithMessage(ValidationMessageFormatter.PropertyDoesNotReferToValidEntity("Event Client Ids", "event client"));

            // Check EventTypeIds either not set or refer to valid Ids
            RuleFor(x => x.EventTypeIds)
                .Must((x, _) =>
                {
                    // TODO: Consider using caching
                    if (x.EventTypeIds == null || !x.EventTypeIds.Any()) return true;
                    var eventTypeIds = eventTypeService.GetAll().Select(et => et.Id).ToList();
                    return !x.EventTypeIds.Except(eventTypeIds).Any();
                })
                .WithMessage(ValidationMessageFormatter.PropertyDoesNotReferToValidEntity("Event Type Ids", "event type"));
        }
    }
}
