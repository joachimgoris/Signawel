﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Signawel.API.Controllers;
using Signawel.Business.Abstractions.Services;
using Signawel.Domain.DataResults;
using Signawel.Dto;
using System;
using System.Collections.Generic;

namespace Signawel.Api.Tests.Controllers
{
    [TestFixture]
    public class BaseControllerTests
    {

        private ImageController _controller;

        [SetUp]
        public void Setup()
        {
            var imageServiceMock = new Mock<IImageService>();
            _controller = new ImageController(imageServiceMock.Object);
        }
        
        [Test]
        public void BadRequest_ShouldReturnAListOfAllPublicErrors()
        {
            // Arrange
            var dataResult = new DataResult();
            var publicErrorCode = Guid.NewGuid().ToString();
            dataResult.AddError(publicErrorCode, "", DataErrorVisibility.Public);
            dataResult.AddError(Guid.NewGuid().ToString(), "", DataErrorVisibility.Internal);
            dataResult.AddError(Guid.NewGuid().ToString(), "", DataErrorVisibility.Private);

            // Act
            var errorResponse = _controller.BadRequest(dataResult) as BadRequestObjectResult;

            // Assert
            Assert.That(errorResponse, Is.Not.Null);
            Assert.That(errorResponse.Value, Is.InstanceOf<IList<ErrorResponseDto>>());
            Assert.That(errorResponse.Value, Has.Count.EqualTo(1));
        }

        [Test]
        public void NotFound_ShouldReturnAListOfAllPublicErrors()
        {
            // Arrange
            var dataResult = new DataResult();
            var publicErrorCode = Guid.NewGuid().ToString();
            dataResult.AddError(publicErrorCode, "", DataErrorVisibility.Public);
            dataResult.AddError(Guid.NewGuid().ToString(), "", DataErrorVisibility.Internal);
            dataResult.AddError(Guid.NewGuid().ToString(), "", DataErrorVisibility.Private);

            // Act
            var errorResponse = _controller.NotFound(dataResult) as NotFoundObjectResult;

            // Assert
            Assert.That(errorResponse, Is.Not.Null);
            Assert.That(errorResponse.Value, Is.InstanceOf<IList<ErrorResponseDto>>());
            Assert.That(errorResponse.Value, Has.Count.EqualTo(1));
        }

    }
}
