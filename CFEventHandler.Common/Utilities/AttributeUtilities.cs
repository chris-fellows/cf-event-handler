namespace CFEventHandler.Utilities
{
    /// <summary>
    /// Attribute utilities
    /// </summary>
    public static class AttributeUtilities
    {       
        /// <summary>
        /// Gets named property attribute from type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">Type with property</param>
        /// <param name="propertyName">Property name</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static T? GetPropertyAttribute<T>(Type type, string propertyName) where T : Attribute
        {
            var propertyInfo = type.GetProperty(propertyName);
            if (propertyInfo == null)
            {
                throw new ArgumentException($"Type {type.Name} does not have property {propertyName}");
            }

            var attribute = Attribute.GetCustomAttribute(propertyInfo, typeof(T));
            if (attribute != null)
            {
                return (T)attribute;
            }
            return default(T);
        }     
    }
}
