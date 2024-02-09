using DTOShared.Enums;
using System.ComponentModel.DataAnnotations;

namespace DTOShared.Validation
{
    public class CheckSexAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {




            if (value != null)
            {
                if ((int)value != (int)Sex.Female && (int)value != (int)Sex.Male)
                {
                    return new ValidationResult("Giới Tính Phải Là Nam Hoặc Nữ");
                }
            }

            return ValidationResult.Success;








        }
    }
}
