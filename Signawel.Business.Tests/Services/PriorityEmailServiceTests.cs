using AutoMapper;
using NUnit.Framework;
using Signawel.Business.MapperProfiles;
using Signawel.Business.Services;
using Signawel.Data;
using Signawel.Domain;
using Signawel.Dto.PriorityEmail;
using System.Linq;
using System.Threading.Tasks;

namespace Signawel.Business.Tests.Services
{
    [TestFixture]
    public class PriorityEmailServiceTests
    {

        private SignawelDbContext _context;
        private IMapper _mapper;
        private PriorityEmailService _priorityEmailService;

        [SetUp]
        public void Setup()
        {
            _context = SignawelDbContextBuilder.GetDatabaseContext();

            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new PriorityEmailProfile())));
            _priorityEmailService = new PriorityEmailService(_context, _mapper);
        }

        #region CheckPriorityEmailAsync

        [Test]
        public async Task CheckPriorityEmailAsync_ShouldReturnTrue_IfEmailAddressSuffixIsPriority()
        {
            // Arrange
            await _context.PriorityEmails.AddAsync(new PriorityEmail
            {
                EmailSuffix = "gmail.com"
            });
            await _context.SaveChangesAsync();

            // Act
            var result = await _priorityEmailService.CheckPriorityEmailAsync("gmail.com");

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task CheckPriorityEmailAsync_ShouldReturnFalse_IfEmailAddressSuffixIsNotPriority()
        {
            // Act
            var result = await _priorityEmailService.CheckPriorityEmailAsync("test.com");

            // Assert
            Assert.That(result, Is.False);
        }

        #endregion CheckPriorityEmailAsync

        #region GetPriorityEmails

        [Test]
        public async Task GetPriorityEmails_ShouldReturnIQueryableOfAllEmail()
        {
            // Arrange
            await _context.PriorityEmails.AddAsync(new PriorityEmail
            {
                EmailSuffix = "gmail.com"
            });
            await _context.SaveChangesAsync();

            // Act
            var allQuery = _priorityEmailService.GetPriorityEmails().ToList();

            // Assert
            Assert.That(allQuery, Has.Count.EqualTo(1));
        }

        #endregion GetPriorityEmails

        #region AddPriorityEmail

        [Test]
        public async Task AddPriorityEmail_ShouldReturnError_WhenSuffixInvalid()
        {
            // Arrange
            var dto = new PriorityEmailCreationRequestDto
            {
                EmailSuffix = "test"
            };

            // Act
            var result = await _priorityEmailService.AddPriorityEmailAsync(dto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Succeeded, Is.False);
        }

        [Test]
        public async Task AddPriorityEmail_ShouldReturnError_WhenSuffixIsAleadyInDatabase()
        {
            // Arrange
            await _context.PriorityEmails.AddAsync(new PriorityEmail
            {
                EmailSuffix = "gmail.com"
            });
            await _context.SaveChangesAsync();

            var dto = new PriorityEmailCreationRequestDto
            {
                EmailSuffix = "gmail.com"
            };

            // Act
            var result = await _priorityEmailService.AddPriorityEmailAsync(dto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Succeeded, Is.False);
        }

        [Test]
        public async Task AddPriorityEmail_ShouldReturnSuccess()
        {
            var dto = new PriorityEmailCreationRequestDto
            {
                EmailSuffix = "gmail.com"
            };

            // Act
            var result = await _priorityEmailService.AddPriorityEmailAsync(dto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Succeeded, Is.True);
        }

        #endregion AddPriorityEmail

    }
}
