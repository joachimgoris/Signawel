using AutoMapper;
using Signawel.Business.Abstractions.Services;
using Signawel.Domain.Reports;
using Signawel.Dto;
using Signawel.Dto.Reports;

namespace Signawel.Business.MapperProfiles
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<ReportCreationRequestDto, Report>();
            CreateMap<Report, ReportResponseDto>()
                .ForMember(e => e.Issue, opt => opt.MapFrom<ReportDefaultIssueResolver>());
            CreateMap<ReportImage, ReportImageResponseDto>();
        }

        public class ReportDefaultIssueResolver : IValueResolver<Report, ReportResponseDto, DefaultIssueResponseDto>
        {
            private readonly IIssueService _issueService;

            public ReportDefaultIssueResolver(IIssueService issueService)
            {
                _issueService = issueService;
            }

            public DefaultIssueResponseDto Resolve(Report source, ReportResponseDto destination, DefaultIssueResponseDto destMember, ResolutionContext context)
            {
                if(string.IsNullOrEmpty(source.IssueId))
                {
                    return new DefaultIssueResponseDto
                    {
                        Name = "Unknown"
                    };
                }

                var result = _issueService.GetDefaultIssue(source.IssueId).Result;

                if(!result.Succeeded)
                {
                    return new DefaultIssueResponseDto
                    {
                        Name = "Unknown"
                    };
                }

                return result.Entity;
            }
        }

    }
}
