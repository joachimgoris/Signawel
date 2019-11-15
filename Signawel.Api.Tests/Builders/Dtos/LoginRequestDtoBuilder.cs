using System;
using Signawel.Dto.Authentication;

namespace Signawel.Api.Tests.Builders.Dtos
{
    public class LoginRequestDtoBuilder
    {
        private readonly LoginRequestDto _loginRequestDto;

        public LoginRequestDtoBuilder()
        {
            _loginRequestDto = new LoginRequestDto();
        }

        public LoginRequestDtoBuilder WithEmail()
        {
            _loginRequestDto.Email = Guid.NewGuid().ToString() + "@mail.com";
            return this;
        }

        public LoginRequestDtoBuilder WithPassword()
        {
            _loginRequestDto.Password = Guid.NewGuid().ToString();
            return this;
        }

        public LoginRequestDto Build()
        {
            return _loginRequestDto;
        }
    }
}
