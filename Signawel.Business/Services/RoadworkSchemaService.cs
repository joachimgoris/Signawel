using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Signawel.Business.Abstractions.Services;
using Signawel.Data;
using Signawel.Domain;
using Signawel.Dto;
using System;
using System.Linq;
using System.Threading.Tasks;
using Signawel.Domain.Constants;
using Signawel.Dto.RoadworkSchema;
using Signawel.Domain.DataResults;

namespace Signawel.Business.Services
{
    public class RoadworkSchemaService : IRoadworkSchemaService
    {
        private readonly SignawelDbContext _context;
        private readonly IMapper _mapper;

        public RoadworkSchemaService(SignawelDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        /// <inheritdoc cref="IRoadworkSchemaService.CreateRoadworkSchema(RoadworkSchemaCreationRequestDto, string)"/>
        public async Task<DataResult<RoadworkSchemaResponseDto>> CreateRoadworkSchema(
            RoadworkSchemaCreationRequestDto dto, string imageId)
        {
            if (dto == null || string.IsNullOrEmpty(imageId))
                return DataResult<RoadworkSchemaResponseDto>.WithPublicError(ErrorCodes.ParameterEmptyError,
                    "Model or imageId is empty.");

            var schema = _mapper.Map<RoadworkSchema>(dto);

            schema.ImageId = imageId;

            try
            {
                await _context.RoadworkSchemas.AddAsync(schema);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return DataResult<RoadworkSchemaResponseDto>.WithError(ErrorCodes.RoadworkSchemaCreationError,
                    "Failed to create roadworkschema.", DataErrorVisibility.Public);
            }

            var result = _mapper.Map<RoadworkSchemaResponseDto>(schema);
            return DataResult<RoadworkSchemaResponseDto>.Success(result);
        }

        /// <inheritdoc cref="IRoadworkSchemaService.DeleteRoadworkSchema(string)" />
        public async Task<DataResult> DeleteRoadworkSchema(string id)
        {
            if (string.IsNullOrEmpty(id))
                return DataResult.WithPublicError(ErrorCodes.ParameterEmptyError, "Id is empty");

            var data = await _context.RoadworkSchemas.FindAsync(id);

            if (data == null)
                return DataResult.WithPublicError(ErrorCodes.NotFoundError, "Roadwork schema not found");

            _context.RoadworkSchemas.Remove(data);
            await _context.SaveChangesAsync();
            return DataResult.Success;
        }

        /// <inheritdoc/>
        public IQueryable<RoadworkSchemaResponseDto> GetAllRoadworkSchemas()
        {
            return _mapper.ProjectTo<RoadworkSchemaResponseDto>(_context.RoadworkSchemas);
        }

        /// <inheritdoc/>
        public async Task<DataResult<RoadworkSchemaResponseDto>> GetRoadworkSchema(string id)
        {
            if(string.IsNullOrEmpty(id))
                return DataResult<RoadworkSchemaResponseDto>.WithError(ErrorCodes.ParameterEmptyError, "Id is empty.");

            var data = await _context.RoadworkSchemas.FindAsync(id);

            if (data == null)
                return DataResult<RoadworkSchemaResponseDto>.WithError(ErrorCodes.RoadworkSchemaNotFoundError,
                    $"No roadwork schema found with id '{id}'.", DataErrorVisibility.Public);

            var response = _mapper.Map<RoadworkSchemaResponseDto>(data);
            return DataResult<RoadworkSchemaResponseDto>.Success(response);
        }

        /// <inheritdoc/>
        public async Task<DataResult<RoadworkSchemaResponseDto>> PutRoadworkSchema(string id,
            RoadworkSchemaPutRequestDto dto)
        {
            var currentSchema = await _context.RoadworkSchemas
                .Include(rs => rs.BoundingBoxes).ThenInclude(bb => bb.Points)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (currentSchema == null)
                return DataResult<RoadworkSchemaResponseDto>.WithError("RoadworkSchemaNotFound",
                    $"No roadwork schema found with id '{id}'.", DataErrorVisibility.Public);

            var newSchema = _mapper.Map<RoadworkSchema>(dto);
            newSchema.Id = id;
            newSchema.ImageId = currentSchema.ImageId;

            _context.RoadworkSchemas.Remove(currentSchema);
            await _context.SaveChangesAsync();
            await _context.RoadworkSchemas.AddAsync(newSchema);
            await _context.SaveChangesAsync();

            return DataResult<RoadworkSchemaResponseDto>.Success(_mapper.Map<RoadworkSchemaResponseDto>(newSchema));
        }
    }
}