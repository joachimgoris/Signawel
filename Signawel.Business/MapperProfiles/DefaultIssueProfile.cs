using AutoMapper;
using Signawel.Domain.Reports;
using Signawel.Dto.DefaultIssue;

namespace Signawel.Business.MapperProfiles
{
    public class DefaultIssueProfile : Profile
    {
        public DefaultIssueProfile()
        {
            CreateMap<ReportDefaultIssue, DefaultIssueResponseDto>();
            CreateMap<DefaultIssueRequestDto, ReportDefaultIssue>();
        }
    }
}
