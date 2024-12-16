namespace WebApi.Helpers
{
    public class ValidationHelper
    {
        public static void CheckForNull(object value, string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName, $"{parameterName} cannot be null.");
            }
        }
        public static void CheckForNullOrEmpty(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"{parameterName} cannot be null or empty.", parameterName);
            }
        }
        public static void CheckForValidId(int id, string parameterName)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(parameterName, $"{parameterName} must be a positive integer.");
            }
        }
    }
}
