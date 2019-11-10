using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Signawel.Business.Abstractions.Services;
using Signawel.Domain;
using Signawel.Domain.Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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

        public ICollection<GetUserDto> GetAllUsersAsync()
        {
            ICollection<GetUserDto> users = new List<GetUserDto>();
            foreach(var user in _userManager.Users)
            {
                users.Add(_mapper.Map<GetUserDto>(user));
            }
            return users;
        }

        public async Task<GetUserDto> GetUserAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return null;

            User user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                _logger.LogWarning("Unable to find user with id {userId}", userId);
                return null;
            }

            return _mapper.Map<GetUserDto>(user);
        }

        #endregion


        public Task<GetUserDto> CreateUserAsync(CreateUserDto model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserasync(string userId)
        {
            throw new NotImplementedException();
        }



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

        public Task<GetUserDto> ModifyUserAsync(string userId, ModifyUserDto modifiedUser)
        {
            throw new NotImplementedException();
        }
    }
}
