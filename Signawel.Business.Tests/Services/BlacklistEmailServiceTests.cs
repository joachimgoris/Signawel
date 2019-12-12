using AutoMapper;
using NUnit.Framework;
using Signawel.Business.MapperProfiles;
using Signawel.Business.Services;
using Signawel.Data;
using Signawel.Domain;
using Signawel.Domain.Constants;
using Signawel.Dto.BlacklistEmail;
using System.Linq;
using System.Threading.Tasks;

namespace Signawel.Business.Tests.Services
{
    [TestFixture]
    public class BlacklistEmailServiceTests
    {
        private SignawelDbContext _context;
        private IMapper _mapper;
        private BlacklistEmailService _blacklistEmailService;

        [SetUp]
        public void Setup()
        {
            _context = SignawelDbContextBuilder.GetDatabaseContext();

            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new BlacklistEmailProfile())));
            _blacklistEmailService = new BlacklistEmailService(_context, _mapper);
        }

        #region CheckBlacklistEmailAsync

        [Test]
        public async Task CheckBlacklistEmailAsync_ShouldReturnSuccess_IfEmailAddressIsBlacklisted()
        {
            // Arrange
            await _context.BlacklistEmails.AddAsync(new BlacklistEmail
            {
                Email = "blacklist@email.com"
            });
            await _context.SaveChangesAsync();

            // Act
            var result = await _blacklistEmailService.CheckBlacklistEmailAsync("blacklist@email.com");

            // Assert
            Assert.That(result.Succeeded, Is.True);
        }

        [Test]
        public async Task CheckBlacklistEmailAsync_ShouldReturnFalse_IfEmailAddressIsNotBlacklisted()
        {
            // Arrange


            // Act
            var result = await _blacklistEmailService.CheckBlacklistEmailAsync("email@test.com");

            // Assert
            Assert.That(result.Succeeded, Is.False);
        }

        #endregion

        #region GetBlacklistEmails

        [Test]
        public async Task GetBlacklistEmails_ShouldReturnIQueryableOfAllEmails()
        {
            // Arrange
            await _context.BlacklistEmails.AddAsync(new BlacklistEmail
            {
                Email = "test@emai.com"
            });
            await _context.SaveChangesAsync();

            // Act
            var result = _blacklistEmailService.GetBlacklistEmails().Entity.ToList();

            // Assert
            Assert.That(result, Has.Count.EqualTo(1));
        }

        #endregion

        #region AddBlacklistEmail

        [Test]
        public async Task AddBlacklistEmail_ShouldReturnError_WhenEmailIsInvalid()
        {
            // Arrange
            var dto = new BlacklistEmailCreationRequestDto
            {
                Email = ""
            };

            // Act
            var result = await _blacklistEmailService.AddBlacklistEmailAsync(dto);


            // Assert
            Assert.That(result.Errors, Is.Not.Empty);
            Assert.That(result.HasError(ErrorCodes.BlacklistEmailCreationError), Is.True);
        }

        [Test]
        public async Task AddBlacklistEmail_ShouldReturnError_WhenEmailIsAlreadyBlacklisted()
        {
            // Arrange
            await _context.BlacklistEmails.AddAsync(new BlacklistEmail
            {
                Email = "email@test.com"
            });
            await _context.SaveChangesAsync();
            var dto = new BlacklistEmailCreationRequestDto
            {
                Email = "email@test.com"
            };

            // Act
            var result = await _blacklistEmailService.AddBlacklistEmailAsync(dto);


            // Assert
            Assert.That(result.Errors, Is.Not.Empty);
            Assert.That(result.HasError(ErrorCodes.BlacklistEmailCreationError), Is.True);
        }

        [Test]
        public async Task AddBlacklistEmailAsync_ShouldReturnSuccess()
        {
            // Arrange
            var dto = new BlacklistEmailCreationRequestDto
            {
                Email = "test@email.com"
            };

            // Act
            var result = await _blacklistEmailService.AddBlacklistEmailAsync(dto);

            // Assert
            Assert.That(result.Errors, Is.Empty);
            Assert.That(result.Entity.Email, Is.EqualTo(dto.Email));
        }

        #endregion
    }
}
