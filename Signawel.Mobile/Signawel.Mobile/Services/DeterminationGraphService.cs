using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Signawel.Dto.Determination;
using Signawel.Mobile.Bootstrap.Abstract;
using Signawel.Mobile.Constants;
using Signawel.Mobile.Services.Abstract;

namespace Signawel.Mobile.Services
{
    public class DeterminationGraphService : IDeterminationGraphService
    {
        private readonly IHttpService _httpService;


        public DeterminationGraphService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<DeterminationGraphResponseDto> GetDeterminationGraph()
        {
            HttpResponseMessage response;
            try
            {
                response = await _httpService.GetAsync(ApiConstants.GetDeterminationGraph);
            }
            catch(Exception e)
            {
                // Handle error: failed to get data
                return null;
            }

            if(response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<DeterminationGraphResponseDto>(content);
            }

            return null;
        }
    }
}