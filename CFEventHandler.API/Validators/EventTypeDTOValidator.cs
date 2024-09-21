using CFEventHandler.Models.DTO;
using CFEventHandler.Utilities;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace CFEventHandler.API.Validators
{
    /// <summary>
    /// Validator for EventTypeDTO
    /// </summary>
    public class EventTypeDTOValidator : AbstractValidator<EventTypeDTO>
    {
        public EventTypeDTOValidator()
        {
            // Get validation attributes
            var nameMaxLength = AttributeUtilities.GetPropertyAttribute<MaxLengthAttribute>(typeof(EventTypeDTO), 
                        nameof(EventTypeDTO.Name))!.Length;

            RuleFor(x => x.Name).NotEmpty().NotNull()
                .WithMessage(ValidationMessageFormatter.PropertyMustBeSet("Name"))
                .MaximumLength(nameMaxLength)
                    .WithMessage(ValidationMessageFormatter.PropertyMustBeMaxLength("Name", nameMaxLength, "characters", true));
        }
    }
}
