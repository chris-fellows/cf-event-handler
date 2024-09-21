using CFEventHandler.Models.DTO;
using CFEventHandler.Utilities;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace CFEventHandler.API.Validators
{
    public class APIKeyInstanceDTOValidator : AbstractValidator<APIKeyInstanceDTO>
    {
        public APIKeyInstanceDTOValidator()
        {
            // Get validation attributes
            var keyMinLength = AttributeUtilities.GetPropertyAttribute<MinLengthAttribute>(typeof(APIKeyInstanceDTO), 
                        nameof(APIKeyInstanceDTO.Key))!.Length;
            var keyMaxLength = AttributeUtilities.GetPropertyAttribute<MaxLengthAttribute>(typeof(APIKeyInstanceDTO), 
                        nameof(APIKeyInstanceDTO.Key))!.Length;
            var nameMaxLength = AttributeUtilities.GetPropertyAttribute<MaxLengthAttribute>(typeof(APIKeyInstanceDTO), 
                        nameof(APIKeyInstanceDTO.Name))!.Length;

            RuleFor(x => x.Key).NotEmpty().NotNull()
               .WithMessage(ValidationMessageFormatter.PropertyMustBeSet("Key"))
               .Length(keyMinLength, keyMaxLength)
                    .WithMessage(ValidationMessageFormatter.PropertyMustBeLengthRange("Key", keyMinLength, keyMaxLength, "characters", true));
            
            RuleFor(x => x.Name).NotEmpty().NotNull()
                .WithMessage(ValidationMessageFormatter.PropertyMustBeSet("Name"))
                .MaximumLength(nameMaxLength)
                    .WithMessage(ValidationMessageFormatter.PropertyMustBeMaxLength("Name", nameMaxLength, "characters", true));

            RuleFor(x => x.Roles).NotEmpty().NotNull()
               .WithMessage(ValidationMessageFormatter.PropertyMustBeSet("Roles"));
        }
    }
}
