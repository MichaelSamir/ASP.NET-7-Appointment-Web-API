using AppointmentWebApi.Core.Dtos;
using AppointmentWebApi.Core.Entities;
using AppointmentWebApi.Core.Interfaces;
using AppointmentWebApi.Core.OtherObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace AppointmentWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private readonly UserManager<User> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly IConfiguration _configuration;

        //public AuthController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        //{
        //    _userManager = userManager;
        //    _roleManager = roleManager;
        //    _configuration = configuration;
        //}

        IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // Seed roles in DB
        [HttpPost]
        [Route("seed-roles")]
        public async Task<IActionResult> SeedRoles()
        {
            //string result = string.Empty;
            //bool isAdminExists = await _roleManager.RoleExistsAsync(StaticRoles.ADMIN);
            //bool isServantExists = await _roleManager.RoleExistsAsync(StaticRoles.SERVANT);

            //if (isAdminExists)
            //    result += "Admin already exists in DB.";
            //else
            //{
            //    var createResult = await _roleManager.CreateAsync(new IdentityRole(StaticRoles.ADMIN));
            //    if (createResult.Succeeded)
            //        result = string.IsNullOrEmpty(result) ? "ADMIN role created successfully." : result + "\nADMIN role created successfully.";
            //    else if (createResult.Errors != null && createResult.Errors.Count() > 0)
            //    {
            //        Array.ForEach(createResult.Errors.ToArray(), d => result += "\n" + d.Description);
            //    }
            //}

            //if (isServantExists)
            //    result = string.IsNullOrEmpty(result) ? "Servant already exists in DB." : result + "\nServant already exists in DB.";
            //else
            //{
            //    var createResult = await _roleManager.CreateAsync(new IdentityRole(StaticRoles.SERVANT));
            //    if (createResult.Succeeded)
            //        result = string.IsNullOrEmpty(result) ? "SERVANT role created successfully." : result + "\nSERVANT role created successfully.";
            //    else if (createResult.Errors != null && createResult.Errors.Count() > 0)
            //    {
            //        Array.ForEach(createResult.Errors.ToArray(), d => result += "\n" + d.Description);
            //    }
            //}

            //return Ok(result);

            DataResultDto resultDto = await _authService.SeedRolesAsync();

            if (resultDto.IsSucceeded)
                return Ok(resultDto);

            return BadRequest(resultDto);
        }

        // Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            //var userInDB = await _userManager.FindByNameAsync(registerDto.UserName);

            //if (userInDB != null && userInDB.Active)
            //{
            //    Error error = new Error { ErrorMessage = "UserName already exists in DB" };
            //    DataResultDto dataResult = new DataResultDto
            //    {
            //        StatusCode = HttpStatusCode.BadRequest.ToString(),
            //        HasErrors = true,
            //        Errors = new List<Error>() { error }
            //    };
            //    return BadRequest(dataResult);
            //}

            //User newUser = new User()
            //{
            //    Name = registerDto.Name,
            //    UserName = registerDto.UserName,
            //    Email = registerDto.Email,
            //    ServiceName = registerDto.ServiceName,
            //    PhoneNumber = registerDto.Mobile,
            //    Active = true,
            //    Created = DateTime.Now,
            //    Modified = DateTime.Now,
            //    SecurityStamp = Guid.NewGuid().ToString()
            //};

            //var createUserResult = await _userManager.CreateAsync(newUser, registerDto.Password);

            //if (!createUserResult.Succeeded)
            //{
            //    string errorString = "User creation failed!";
            //    if (createUserResult.Errors != null && createUserResult.Errors.Count() > 0)
            //    {
            //        errorString += " with Error(s):";
            //        foreach (var error in createUserResult.Errors)
            //        {
            //            errorString += "\n" + error.Description;
            //        }
            //    }

            //    Error errorObj = new Error { ErrorMessage = errorString };

            //    DataResultDto dataResult = new DataResultDto
            //    {
            //        StatusCode = HttpStatusCode.BadRequest.ToString(),
            //        HasErrors = true,
            //        Errors = new List<Error>() { errorObj }
            //    };

            //    return BadRequest(dataResult);
            //}

            //// Add default SERVANT role to all users
            //await _userManager.AddToRoleAsync(newUser, StaticRoles.SERVANT);

            //return Ok("User created successfully.");

            DataResultDto resultDto = await _authService.RegisterAsync(registerDto);

            if (resultDto.IsSucceeded)
                return Ok(resultDto);

            return BadRequest(resultDto);
        }

        // Login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            //var userInDB = await _userManager.FindByNameAsync(loginDto.UserName);
            //DataResultDto dataResult;

            //if (userInDB == null || !userInDB.Active)
            //{
            //    Error errorObj = new Error { ErrorMessage = "Invalid credentials!" };

            //    dataResult = new DataResultDto
            //    {
            //        StatusCode = ((int)HttpStatusCode.BadRequest).ToString(),
            //        HasErrors = true,
            //        Errors = new List<Error>() { errorObj }
            //    };

            //    return Unauthorized(dataResult);
            //}

            //var isPasswordCorrect = await _userManager.CheckPasswordAsync(userInDB, loginDto.Password);

            //if (!isPasswordCorrect)
            //{
            //    Error errorObj = new Error { ErrorMessage = "Invalid credentials!" };

            //    dataResult = new DataResultDto
            //    {
            //        StatusCode = ((int)HttpStatusCode.BadRequest).ToString(),
            //        HasErrors = true,
            //        Errors = new List<Error>() { errorObj }
            //    };

            //    return Unauthorized(dataResult);
            //}

            //var userRoles = await _userManager.GetRolesAsync(userInDB);

            //var authClaims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name,userInDB.UserName),
            //    new Claim(ClaimTypes.NameIdentifier,userInDB.Id),
            //    new Claim("JWTID",Guid.NewGuid().ToString())
            //};

            //foreach (var userRole in userRoles)
            //{
            //    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            //}

            //var token = GenerateNewJsonWebToken(authClaims);

            //dataResult = new DataResultDto
            //{
            //    StatusCode = ((int)HttpStatusCode.OK).ToString(),
            //    Result = token,
            //    HasErrors = false
            //};

            //return Ok(dataResult);
            DataResultDto resultDto = await _authService.LoginAsync(loginDto);

            if(resultDto.IsSucceeded)
                return Ok(resultDto);

            return Unauthorized(resultDto);
        }

        //private string GenerateNewJsonWebToken(List<Claim> claims)
        //{
        //    var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        //    var tokenObject = new JwtSecurityToken(
        //        issuer: _configuration["JWT:ValidIssuer"],
        //        audience: _configuration["JWT:ValidAudience"],
        //        expires: DateTime.Now.AddHours(1),
        //        claims: claims,
        //        signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)
        //        );

        //    string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);

        //    return token;
        //}

        //ValidateIssuer = true,
        //    ValidateAudience = true,
        //    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        //    ValidAudience = builder.Configuration["JWT:ValidAudience"],
        //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    }
}
