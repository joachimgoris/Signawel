using System;
using Signawel.Dto.Authentication;

namespace Signawel.Api.Tests.Builders.Dtos
{
    public class RegisterRequestDtoBuilder
    {
        private readonly RegisterRequestDto _registerRequestDto;

        public RegisterRequestDtoBuilder()
        {
            _registerRequestDto = new RegisterRequestDto();
        }

        public RegisterRequestDtoBuilder WithEmail()
        {
            _registerRequestDto.Email = Guid.NewGuid().ToString() + "@mail.com";
            return this;
        }

        public RegisterRequestDtoBuilder WithPassword()
        {
            _registerRequestDto.Password = Guid.NewGuid().ToString();
            return this;
        }

        public RegisterRequestDtoBuilder WithPasswordRepeat()
        {
            _registerRequestDto.PasswordRepeat = _registerRequestDto.Password;
            return this;
        }

        public RegisterRequestDto Build()
        {
            return _registerRequestDto;
        }
    }
}
