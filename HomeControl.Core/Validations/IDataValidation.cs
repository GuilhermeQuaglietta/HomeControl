namespace HomeControl.Core.Validations
{
    public interface IDataValidation
    {
        string Key { get; }

        string Message { get; }

        ValidationType Type { get; }
    }
}
