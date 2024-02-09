using AutoMapper;
using Castle.Core.Internal;
using DTOShared.Modules.User;
using DTOShared.Modules.User.Models;
using DTOShared.Modules.User.Request;
using DTOShared.Modules.User.Response;
using Google.Apis.Auth.OAuth2;
using Google.Apis.PeopleService.v1;
using Google.Apis.PeopleService.v1.Data;
using Google.Apis.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MydemoFirst.DataAccess.Infrastructure;
using MydemoFirst.Errors.Exceptions;
using MydemoFirst.Services.Notification;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MydemoFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : GenericBaseController
    {
        private readonly IAccountRepository _accountRepository;

        public AuthenticationController(IMediator mediator, IMapper mapper, IAccountRepository accountRepository)
            : base(mediator, mapper)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet("ConfirmMailToken")]
        public async Task<IActionResult> ConfirmMailToken([FromQuery] string token, string email)

        {
            var result = await _accountRepository.ConfirmEmail(email, token);

            if (result)
            {
                return Ok("Da Xac Nhan Email Thanh Cong-Hay qua ve trang cua chung toi de dang nhap");
            }

            return NotFound();
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SingUp([FromBody] RegisterUser registerUser)

        {
            var checkExist = await _accountRepository.CheckAccountRegisted(registerUser.Email);

            if (checkExist)
            {
                // ModelState.AddModelError("Email", "Email Da ton tai trong he thong");
                return BadRequest("Email Da ton tai trong he thong");
            }

            var result = await _accountRepository.SignUpAsync(registerUser);

            if (result.Succeeded)
            {
                string token = await _accountRepository.GenerateConfirmTokenEmail(registerUser.Email);

                var link = Url.Action(nameof(ConfirmMailToken), "Authentication", new { token = token, email = registerUser.Email }, Request.Scheme);
                Log.Information(token);

                //send mail
                _imediator.Publish(new SendMailNotification("day la test mail with Html", $"<a href={link} >Nhan vao day de xac thuc email cua ban</a>", registerUser.Email));

                //  string jobId = BackgroundJob.Enqueue<IMail>(x => x.SendMailAsync("day la test mail with Html", , ));

                return Ok("Dang Ki Thanh Cong Hãy Tiến Hành xác nhận Email Của Bạn");
            }

            return BadRequest();
        }

        [HttpPost("SingIn")]
        public async Task<IActionResult> SingIn([FromBody] SingInUser singInUser)

        {
            var result = await _accountRepository.SignInAsync(singInUser);

            if (result.IsNullOrEmpty()) throw new ExceptionUnauthorized("Sai ten Tai khoan or mat khau");

            return Ok(new SingInResponse
            {
                jwtToken = result,
            });
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole([FromBody] RoleRequest roleRequest)
        {
            if (await _accountRepository.CheckExistRole(roleRequest.Name))
            {
                throw new ExceptionBadRequest("Role Da tòn tai");
            }

            RoleResponse newRole = await _accountRepository.AddRole(roleRequest);

            if (newRole != null)
            {
                return Ok(newRole);
            }

            return NotFound();
        }

        [HttpPost("AddUserInRole")]
        [Authorize]
        public async Task<IActionResult> AddUserInRole([FromBody] UserInRoleRequest userInRoleRequest)
        {
            if (!await _accountRepository.CheckExistUser(userInRoleRequest.UserName))
            {
                throw new ExceptionBadRequest("User Không Tòn tai");
            }

            if (!await _accountRepository.CheckExistRole(userInRoleRequest.RoleName))
            {
                throw new ExceptionBadRequest("Role Khoong Tòn tai");
            }

            if (await _accountRepository.CheckUserInRole(userInRoleRequest.UserName, userInRoleRequest.RoleName))
            {
                throw new ExceptionBadRequest("User da có role này");
            }

            bool result = await _accountRepository.AddUserInRole(userInRoleRequest.UserName, userInRoleRequest.RoleName);

            if (result) { return Ok("Thêm Thành Công"); }

            return BadRequest();
        }

        [HttpPost("AddRoleClaim")]
        public async Task<IActionResult> AddRoleClaim([FromBody] RoleClaims roleClaims)
        {
            RoleClaims result = await _accountRepository.AddOrUpdateRoleClaim(roleClaims);

            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet("TestResoureForFemale")]
        [Authorize]
        [Authorize(Policy = "IsFemale")]
        public async Task<IActionResult> TestResoureForFemale()
        {
            return Ok("this is resource for female");
        }

        [HttpPost("ConfirmAccessGoogleToken")]
        public async Task<IActionResult> confirmAccessGoogleToken(RequestConfirmGoogleToken confirmGoogleToken)
        {
            string idToken = confirmGoogleToken.accestokenGoogle;

            try
            {
                //GoogleJsonWebSignature.ValidationSettings validationSettings = new GoogleJsonWebSignature.ValidationSettings();
                //GoogleJsonWebSignature.Payload payload = GoogleJsonWebSignature.ValidateAsync(idToken, validationSettings).Result;

                //bool mailVerfify = payload.EmailVerified;

                //string firstName = payload.GivenName;
                //string lastName = payload.FamilyName;
                //string userEmail = payload.Email;

                //RegisterUser registerUser = new RegisterUser
                //{
                //    FirstName = payload.Email,
                //    LastName = payload.Email,
                //};

                var credential = GoogleCredential.FromAccessToken("eyJhbGciOiJSUzI1NiIsImtpZCI6IjFmNDBmMGE4ZWYzZDg4MDk3OGRjODJmMjVjM2VjMzE3YzZhNWI3ODEiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2FjY291bnRzLmdvb2dsZS5jb20iLCJhenAiOiIxNjAyNDI0MzQ0NDQtajFxMDFvbjVkZnFrdDNoMjdlMWUxajlpZHFmamdpdnMuYXBwcy5nb29nbGV1c2VyY29udGVudC5jb20iLCJhdWQiOiIxNjAyNDI0MzQ0NDQtajFxMDFvbjVkZnFrdDNoMjdlMWUxajlpZHFmamdpdnMuYXBwcy5nb29nbGV1c2VyY29udGVudC5jb20iLCJzdWIiOiIxMDU3OTg4Nzg0MTcyNTE4ODI4MzciLCJlbWFpbCI6Imx5dGhldmluaDEwNkBnbWFpbC5jb20iLCJlbWFpbF92ZXJpZmllZCI6dHJ1ZSwibmJmIjoxNzA1MDYxOTIxLCJuYW1lIjoiVmluaCBseSB0aGUiLCJwaWN0dXJlIjoiaHR0cHM6Ly9saDMuZ29vZ2xldXNlcmNvbnRlbnQuY29tL2EvQUNnOG9jS2tRUXBWLURtN0FVOTVGVzZKb2NkMWpOOHZUdUU1cUdxYml5enF2aTNQPXM5Ni1jIiwiZ2l2ZW5fbmFtZSI6IlZpbmgiLCJmYW1pbHlfbmFtZSI6Imx5IHRoZSIsImxvY2FsZSI6InZpIiwiaWF0IjoxNzA1MDYyMjIxLCJleHAiOjE3MDUwNjU4MjEsImp0aSI6ImZjZDNiNjhkMGZmMGYzOGZiZTIzMzUxZjRhNTM5M2JjYmExMmY4MTgifQ.eb9MCS-MsCcSss4hazrjjsHzKSKyBuGJIKBL0zQDVtmcF-m37_k_UQ2LM3NTGJAowbL1e4ogObiPXvKYVo1hRSYdjwNVZBmkaxIDoKjY8MnEs_n_QRGzeNFoBUkvQAB7lMR5PQvSeyiDZl1hBQPk5jDunYQkwp5tDh6lbfkhhLu0cHFmCl4SDWWdSHOFhQdLiYPtbkYjGXgWaPxaWukgrGdRqqHyph3Dgy6YkhQwUSkdj75dyp3S7O03IMiGni6DQ7S-EdGzrb4S71UWtRE_909oSi29i612wWz5xsBRejQXP9D1k-UYqaOXVtxa7dlFBxT2pTYNs9I1lDwloAR_yg");

                // Load thông tin xác thực từ tệp JSON
                //using (var stream = new FileStream(pathToCredentialsJson, FileMode.Open, FileAccess.Read))
                //{
                //    credential = GoogleCredential.FromStream(stream)
                //     .CreateScoped(PeopleServiceService.Scope.ContactsReadonly);
                //}

                // Tạo dịch vụ People API
                var service = new PeopleServiceService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "AspWith3tierDemo"
                });

                // Gọi API để lấy thông tin người dùng
                PeopleResource.GetRequest peopleRequest = service.People.Get("people/me");
                peopleRequest.PersonFields = "names,emailAddresses";

                Person me = peopleRequest.Execute();

                // Xử lý kết quả ở đây...
                Console.WriteLine($"Display Name: {me.Names?[0].DisplayName}");
                Console.WriteLine($"Email Address: {me.EmailAddresses?[0].Value}");

                // string jwtToken = _accountRepository.SignUpAsync();

                //using (HttpClient client = new HttpClient())
                //{
                //    // Thêm AccessToken vào Header Authorization
                //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", idToken);

                //    // Xây dựng URL API People
                //    string apiUrl = "https://people.googleapis.com/v1/people/me?personFields=names,emailAddresses";

                //    // Gửi yêu cầu GET
                //    HttpResponseMessage response = await client.GetAsync(apiUrl);

                //    if (response.IsSuccessStatusCode)
                //    {
                //        // Đọc và xử lý dữ liệu trả về từ API
                //        string responseData = await response.Content.ReadAsStringAsync();
                //        Console.WriteLine(responseData);
                //    }
                //    else
                //    {
                //        Console.WriteLine($"Error  ---: {response.StatusCode} - {response.ReasonPhrase}");
                //        string errorContent = await response.Content.ReadAsStringAsync();
                //        Console.WriteLine($"Error Content: {errorContent}");
                //        Console.WriteLine($"Error  ---: {response.StatusCode} - {response.ReasonPhrase}");
                //    }

                //}

                return Ok("Token is valid");
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khi xác minh token
                return BadRequest("Token validation failed: " + ex.Message);
            }
        }
    }
}