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

        public static string PropertyMustBeSet(string propertyName) => $"{propertyName} must be set";

        public static string PropertyReadOnly(string propertyName) => $"{propertyName} is read only";        
    }
}
