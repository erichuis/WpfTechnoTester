using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Validators
{
    public abstract class PropertyValidateModel : IDataErrorInfo
    {
        // check for general model error    
        public string Error { get { return string.Empty; } }

        // check for property errors    
        public string this[string columnName]
        {
            get
            {
                if(columnName == null)
                {
                    return string.Empty;
                }
                var validationResults = new List<ValidationResult>();
                
                var validatedObject = GetType()?.GetProperty(columnName)?.GetValue(this);
                
                var validationContext = new ValidationContext(this){MemberName = columnName};

                if (Validator.TryValidateProperty(validatedObject, validationContext                        
                        , validationResults))
                    return string.Empty;

                return validationResults?.First()?.ErrorMessage?? string.Empty;
            }
        }
    }
}