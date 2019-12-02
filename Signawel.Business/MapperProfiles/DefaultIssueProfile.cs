using AutoMapper;
using Signawel.Domain.Reports;
using Signawel.Dto;

namespace Signawel.Business.MapperProfiles
{
    public class DefaultIssueProfile : Profile
    {
        public DefaultIssueProfile()
        {
            CreateMap<ReportDefaultIssue, DefaultIssueResponseDto>();
        }
    }
}
