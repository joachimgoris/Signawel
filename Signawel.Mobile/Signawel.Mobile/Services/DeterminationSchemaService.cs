using Newtonsoft.Json;
using Signawel.Mobile.Services.Abstract;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Signawel.Dto.RoadworkSchema;
using Signawel.Mobile.Bootstrap.Abstract;
using Signawel.Mobile.Constants;

namespace Signawel.Mobile.Services
{
    public class DeterminationSchemaService : IDeterminationSchemaService
    {
        private readonly IHttpService _httpService;

        public DeterminationSchemaService(IHttpService httpService)
        {
            this._httpService = httpService;
        }

        public async Task<RoadworkSchemaResponseDto> GetRoadworkSchema(string id)
        {
            HttpResponseMessage response;
            try
            {
                response = await _httpService.GetAsync(ApiConstants.GetRoadworkSchema(id));
            }
            catch(Exception)
            {
                // Handle error: failed to get data
                return null;
            }

            if(response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<RoadworkSchemaResponseDto>(content);
            }

            return null;
        }
    }
}
