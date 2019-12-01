using AutoMapper;
using Signawel.Domain.Reports;
using Signawel.Dto.Reports;

namespace Signawel.Business.MapperProfiles
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<ReportCreationRequestDto, Report>();
            CreateMap<Report, ReportResponseDto>();
        }
    }
}
