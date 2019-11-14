using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Signawel.Business.Abstractions.Services;
using Signawel.Data;
using Signawel.Domain;
using Signawel.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<RoadworkSchemaResponseDto> CreateRoadworkSchema(RoadworkSchemaCreationRequestDto dto)
        {
            var schema = _mapper.Map<RoadworkSchema>(dto);

            await _context.RoadworkSchemas.AddAsync(schema);
            await _context.SaveChangesAsync();
            return _mapper.Map<RoadworkSchemaResponseDto>(schema);
        }

        public async Task<bool> DeleteRoadworkSchema(string id)
        {
            var data = await _context.RoadworkSchemas.FindAsync(id);

            if (data == null)
                return false;

            _context.RoadworkSchemas.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

        public IQueryable<RoadworkSchemaResponseDto> GetAllRoadworkSchemas()
        {
            return _mapper.ProjectTo<RoadworkSchemaResponseDto>(_context.RoadworkSchemas);
        }

        public async Task<RoadworkSchemaResponseDto> GetRoadworkSchema(string id)
        {
            var data = await _context.RoadworkSchemas.FindAsync(id);

            if(data == null)
                return null;

            return _mapper.Map<RoadworkSchemaResponseDto>(data);
        }

        public async Task<RoadworkSchemaResponseDto> PutRoadworkSchema(string id, RoadworkSchemaPutRequestDto dto)
        {
            var currentSchema = await _context.RoadworkSchemas
                .Include(rs => rs.BoundingBoxes).ThenInclude(bb => bb.Points)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(currentSchema == null)
                return null;

            var newSchema = _mapper.Map<RoadworkSchema>(dto);
            newSchema.Id = id;

            _context.RoadworkSchemas.Remove(currentSchema);
            await _context.SaveChangesAsync();
            await _context.RoadworkSchemas.AddAsync(newSchema);
            await _context.SaveChangesAsync();

            return _mapper.Map<RoadworkSchemaResponseDto>(newSchema);
        }
    }
}
