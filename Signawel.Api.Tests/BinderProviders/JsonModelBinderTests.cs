﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using NUnit.Framework;
using Signawel.API.BinderProviders;
using System;

namespace Signawel.Api.Tests.BinderProviders
{
    [TestFixture]
    public class JsonModelBinderTests
    {

        private IModelBinder _binder;

        [SetUp]
        public void Setup()
        {
            _binder = new JsonModelBinder();
        }

        [Test]
        public void BindModelAsync_ShouldThrowException_WhenContextIsNull()
        {
            // Act + Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _binder.BindModelAsync(null));
        }

    }
}
