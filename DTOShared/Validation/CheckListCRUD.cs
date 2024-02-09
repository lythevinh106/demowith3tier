using DTOShared.Enums;
using System.ComponentModel.DataAnnotations;

namespace DTOShared.Validation
{
    public class CheckListCRUD : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {



            if (value is List<string> permissions)
            {




                List<string> enumStringValues = Enum.GetNames(typeof(AppRoleClaim)).ToList(); ;

                if (permissions != null && permissions.Count > 0)
                {

                    bool isValid = false;

                    isValid = permissions.All(p => enumStringValues.Any(e => e == p));

                    return isValid == true ? ValidationResult.Success
                        : new ValidationResult("Kiểu Permission Không Hợp Lệ");


                }


            }

            return ValidationResult.Success;










        }
    }

}

