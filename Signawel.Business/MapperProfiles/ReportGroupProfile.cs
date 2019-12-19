using AutoMapper;
using Signawel.Data;
using Signawel.Domain;
using Signawel.Domain.ReportGroups;
using Signawel.Dto.Authentication;
using Signawel.Dto.ReportGroup;
using System.Collections.Generic;
using System.Linq;

namespace Signawel.Business.MapperProfiles
{
    public class ReportGroupProfile : Profile
    {
        public ReportGroupProfile()
        {
            CreateMap<City, CityResponseDto>();
            CreateMap<CityCreationRequestDto, City>();

            CreateMap<Email, EmailResponseDto>();
            CreateMap<EmailCreationRequestDto, Email>();

            CreateMap<User, UserResponseDto>();

            CreateMap<ReportGroup, ReportGroupResponseDto>()
        .ForMember(rgrd => rgrd.CityReportGroups, opt => opt.MapFrom<ReportGroupCitiesResolver>())
        .ForMember(rgrd => rgrd.EmailReportGroups, opt => opt.MapFrom<ReportGroupEmailResolver>())
        .ForMember(rgrd => rgrd.UserReportGroups, opt => opt.MapFrom<ReportGroupUserResolver>());

        }
        public class ReportGroupCitiesResolver : IValueResolver<ReportGroup, ReportGroupResponseDto, List<CityResponseDto>>
        {
            private readonly SignawelDbContext _context;
            private readonly IMapper _mapper;
            public ReportGroupCitiesResolver(SignawelDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public List<CityResponseDto> Resolve(ReportGroup source, ReportGroupResponseDto destination, List<CityResponseDto> destMember, ResolutionContext context)
            {
                var cityReportGroups = _context.CityReportGroups.ToList();
                var reportGroup = cityReportGroups.Where(c => c.ReportGroupId == source.Id);
                var reportGroup2 = reportGroup.Select(r => _mapper.Map<CityResponseDto>(_context.Cities.Find(r.CityId)));
                var result = reportGroup2.ToList();

                return result;
            }

        }

        public class ReportGroupEmailResolver : IValueResolver<ReportGroup, ReportGroupResponseDto, List<EmailResponseDto>>
        {
            private readonly SignawelDbContext _context;
            private readonly IMapper _mapper;
            public ReportGroupEmailResolver(SignawelDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public List<EmailResponseDto> Resolve(ReportGroup source, ReportGroupResponseDto destination, List<EmailResponseDto> destMember, ResolutionContext context)
            {
                var emailReportGroups = _context.EmailReportGroups.ToList();
                var reportGroup = emailReportGroups.Where(c => c.ReportGroupId == source.Id);
                var reportGroup2 = reportGroup.Select(r => _mapper.Map<EmailResponseDto>(_context.Emails.Find(r.EmailId)));
                var result = reportGroup2.ToList();

                return result;
            }
        }

        public class ReportGroupUserResolver : IValueResolver<ReportGroup, ReportGroupResponseDto, List<UserResponseDto>>
        {
            private readonly SignawelDbContext _context;
            private readonly IMapper _mapper;
            public ReportGroupUserResolver(SignawelDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public List<UserResponseDto> Resolve(ReportGroup source, ReportGroupResponseDto destination, List<UserResponseDto> destMember, ResolutionContext context)
            {
                var userReportGroups = _context.UserReportGroups.ToList();
                var reportGroup = userReportGroups.Where(c => c.ReportGroupId == source.Id);
                var reportGroup2 = reportGroup.Select(r => _mapper.Map<UserResponseDto>(_context.Users.Find(r.UserId)));
                var result = reportGroup2.ToList();

                return result;
            }
        }
    }
}

  
