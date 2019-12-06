using Newtonsoft.Json;
using Signawel.Mobile.Services.Abstract;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Signawel.Dto.RoadworkSchema;
using Signawel.Mobile.Bootstrap.Abstract;
using Signawel.Mobile.Constants;
using Signawel.Domain.Enums;
using System.Collections.Generic;

namespace Signawel.Mobile.Services
{
    public class RoadworkSchemaService : IRoadworkSchemaService
    {
        private readonly IHttpService _httpService;

        public RoadworkSchemaService(IHttpService httpService)
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

        public async Task<IList<RoadworkSchemaResponseDto>> GetRoadworkSchemaByCategory(RoadworkCategory category)
        {
            var response = await _httpService.GetAsync(ApiConstants.GetAllRoadworkSchemasByCategory(category));
            var responseContent = await response.Content.ReadAsStringAsync();
            var paginationResponse = JsonConvert.DeserializeObject<RoadworkSchemaPaginationResponseDto>(responseContent);
            return paginationResponse.Schemas;
        }
    }
}
