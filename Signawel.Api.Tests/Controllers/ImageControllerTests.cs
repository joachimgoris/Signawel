﻿using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Signawel.API.Controllers;
using Signawel.Business.Abstractions.Services;

namespace Signawel.Api.Tests.Controllers
{
    [TestFixture]
    public class ImageControllerTests
    {

        private ImageController _sutImageController;
        private Mock<IImageService> _iImageServiceMock;

        [SetUp]
        public void SetUp()
        {
            _iImageServiceMock = new Mock<IImageService>();
            _sutImageController = new ImageController(_iImageServiceMock.Object);
        }

        [Test]
        public async Task GetImage_ShouldReturnStatusCode404WhenImageNotFound()
        {
            var id = Guid.NewGuid().ToString();
            _iImageServiceMock.Setup(service => service.GetImageAsync(id)).ReturnsAsync((MemoryStream)null);

            var response = await _sutImageController.GetImage(id) as NotFoundResult;
            
            _iImageServiceMock.Verify(service => service.GetImageAsync(id), Times.Once);
            Assert.IsInstanceOf<NotFoundResult>(response);
        }

        [Test]
        public async Task GetImage_ShouldReturnFileStreamWithImageWhenImageFound()
        {
            var id = Guid.NewGuid().ToString();
            _iImageServiceMock.Setup(service => service.GetImageAsync(id)).ReturnsAsync(new MemoryStream());

            var response = await _sutImageController.GetImage(id) as FileStreamResult;

            _iImageServiceMock.Verify(service => service.GetImageAsync(id), Times.Once);
            Assert.That(response?.FileStream, Is.Not.Null);
        }

        //TODO write test for post request
    }
}
