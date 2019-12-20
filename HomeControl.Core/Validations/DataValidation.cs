namespace HomeControl.Core.Validations
{
    public class DataValidation : IDataValidation
    {
        public DataValidation(string key, string message, ValidationType type)
        {
            Key = key;
            Message = message;
            Type = type;
        }

        public string Key { get; }

        public string Message { get; }

        public ValidationType Type { get; }
    }
}
