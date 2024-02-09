//using Microsoft.AspNetCore.Identity;
//using DTOShared.Modules.User.Models;
//using System.ComponentModel.DataAnnotations;

//namespace DTOShared.Validation
//{
//    public class UniqueEmailAttribute : ValidationAttribute
//    {
//        private readonly UserManager<User> _userManager;
//        public UniqueEmailAttribute(UserManager<User> userManager)
//        {
//            _userManager = userManager;
//        }

//        protected  async Task<ValidationResult> IsValid(object value, ValidationContext validationContext)
//        {
//            var email = value as string;

//            User existingUser = await _userManager.FindByEmailAsync(email);

//            if (existingUser != null)
//            {
//                var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(existingUser);

//                if (!isEmailConfirmed)
//                {
//                    return new ValidationResult(GetErrorMessage());
//                }
//            }

//            return ValidationResult.Success;
//        }



//        public string GetErrorMessage()
//        {
//            return $"Email Da ton tai trong he thong";
//        }
//    }
//}
