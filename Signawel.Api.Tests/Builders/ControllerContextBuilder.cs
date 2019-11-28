using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Signawel.Api.Tests.Builders
{
    class ControllerContextBuilder
    {
        private readonly Random _random;
        private readonly ControllerContext _context;

        public ControllerContextBuilder()
        {
            _random = new Random();
            _context = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
            };
        }

        public ControllerContextBuilder WithUser()
        {
            _context.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()));
            return this;
        }

        public ControllerContextBuilder WithClientIp()
        {
            var maxValue = Convert.ToInt64("FFFFFFFF", 16);
            var address = Convert.ToInt64(_random.NextDouble() * maxValue);
            _context.HttpContext.Connection.RemoteIpAddress = new IPAddress(address);
            return this;
        }

        public ControllerContext Build()
        {
            return _context;
        }
    }
}
