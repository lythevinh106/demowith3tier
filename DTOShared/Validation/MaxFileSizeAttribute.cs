using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DTOShared.Validation
{


    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSizeMB;
        public MaxFileSizeAttribute(int maxFileSizeMB)
        {
            _maxFileSizeMB = maxFileSizeMB;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            ////Log.Error(file.Length.ToString());
            ////Log.Error((_maxFileSizeMB * 1024).ToString());
            if (file != null)
            {
                if (file.Length > _maxFileSizeMB * 1024 * 1024)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Kích thước file tối đa là {_maxFileSizeMB} MB.";
        }
    }
}
