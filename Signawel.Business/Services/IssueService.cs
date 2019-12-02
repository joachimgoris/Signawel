using System.Linq;
using AutoMapper;
using Signawel.Business.Abstractions.Services;
using Signawel.Data;
using Signawel.Dto;

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
    }
}
