using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Authentication;
using E_Commerce.Application.DTOs.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_Commerce.API.Controllers
{
   
    public class AuthenticationController : ApiBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        => ToActionResult(await _authenticationService.LoginAsync(loginDto));

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto,CancellationToken ct)
            =>ToActionResult ( await _authenticationService.RegisterAsync(registerDto,ct));


        [HttpGet("EmailExist")]
        public async Task<ActionResult<bool>> CheckeEmail([FromQuery]string email,CancellationToken ct)
        
        => ToActionResult(await _authenticationService.CheckeEmailExistAsync(email,ct));
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> CurrentUser(CancellationToken ct)
        =>ToActionResult(await _authenticationService.GetCurrentUserAsync(GetEmailFormToken(),ct));
        

        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress(CancellationToken ct)
        => ToActionResult( await _authenticationService.GetUserAddressAsync(GetEmailFormToken(), ct));

        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto,CancellationToken ct)
          => ToActionResult(  await _authenticationService.UpSertUserAddressAsync(GetEmailFormToken(),addressDto,ct));
        
        
    }
}
