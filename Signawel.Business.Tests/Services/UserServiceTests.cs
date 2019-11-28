using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Signawel.Business.Services;
using Signawel.Domain;

namespace Signawel.Business.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private UserService _service;
        private Mock<UserManager<User>> _userManager;
        private Mock<RoleManager<Role>> _roleManager;
        private Mock<IMapper> _mapper;

        [OneTimeSetUpAttribute]
        public void SetUp()
        {
            var logger = new Mock<ILogger<UserService>>();
            _userManager = new Mock<UserManager<User>>();
            _roleManager = new Mock<RoleManager<Role>>();
            _mapper = new Mock<IMapper>();

            _service = new UserService(logger.Object, _mapper.Object, _userManager.Object, _roleManager.Object);
        }


    }
}
