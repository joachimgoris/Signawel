using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Signawel.Business.Abstractions.Services;
using Signawel.Data;
using Signawel.Domain.Constants;
using Signawel.Domain.DataResults;
using Signawel.Domain.Reports;
using Signawel.Dto;
using Signawel.Dto.DefaultIssue;

namespace Signawel.Business.Services
{
    /// <inheritdoc cref="IIssueService"/>
    public class IssueService : IIssueService
    {
        private readonly SignawelDbContext _context;
        private readonly IMapper _mapper;

        public IssueService(SignawelDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IList<DefaultIssueResponseDto>> GetDefaultIssues()
        {
            var list = await _context.DefaultIssues.ToListAsync();
            return _mapper.Map<IList<DefaultIssueResponseDto>>(list);
        }
        
        public async Task<DataResult<DefaultIssueResponseDto>> AddDefaultIsueAsync(DefaultIssueRequestDto dto)
        {
            try
            {
                var toSave = _mapper.Map<ReportDefaultIssue>(dto);
                await _context.DefaultIssues.AddAsync(toSave);
                await _context.SaveChangesAsync();
                return DataResult<DefaultIssueResponseDto>.Success(_mapper.Map<DefaultIssueResponseDto>(toSave));
            } catch
            {
                return DataResult<DefaultIssueResponseDto>.WithError(ErrorCodes.DefaultIssueCreationError, "Failed to create default issue");
            }
        }

        public async Task<DataResult> DeleteDefaultIssueAsync(string id)
        {
            var toDelete = _context.DefaultIssues.Find(id);

            if (toDelete == null)
            {
                return DataResult.WithPublicError(ErrorCodes.NotFoundError, $"No default issue found with id '{id}'");
            }

            try
            {
                _context.DefaultIssues.Remove(toDelete);
                await _context.SaveChangesAsync();
                return DataResult.Success;
            } catch
            {
                return DataResult.WithError(ErrorCodes.DefaultIssueDeletionError, "Failed to delete default issue");
            }
        }

        public async Task<DataResult<DefaultIssueResponseDto>> GetDefaultIssue(string id)
        {
            var result = await _context.DefaultIssues.FindAsync(id);

            if (result == null)
            {
                return DataResult<DefaultIssueResponseDto>.WithPublicError(ErrorCodes.NotFoundError, $"No default issue found with id '{id}'");
            }
            
            return DataResult<DefaultIssueResponseDto>.Success(_mapper.Map<DefaultIssueResponseDto>(result));
        }
    }
}
