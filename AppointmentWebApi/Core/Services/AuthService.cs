using AppointmentWebApi.Core.Dtos;
using AppointmentWebApi.Core.Entities;
using AppointmentWebApi.Core.Interfaces;
using AppointmentWebApi.Core.OtherObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace AppointmentWebApi.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        public async Task<DataResultDto> LoginAsync(LoginDto loginDto)
        {
            var userInDB = await _userManager.FindByNameAsync(loginDto.UserName);
            DataResultDto dataResult;

            if (userInDB == null || !userInDB.Active)
            {
                Error errorObj = new Error { ErrorMessage = "Invalid credentials!" };

                dataResult = new DataResultDto
                {
                    IsSucceeded = false,
                    StatusCode = ((int)HttpStatusCode.Unauthorized).ToString(),
                    HasErrors = true,
                    Errors = new List<Error>() { errorObj }
                };

                return dataResult;
            }

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(userInDB, loginDto.Password);

            if (!isPasswordCorrect)
            {
                Error errorObj = new Error { ErrorMessage = "Invalid credentials!" };

                dataResult = new DataResultDto
                {
                    IsSucceeded = false,
                    StatusCode = ((int)HttpStatusCode.Unauthorized).ToString(),
                    HasErrors = true,
                    Errors = new List<Error>() { errorObj }
                };

                return dataResult;
            }

            var userRoles = await _userManager.GetRolesAsync(userInDB);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,userInDB.UserName),
                new Claim(ClaimTypes.NameIdentifier,userInDB.Id),
                new Claim("JWTID",Guid.NewGuid().ToString())
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = GenerateNewJsonWebToken(authClaims);

            dataResult = new DataResultDto
            {
                IsSucceeded = true,
                StatusCode = ((int)HttpStatusCode.OK).ToString(),
                Result = token,
                HasErrors = false
            };

            return dataResult;
        }

        public async Task<DataResultDto> RegisterAsync(RegisterDto registerDto)
        {
            var userInDB = await _userManager.FindByNameAsync(registerDto.UserName);

            if (userInDB != null && userInDB.Active)
            {
                Error error = new Error { ErrorMessage = "UserName already exists in DB" };
                DataResultDto dataResult = new DataResultDto
                {
                    IsSucceeded = false,
                    StatusCode = HttpStatusCode.BadRequest.ToString(),
                    HasErrors = true,
                    Errors = new List<Error>() { error }
                };
                return dataResult;
            }

            User newUser = new User()
            {
                Name = registerDto.Name,
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                ServiceName = registerDto.ServiceName,
                PhoneNumber = registerDto.Mobile,
                Active = true,
                Created = DateTime.Now,
                Modified = DateTime.Now,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var createUserResult = await _userManager.CreateAsync(newUser, registerDto.Password);

            if (!createUserResult.Succeeded)
            {
                string errorString = "User creation failed!";
                if (createUserResult.Errors != null && createUserResult.Errors.Count() > 0)
                {
                    errorString += " with Error(s):";
                    foreach (var error in createUserResult.Errors)
                    {
                        errorString += "\n" + error.Description;
                    }
                }

                Error errorObj = new Error { ErrorMessage = errorString };

                DataResultDto dataResult = new DataResultDto
                {
                    IsSucceeded = false,
                    StatusCode = HttpStatusCode.BadRequest.ToString(),
                    HasErrors = true,
                    Errors = new List<Error>() { errorObj }
                };

                return dataResult;
            }

            // Add default SERVANT role to all users
            await _userManager.AddToRoleAsync(newUser, StaticRoles.SERVANT);
            DataResultDto resultDto = new DataResultDto
            {
                IsSucceeded = true,
                Result = "User created successfully.",
                StatusCode = HttpStatusCode.Created.ToString(),
                HasErrors = false,
            };
            return resultDto;
        }

        public async Task<DataResultDto> SeedRolesAsync()
        {
            string result = string.Empty;
            bool isAdminExists = await _roleManager.RoleExistsAsync(StaticRoles.ADMIN);
            bool isServantExists = await _roleManager.RoleExistsAsync(StaticRoles.SERVANT);
            DataResultDto resultDto = new DataResultDto();

            if (isAdminExists)
            {
                result += "Admin already exists in DB.";
                resultDto.Result = result;
                resultDto.IsSucceeded = true;
                resultDto.StatusCode = ((int)HttpStatusCode.OK).ToString();
            }
            else
            {
                var createResult = await _roleManager.CreateAsync(new IdentityRole(StaticRoles.ADMIN));
                if (createResult.Succeeded)
                {
                    result = string.IsNullOrEmpty(result) ? "ADMIN role created successfully." : result + "\nADMIN role created successfully.";

                    resultDto.Result = result;
                    resultDto.IsSucceeded = true;
                    resultDto.StatusCode = ((int)HttpStatusCode.OK).ToString();
                }
                else if (createResult.Errors != null && createResult.Errors.Count() > 0)
                {
                    List<Error> errors = new List<Error>();
                    Array.ForEach(createResult.Errors.ToArray(), d =>
                    {
                        if (d != null)
                            errors.Add(new Error { ErrorMessage = d.Description });
                    });

                    resultDto.Errors = errors;
                    resultDto.IsSucceeded = false;
                    resultDto.HasErrors = true;
                    resultDto.StatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                }
            }

            if (isServantExists)
            {
                result = string.IsNullOrEmpty(result) ? "Servant already exists in DB." : result + "\nServant already exists in DB.";

                resultDto.Result = result;
                resultDto.IsSucceeded = true;
                resultDto.StatusCode = ((int)HttpStatusCode.OK).ToString();
            }
            else
            {
                var createResult = await _roleManager.CreateAsync(new IdentityRole(StaticRoles.SERVANT));
                if (createResult.Succeeded)
                {
                    result = string.IsNullOrEmpty(result) ? "SERVANT role created successfully." : result + "\nSERVANT role created successfully.";

                    resultDto.Result = result;
                    resultDto.IsSucceeded = true;
                    resultDto.StatusCode = ((int)HttpStatusCode.OK).ToString();
                }
                else if (createResult.Errors != null && createResult.Errors.Count() > 0)
                {
                    List<Error> errors = new List<Error>();
                    Array.ForEach(createResult.Errors.ToArray(), d =>
                    {
                        if (d != null)
                            errors.Add(new Error { ErrorMessage = d.Description });
                    });

                    resultDto.Errors = errors;
                    resultDto.IsSucceeded = false;
                    resultDto.HasErrors = true;
                    resultDto.StatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                }
            }

            return resultDto;
        }

        private string GenerateNewJsonWebToken(List<Claim> claims)
        {
            var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var tokenObject = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: claims,
                signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)
                );

            string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);

            return token;
        }
    }
}
