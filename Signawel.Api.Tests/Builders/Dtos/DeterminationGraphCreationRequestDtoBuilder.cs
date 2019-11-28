using Signawel.Dto.Determination;

namespace Signawel.Api.Tests.Builders.Dtos
{
    public class DeterminationGraphCreationRequestDtoBuilder
    {
        private readonly DeterminationGraphCreationRequestDto _determinationGraphCreationRequestDto;

        public DeterminationGraphCreationRequestDtoBuilder()
        {
            _determinationGraphCreationRequestDto = new DeterminationGraphCreationRequestDto();
        }

        public DeterminationGraphCreationRequestDtoBuilder WithStart()
        {
            _determinationGraphCreationRequestDto.Start = new DeterminationNodeCreatingRequestDto();
            return this;
        }

        public DeterminationGraphCreationRequestDto Build()
        {
            return _determinationGraphCreationRequestDto;
        }
    }
}
