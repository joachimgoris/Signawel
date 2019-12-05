using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Signawel.Mobile.Bootstrap.Abstract;
using Signawel.Mobile.Constants;
using Signawel.Mobile.Services.Abstract;
using Signawel.Mobile.ViewModels;
using Image = Xamarin.Forms.Image;

namespace Signawel.Mobile.Tests.ViewModels
{
    [TestFixture]
    public class ReportViewModelTests
    {
        private ReportViewModel _sutReportViewModel;
        private Mock<INavigationService> _navigationServiceMock;
        private Mock<IMessageBoxService> _messageBoxServiceMock;
        private Mock<IReportService> _reportServiceMock;
        private Mock<IIssueService> _issueServiceMock;

        [SetUp]
        public void SetUp()
        {
            _navigationServiceMock = new Mock<INavigationService>();
            _messageBoxServiceMock = new Mock<IMessageBoxService>();
            _reportServiceMock = new Mock<IReportService>();
            _issueServiceMock = new Mock<IIssueService>();
            _sutReportViewModel = new ReportViewModel(_navigationServiceMock.Object,
                _messageBoxServiceMock.Object, 
                _reportServiceMock.Object, 
                _issueServiceMock.Object);
        }

        [Test]
        public void ImageClickedCommand_ShouldDeleteImageFromReport()
        {
            var image = new Image();
            _messageBoxServiceMock.Setup(service => 
                service.ShowActionSheet(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(), null))
                .ReturnsAsync(TextConstants.DeleteButton);

            _sutReportViewModel.Images.Add(image);

            _sutReportViewModel.ImageClickedCommand.Execute(image);

            _messageBoxServiceMock.Verify(service => service
                .ShowActionSheet(TextConstants.DeleteImage, 
                                      TextConstants.CancelButton,
                             TextConstants.DeleteButton, 
                                null), Times.Once);

            CollectionAssert.DoesNotContain(_sutReportViewModel.Images, image);
        }
    }
}
