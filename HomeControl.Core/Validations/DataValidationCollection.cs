using System.Collections.Generic;
using System.Linq;

namespace HomeControl.Core.Validations
{
    public class DataValidationCollection
    {
        private readonly List<IDataValidation> _validationErrors;

        public IEnumerable<IDataValidation> Validations
        {
            get
            {
                return _validationErrors.AsReadOnly();
            }
        }
        public IEnumerable<IDataValidation> Errors
        {
            get
            {
                return _validationErrors.Where(x => x.Type == ValidationType.Error);
            }
        }

        public DataValidationCollection()
        {
            _validationErrors = new List<IDataValidation>();
        }

        public void Add(string key, string message, ValidationType validation)
        {
            _validationErrors.Add(new DataValidation(key, message, validation));
        }
        public void AddError(string key, string message)
        {
            _validationErrors.Add(new DataValidation(key, message, ValidationType.Error));
        }
    }
}
