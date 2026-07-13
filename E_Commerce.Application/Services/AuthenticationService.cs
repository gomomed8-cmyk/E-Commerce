using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Authentication;
using E_Commerce.Application.DTOs.Identity;
using Microsoft.IdentityModel.Tokens.Experimental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly IIdentityService _identityService;
        private readonly ITokenService _tokenService;

        public AuthenticationService(IIdentityService identityService,ITokenService tokenService)
        {
            _identityService = identityService;
            _tokenService = tokenService;
        }

        public async Task<Result<bool>> CheckeEmailExistAsync(string email, CancellationToken ct = default)
            =>await _identityService.EmailExistAsync(email, ct);

        public async Task<Result<UserDto>> GetCurrentUserAsync(string email, CancellationToken ct = default)
        {

         var userResult= await _identityService.FindUserByEmailAsync(email, ct);
            var user = userResult.data;
            var roleResult=await _identityService.GetUserRolesAsync(email, ct);
            var token= _tokenService.CreateToken(user.Id,user.Email,user.UserName,roleResult.data);
            return new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = email,
                Token = token,
            };
            
        }

        public async Task<Result<AddressDto>> GetUserAddressAsync(string email, CancellationToken ct = default)
        {
            return await _identityService.GetUserAddressByEmailAysnc(email, ct);
        }

        public async Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default)
        {
            var userResult=await _identityService.FindUserByEmailAsync(loginDto.Email);
            if(!userResult.IsSuccess)
                return Result<UserDto>.Fail(userResult.Errors);

            var passwordResult=await _identityService.CheckPasswordAsync(loginDto.Email,loginDto.Password,ct);
            if(!passwordResult.IsSuccess)
                return Result<UserDto>.Fail(userResult.Errors);
            if(!passwordResult.data)
                return Result<UserDto>.Fail(Error.Unauthorized("Invalid Email Or Password"));

            var user = userResult.data;
            var rolesResult = await _identityService.GetUserRolesAsync(user.Email);
            var roles = rolesResult.data;
            var token = _tokenService.CreateToken(user.Id, user.Email, user.UserName, roles);

            return new UserDto()
            {
                Email= loginDto.Email,
                DisplayName=userResult.data.DisplayName,
                Token=token

            };



        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken ct = default)
        {
          var userResult=await _identityService.CreateUserAsync(registerDto, ct);
            if(!userResult.IsSuccess)
            {
                return Result<UserDto>.Fail(userResult.Errors);
            }
            var user=userResult.data;
            var rolesResult = await _identityService.GetUserRolesAsync(user.Email);
            var roles = rolesResult.data;
            var token = _tokenService.CreateToken(user.Id, user.Email, user.UserName, roles);
            return Result<UserDto>.Ok(new UserDto()
            {
                Email=user.Email,
                DisplayName=user.DisplayName,
                Token=token

            });
        }

        public async Task<Result<AddressDto>> UpSertUserAddressAsync(string email, AddressDto addressDto, CancellationToken ct = default)
            =>await _identityService.UpdateOrInsertUserAddressAsync(email, addressDto, ct);
        
    }
}
