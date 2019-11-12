using Signawel.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signawel.Business.Abstractions.Services
{
    public interface IRoadworkSchemaService
    {

        Task<RoadworkSchemaResponseDto> GetRoadworkSchema(string id);

        Task<RoadworkSchemaResponseDto> CreateRoadworkSchema(RoadworkSchemaCreationRequestDto dto);

        Task<RoadworkSchemaResponseDto> PutRoadworkSchema(string id, RoadworkSchemaCreationRequestDto dto);

        IQueryable<RoadworkSchemaResponseDto> GetAllRoadworkSchemas();

    }
}
