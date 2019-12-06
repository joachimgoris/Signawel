using System.Collections.Generic;
using System.Threading.Tasks;
using Signawel.Domain.Enums;
using Signawel.Dto.RoadworkSchema;

namespace Signawel.Mobile.Services.Abstract
{
    public interface IRoadworkSchemaService
    {

        Task<RoadworkSchemaResponseDto> GetRoadworkSchema(string id);

        Task<IList<RoadworkSchemaResponseDto>> GetRoadworkSchemaByCategory(RoadworkCategory category);

    }
}
