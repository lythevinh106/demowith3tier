using DTOShared.Enums;
using System.ComponentModel.DataAnnotations;

namespace DTOShared.Validation
{
    public class CheckListModule : ValidationAttribute
    {


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {




            if (value is string Module)
            {

                List<string> modules = CRUDTableTypes.AllTypes;

                if (!string.IsNullOrEmpty(Module))
                {

                    bool isValid = false;

                    isValid = modules.Any(m => m == Module.Trim());

                    return isValid == true ? ValidationResult.Success
                        : new ValidationResult("Kiểu Module Không Hợp Lệ");


                }


            }

            return ValidationResult.Success;







        }

    }


}

