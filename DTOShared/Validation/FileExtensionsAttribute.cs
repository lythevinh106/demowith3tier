using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTOShared.Validation
{

    public class FileExtensionAttribute : ValidationAttribute
    {
        public string[] _arrayExtensions;

        // Constructor que recibe un parámetro arrayExtensions de tipo Array<string>
        public FileExtensionAttribute(string[] arrayExtensions)
        {
            _arrayExtensions = arrayExtensions;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName); //123.jpg



                bool result = _arrayExtensions.Any(x => extension.EndsWith(x));

                if (!result)
                {

                    StringBuilder sb = new StringBuilder();

                    foreach (var item in _arrayExtensions)
                    {
                        sb.Append(item + ",");
                    }

                    return new ValidationResult("Allowed extensions are " + sb);
                }
            }
            return ValidationResult.Success;

        }
    }



}
