using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Signawel.Business.Abstractions.Services;
using Signawel.Data;
using Signawel.Domain.Constants;
using Signawel.Domain.DataResults;
using Signawel.Domain.Reports;
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

        public IQueryable<DefaultIssueResponseDto> GetDefaultIssues()
        {
            return _mapper.ProjectTo<DefaultIssueResponseDto>(_context.DefaultIssues);
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
    }
}
