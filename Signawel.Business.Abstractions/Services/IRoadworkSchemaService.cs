using Signawel.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Signawel.Dto.RoadworkSchema;

namespace Signawel.Business.Abstractions.Services
{
    public interface IRoadworkSchemaService
    {

        Task<RoadworkSchemaResponseDto> GetRoadworkSchema(string id);

        Task<RoadworkSchemaResponseDto> CreateRoadworkSchema(RoadworkSchemaCreationRequestDto dto);

        Task<RoadworkSchemaResponseDto> PutRoadworkSchema(string id, RoadworkSchemaPutRequestDto dto);

        IQueryable<RoadworkSchemaResponseDto> GetAllRoadworkSchemas();

        Task<bool> DeleteRoadworkSchema(string id);

    }
}
