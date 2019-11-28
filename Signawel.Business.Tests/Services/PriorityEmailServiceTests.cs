using NUnit.Framework;
using Signawel.Business.Services;
using Signawel.Data;
using Signawel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signawel.Business.Tests.Services
{
    [TestFixture]
    public class PriorityEmailServiceTests
    {

        private SignawelDbContext _context;
        private PriorityEmailService _priorityEmailService;

        [SetUp]
        public void Setup()
        {
            _context = SignawelDbContextBuilder.GetDatabaseContext();
            _priorityEmailService = new PriorityEmailService(_context);
        }

        [Test]
        public async Task CheckPriorityEmailAsync_ShouldReturnTrue_IfEmailAddressSuffixIsPriority()
        {
            // Arrange
            await _context.PriorityEmails.AddAsync(new Domain.PriorityEmail
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

        [Test]
        public async Task GetPriorityEmails_ShouldReturnIQueryableOfAllEmail()
        {
            // Arrange
            await _context.PriorityEmails.AddAsync(new Domain.PriorityEmail
            {
                EmailSuffix = "gmail.com"
            });
            await _context.SaveChangesAsync();

            // Act
            var allQuery = _priorityEmailService.GetPriorityEmails().ToList();

            // Assert
            Assert.That(allQuery, Has.Count.EqualTo(1));
        }

    }
}
