using System.Threading.Tasks;
using Signawel.Dto.RoadworkSchema;

namespace Signawel.Mobile.Services.Abstract
{
    public interface IDeterminationSchemaService
    {

        Task<RoadworkSchemaResponseDto> GetRoadworkSchema(string id);

    }
}
