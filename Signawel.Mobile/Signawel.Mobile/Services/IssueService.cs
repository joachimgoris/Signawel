using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Signawel.Dto;
using Signawel.Mobile.Bootstrap.Abstract;
using Signawel.Mobile.Constants;
using Signawel.Mobile.Services.Abstract;

namespace Signawel.Mobile.Services
{
    /// <inheritdoc cref="IIssueService"/>
    public class IssueService : IIssueService
    {
        private readonly IHttpService _httpService;

        public IssueService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IList<DefaultIssueResponseDto>> GetAllDefaultIssues()
        {
            var result = await _httpService.GetAsync(ApiConstants.GetDefaultIssues);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IList<DefaultIssueResponseDto>>(jsonString);
            }

            return null;
        }
    }
}
