using Newtonsoft.Json;
using Polly;
using Signawel.Dto;
using Signawel.Mobile.Bootstrap.Abstract;
using Signawel.Mobile.Constants;
using Signawel.Mobile.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Signawel.Mobile.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IHttpService _httpService;

        public CategoryService(IHttpService httpService)
        {
            this._httpService = httpService;
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetCategoriesAsync()
        {
            try
            {
                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await _httpService.GetAsync(ApiConstants.GetCategories));

                if (responseMessage.IsSuccessStatusCode)
                {
                    var content = await responseMessage.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<CategoryResponseDto>>(content);
                }

                throw new HttpRequestException();

            } catch(Exception exception)
            {
                Debug.WriteLine($"{ exception.GetType().Name + " : " + exception.Message}");
            }
            return null;
        }
    }
}
