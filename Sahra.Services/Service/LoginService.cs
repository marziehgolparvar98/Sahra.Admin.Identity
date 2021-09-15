//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using Sahara.Common;
//using Sahra.DataLayer.Models.Identity;
//using Sahra.Services.IService;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;

//namespace Sahra.Services.Service
//{
//    public class LoginService : ILoginService
//    {

//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly IConfiguration _configuration;
//        public LoginService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
//        {
//            _userManager = userManager;
//            _configuration = configuration;

//        }
//        public async Task<Result<LoginResponse>> Login(LoginRequest model)
//        {
//            try
//            {

//                var user = await _userManager.FindByNameAsync(model.Username);
//                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
//                {
//                    var userRoles = await _userManager.GetRolesAsync(user);

//                    var authClaims = new List<Claim>
//                {
//                    new Claim(ClaimTypes.Name, user.UserName),

//                };

//                    foreach (var userRole in userRoles)
//                    {
//                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
//                    }

//                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

//                    var token = new JwtSecurityToken(
//                        issuer: _configuration["JWT:ValidIssuer"],
//                        audience: _configuration["JWT:ValidAudience"],
//                        expires: DateTime.Now.AddHours(3),
//                        claims: authClaims,
//                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
//                        );
//                    var res = new LoginResponse
//                    {
//                        Token = new JwtSecurityTokenHandler().WriteToken(token),
//                        Expiration = token.ValidTo
//                    };

//                    return Result.Success(res);
//                }

//                return Result.Failed<LoginResponse>("نام کاربری یا پسوورد اشتباه میباشد.");
//            }

//            catch (Exception)
//            {

//                return Result.Failed<LoginResponse>("خطا در سیستم");
//            }

//        }
//    }
//}
