namespace CFEventHandler.API.Validators
{
    /// <summary>
    /// Validation message formatter. Ensures that messages have a consistent format
    /// </summary>
    public static class ValidationMessageFormatter
    {
        public static string PropertyDoesNotReferToValidEntity(string propertyName, string entityType) => $"{propertyName} does not refer to a valid {entityType}";

        public static string PropertyInvalid(string propertyName) => $"{propertyName} is invalid";

        public static string PropertyMustBe(string propertyName, string values) => $"{propertyName} must be {values}";

        public static string PropertyMustBeGreaterThan(string propertyName, string value, bool includingValue) =>
                    includingValue ?
                        $"{propertyName} must be {value} or greater" :
                        $"{propertyName} must be greater than {value}";

        public static string PropertyMustBeLengthRange(string propertyName, int minLength, int maxLength, string lengthUnitType, bool includingValue) =>
             includingValue ?
                    $"{propertyName} must be between {minLength} and {maxLength} {lengthUnitType}" :
                    $"{propertyName} must be more than {minLength} and less than {maxLength} {lengthUnitType}";

        public static string PropertyMustBeLessThan(string propertyName, string value, bool includingValue) =>
                includingValue ?
                    $"{propertyName} must be {value} or less" :
                    $"{propertyName} must be less than {value}";

        public static string PropertyMustBeMaxLength(string propertyName, int maxLength, string lengthUnitType, bool includingValue) =>
             includingValue ?
                    $"{propertyName} must be {maxLength} {lengthUnitType} or less" :
                    $"{propertyName} must be less than {maxLength} {lengthUnitType}";

        //public static string PropertyMustBeMinLength(string propertyName, int minLength, string lengthUnitType, bool includingValue) =>
        //     includingValue ?
        //            $"{propertyName} must be {minLength} {lengthUnitType} or more" :
        //            $"{propertyName} must be more than {minLength} {lengthUnitType}";

        public static string PropertyMustBeSet(string propertyName) => $"{propertyName} must be set";

        public static string PropertyMustByUnique(string propertyName) => $"{propertyName} must be unique";

        public static string PropertyReadOnly(string propertyName) => $"{propertyName} is read only";        
    }
}
