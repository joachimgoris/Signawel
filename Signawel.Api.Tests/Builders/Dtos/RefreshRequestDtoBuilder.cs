using System;
using Signawel.Dto.Authentication;

namespace Signawel.Api.Tests.Builders.Dtos
{
    public class RefreshRequestDtoBuilder
    {
        private readonly RefreshRequestDto _refreshRequestDto;

        public RefreshRequestDtoBuilder()
        {
            _refreshRequestDto = new RefreshRequestDto();
        }

        public RefreshRequestDtoBuilder WithJwtToken()
        {
            _refreshRequestDto.JwtToken = Guid.NewGuid().ToString();
            return this;
        }

        public RefreshRequestDtoBuilder WithRefreshToken()
        {
            _refreshRequestDto.RefreshToken = Guid.NewGuid().ToString();
            return this;
        }

        public RefreshRequestDto Build()
        {
            return _refreshRequestDto;
        }
    }
}
