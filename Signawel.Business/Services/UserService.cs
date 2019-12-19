using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Signawel.Business.Abstractions.Services;
using Signawel.Domain;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Signawel.Domain.Constants;
using Signawel.Domain.DataResults;
using Signawel.Dto.Authentication;

namespace Signawel.Business.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UserService(ILogger<UserService> logger, IMapper mapper, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        #region Get

        public async Task<DataResult<ICollection<Dto.ReportGroup.UserResponseDto>>> GetAllUsersAsync()
        {
            ICollection<Dto.ReportGroup.UserResponseDto> users = new List<Dto.ReportGroup.UserResponseDto>();
            
            await _userManager.Users.ForEachAsync(u => users.Add(_mapper.Map<Dto.ReportGroup.UserResponseDto>(u)));
            
            return DataResult<ICollection<Dto.ReportGroup.UserResponseDto>>.Success(users);
        }

        public async Task<DataResult<UserResponseDto>> GetUserAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return DataResult<UserResponseDto>.WithPublicError(ErrorCodes.ParameterEmptyError,
                    $"{nameof(userId)} is empty.");

            User user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                _logger.LogWarning("Unable to find user with id {userId}", userId);
                return DataResult<UserResponseDto>.WithPublicError(ErrorCodes.NotFoundError,
                    $"There was no user with the given id: {userId}");
            }

            return DataResult<UserResponseDto>.Success(_mapper.Map<UserResponseDto>(user));
        }

        #endregion

        #region GetUserClaimsAsync
        
        public async Task<ICollection<Claim>> GetUserClaimsAsync(string userId, bool includeRoles = true)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                _logger.LogWarning("Unable to find user with id {userId}", userId);
                return null;
            }

            var claims = new List<Claim>();

            claims.AddRange(await _userManager.GetClaimsAsync(user));

            if (!includeRoles)
            {
                return claims;
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var role = await _roleManager.FindByNameAsync(userRole);

                if (role == null)
                {
                    continue;
                }

                var roleClaims = await _roleManager.GetClaimsAsync(role);

                foreach (var roleClaim in roleClaims)
                {
                    if (claims.Contains(roleClaim))
                    {
                        continue;
                    }

                    claims.Add(roleClaim);
                }
            }

            return claims;
        }
        
        #endregion
        
        #region ModifyUser
        
        public async Task<DataResult> ModifyUserAsync(string userId, UserModifyRequestDto model)
        {
            User user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return DataResult<UserResponseDto>.WithPublicError(ErrorCodes.NotFoundError,
                    $"There was no user with the given id: {userId}");
            }
            
            IdentityResult result = await _userManager.UpdateAsync(_mapper.Map<User>(model));
            
            return result.Succeeded ? DataResult.Success :
                DataResult.WithPublicError(ErrorCodes.InvalidOperationError, "Something went wrong with updating a user.");
        }
        
        #endregion
        
        #region CreateUser
        
        public async Task<DataResult<UserResponseDto>> CreateUserAsync(UserCreateRequestDto model)
        {
            var user = _mapper.Map<User>(model);

            await _userManager.CreateAsync(user);

            var response = _mapper.Map<UserResponseDto>(user);

            return DataResult<UserResponseDto>.Success(response);
        }
        
        #endregion

        #region DeleteUser

        public async Task<DataResult> DeleteUserAsync(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);
            
            if (user == null)
            {
                return DataResult<UserResponseDto>.WithPublicError(ErrorCodes.NotFoundError,
                    $"There was no user with the given id: {userId}");
            }

            IdentityResult result = await _userManager.DeleteAsync(user);

            return result.Succeeded ? DataResult.Success :
                DataResult.WithPublicError(ErrorCodes.InvalidOperationError, "Something went wrong with deleting a user.");
        }
        
        #endregion
    }
}
