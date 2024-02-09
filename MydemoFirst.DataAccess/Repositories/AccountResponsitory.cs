using DTOShared.Modules.User;
using DTOShared.Modules.User.Models;
using DTOShared.Modules.User.Request;
using DTOShared.Modules.User.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MydemoFirst.DataAccess.Infrastructure;
using MydemoFirst.Models;
using MydemoFirst.Models.Modules.User.Models;
using MydemoFirst.Models.Modules.UserRefreshToken.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;

namespace MydemoFirst.DataAccess.Repositories
{
    public class AccountResponsitory : IAccountRepository
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly MyDemoFirstWith3TierContext _context;


        public AccountResponsitory(
            UserManager<User> userManager


            , SignInManager<User> signInManager,
            IConfiguration configuration,

            RoleManager<IdentityRole> roleManager,
            MyDemoFirstWith3TierContext context
          )
        {

            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;



        }



        public async Task<IdentityResult> SignUpAsync(RegisterUser registerUser)
        {
            var user = new User()
            {
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName,
                Email = registerUser.Email,
                UserName = registerUser.Email

            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);


            if (result.Succeeded)
            {
                var resultGenerate = await GenerateAuthListClaim(registerUser, user);

                if (!resultGenerate)
                {
                    return null;
                }



            };
            return result;

        }








        public async Task<string> SignInAsync(SingInUser singInUser)
        {



            var result = await _signInManager.PasswordSignInAsync(singInUser.Email,
                singInUser.Password, false, false);


            var user = await _userManager.FindByEmailAsync(singInUser.Email);

            if (!result.Succeeded)
            {
                return String.Empty;
            }


            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                (_configuration["JWT:Serect"])
            );
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(500),
                claims: await _userManager.GetClaimsAsync(user),



                signingCredentials: new SigningCredentials(

                    authenKey, SecurityAlgorithms.HmacSha512
                    )

              );


            return new JwtSecurityTokenHandler().WriteToken(token);

        }









        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[40];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        public async Task<bool> GenerateAuthListClaim(RegisterUser registerUser, User user)

        {
            var authClaims = new List<Claim> {

                 new Claim(ClaimTypes.Name,user.Email),

                new Claim("firstName",user.FirstName),
                new Claim("lastName",user.LastName),
                new Claim("sex",((int)registerUser.Sex).ToString()),
                new Claim("userId",user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),


            };

            try
            {
                foreach (var authclaim in authClaims)
                {
                    await _userManager.AddClaimAsync(user, authclaim);

                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("xay ra lỗi khi thêm claim");


            }

            return false;







        }



        public UserRefreshToken GenerateUserRefreshToken(string userId)
        {
            throw new NotImplementedException();
        }



        public async Task<bool> CheckAccountRegisted(string email)
        {

            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {


                bool userIsConfirmedMail = await _userManager.IsEmailConfirmedAsync(user);

                if (!userIsConfirmedMail)
                {

                    await _userManager.DeleteAsync(user);

                    return false;
                }
                return true;

            }


            return false;





        }

        public async Task<string> GenerateConfirmTokenEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            string TokenConfirmMail = await _userManager.GenerateEmailConfirmationTokenAsync(user);



            return TokenConfirmMail;

        }

        public async Task<bool> ConfirmEmail(string Email, string token)
        {
            var user = await _userManager.FindByEmailAsync(Email);

            if (user != null)
            {
                var resultConfirm = await _userManager.ConfirmEmailAsync(user, token);

                if (resultConfirm.Succeeded)
                {

                    return true;
                }
            }

            return false;



        }

        public async Task<RoleResponse> AddRole(RoleRequest roleRequest)
        {

            IdentityRole newRole = new IdentityRole(roleRequest.Name);
            var result = await _roleManager.CreateAsync(newRole);

            if (result.Succeeded)
            {
                return new RoleResponse
                {
                    Name = newRole.Name,
                    Id = newRole.Id


                };

            }

            return null;

        }







        public async Task<bool> CheckExistRole(string Namerole)
        {
            if (await _roleManager.RoleExistsAsync(Namerole)) return true;
            return false;

        }

        public async Task<bool> CheckUserInRole(string userName, string nameRole)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (await _userManager.IsInRoleAsync(user, nameRole))
                return true;

            return false;

        }


        public async Task<bool> CheckExistUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user != null) return true;

            return false;
        }

        public async Task<bool> AddUserInRole(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded) return true;
            return false;

        }

        public async Task<RoleClaims> AddOrUpdateRoleClaim(RoleClaims roleClaims)
        {



            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {


                RoleClaims result = null;

                try
                {
                    IdentityRole role = await _roleManager.FindByIdAsync(roleClaims.RoleId);

                    if (role != null)
                    {
                        var roleToDelete = await _roleManager.GetClaimsAsync(role);
                        if (roleClaims.Claims.Count <= 0)
                        {

                            foreach (var claim in roleToDelete)
                            {
                                await _roleManager.RemoveClaimAsync(role, claim);
                            }

                        }
                        else
                        {
                            foreach (var claim in roleToDelete)
                            {
                                await _roleManager.RemoveClaimAsync(role, claim);


                            }

                            // Thêm các RoleClaims mới
                            foreach (var claimRequest in roleClaims.Claims)
                            {
                                foreach (var permission in claimRequest.Permissions)
                                {
                                    var newClaim = new Claim(claimRequest.Module.ToString(), permission.ToString());
                                    await _roleManager.AddClaimAsync(role, newClaim);
                                }


                            }


                        }






                        var newClaims = await _roleManager.GetClaimsAsync(role);


                        scope.Complete();

                        var resultPending = newClaims.GroupBy(rcl => rcl.Type).ToList();

                        result = new RoleClaims
                        {
                            RoleId = roleClaims.RoleId,
                            Claims = resultPending.Select(rcl =>

                                 new ClaimsInRole
                                 {
                                     Module = rcl.Key,
                                     Permissions = rcl.Select(p => p.Value).ToList()

                                 }


                                ).ToList()

                        };


                    }
                }






                catch (Exception ex)
                {
                    // Xử lý lỗi nếu cần thiết
                    scope.Dispose(); // Gọi Dispose để rollback giao dịch


                    throw new Exception(ex.Message.ToString());
                }
                return result;


            }


        }
    }
}

