using AutoMapper;

namespace Signawel.Business.MapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            new UserProfile();
            new DeterminationGraphProfile();
            new DeterminationNodeProfile();
            new DeterminationAnswerProfile();
            new ImageProfile();
            new RoadworkSchemaProfile();
            new BBoxProfile();
            new BBoxPointProfile();
            new ReportProfile();
            new PriorityEmailProfile();
        }
    }
}
