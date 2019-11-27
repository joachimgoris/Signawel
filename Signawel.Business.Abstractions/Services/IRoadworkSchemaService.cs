using Signawel.Dto;
using System.Linq;
using System.Threading.Tasks;
using Signawel.Dto.RoadworkSchema;
using Signawel.Domain.DataResults;

namespace Signawel.Business.Abstractions.Services
{
    public interface IRoadworkSchemaService
    {

        /// <summary>
        ///     Get a roadwork schema from the database.
        /// </summary>
        /// <param name="id">
        ///     Id of the roadwork schema to get from the database.
        /// </param>
        /// <returns>
        ///     Instance of <see cref="DataResult{TEntity}"/> containing an <see cref="RoadworkSchemaResponseDto"/> or errors;
        /// </returns>
        Task<DataResult<RoadworkSchemaResponseDto>> GetRoadworkSchema(string id);

        /// <summary>
        ///     Create a new roadwork schema
        /// </summary>
        /// <param name="dto">
        ///     Instance of <see cref="RoadworkSchemaCreationRequestDto"/> containing details about the roadwork schema to create
        /// </param>
        /// <param name="imageId">
        ///     Id of the image to add the the roadwork schema.
        /// </param>
        /// <returns>
        ///     Instance of <see cref="DataResult{TEntity}"/> containing an <see cref="RoadworkSchemaResponseDto"/> or errors;
        /// </returns>
        Task<DataResult<RoadworkSchemaResponseDto>> CreateRoadworkSchema(RoadworkSchemaCreationRequestDto dto, string imageId);

        /// <summary>
        ///     Override a roadwork schema in the database.
        /// </summary>
        /// <param name="id">
        ///     Id of the roadwork schema to override
        /// </param>
        /// <param name="dto">
        ///     Instance of <see cref="RoadworkSchemaResponseDto"/> containg details about the data to override.
        /// </param>
        /// <returns>
        ///     Instance of <see cref="DataResult{TEntity}"/> containing an <see cref="RoadworkSchemaResponseDto"/> or errors;
        /// </returns>
        Task<DataResult<RoadworkSchemaResponseDto>> PutRoadworkSchema(string id, RoadworkSchemaPutRequestDto dto);

        /// <summary>
        ///     Get all roadwork schemas from the database.
        /// </summary>
        /// <returns>
        ///     Instance of <see cref="IQueryable{T}"/> of <see cref="RoadworkSchemaResponseDto"/>
        /// </returns>
        IQueryable<RoadworkSchemaResponseDto> GetAllRoadworkSchemas();

        /// <summary>
        ///     Delete a roadwork schema from the database.
        /// </summary>
        /// <param name="id">
        ///     Id of the roadwork schema to delete.
        /// </param>
        /// <returns>
        ///     Instance of <see cref="DataResult"/> success or with error if failed.
        /// </returns>
        Task<DataResult> DeleteRoadworkSchema(string id);

    }
}
