using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirportsDemo.App.Models
{
    public class ValidationResult
    {
        public bool IsValid { get; private set; }
        public ValidationErrorType ErrorType { get; private set; }
        public string ErrorMessage { get; private set; }

        public static ValidationResult Success => new ValidationResult(ValidationErrorType.None);

        public ValidationResult(ValidationErrorType errorType, string errorMessage = null) {
            IsValid = errorType == ValidationErrorType.None;
            ErrorType = errorType;
            ErrorMessage = errorMessage;
        }
    }

    public enum ValidationErrorType
    {
        None,
        BadFormat,
        NotFound,
    }
}
