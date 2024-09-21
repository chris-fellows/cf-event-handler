using CFEventHandler.Models.DTO;
using CFEventHandler.Utilities;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace CFEventHandler.API.Validators
{
    /// <summary>
    /// Validator for EventClientDTO
    /// </summary>
    public class EventClientDTOValidator : AbstractValidator<EventClientDTO>
    {
        public EventClientDTOValidator()
        {
            // Get validation attributes
            var nameMaxLength = AttributeUtilities.GetPropertyAttribute<MaxLengthAttribute>(typeof(EventClientDTO), 
                        nameof(EventClientDTO.Name))!.Length;

            RuleFor(x => x.Name).NotEmpty().NotNull()
                .WithMessage(ValidationMessageFormatter.PropertyMustBeSet("Name"))
                .MaximumLength(nameMaxLength)
                    .WithMessage(ValidationMessageFormatter.PropertyMustBeMaxLength("Name", nameMaxLength, "characters", true));
        }
    }
}
