using AutoMapper;
using Signawel.Business.Abstractions.Services;
using Signawel.Data.Abstractions.Repositories;
using Signawel.Domain;
using Signawel.Dto.Determination;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Signawel.Business.Services
{
    public class DeterminationService : IDeterminationService
    {
        private readonly IDeterminationRepository _repository;
        private readonly IMapper _mapper;

        public DeterminationService(IDeterminationRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task<DeterminationGraphResponseDto> GetGraphAsync()
        {
            var result = await _repository.GetGraphAsync();
            var response = _mapper.Map<DeterminationGraphResponseDto>(result);
            return response;
        }

        public async Task<DeterminationGraphResponseDto> SetGraphAsync(DeterminationGraphCreationRequestDto creationDto)
        {
            var toSave = _mapper.Map<DeterminationGraph>(creationDto);

            await _repository.SetGraphAsync(toSave);
            return await GetGraphAsync();
        }
    }
}
