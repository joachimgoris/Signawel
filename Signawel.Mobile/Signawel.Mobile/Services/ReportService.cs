using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Signawel.Dto;
using Signawel.Mobile.Bootstrap.Abstract;
using Signawel.Mobile.Constants;
using Signawel.Mobile.Models;
using Signawel.Mobile.Services.Abstract;
using Xamarin.Forms;

namespace Signawel.Mobile.Services
{
    /// <inheritdoc cref="IReportService"/>
    public class ReportService : IReportService
    {
        private readonly IHttpService _httpService;

        public ReportService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IList<ErrorResponseDto>> AddReport(ReportData reportData)
        {
            var content = await CreatePostContent(reportData);

            var result = await _httpService.PostAsync(ApiConstants.PostReport, content);

            if (result.IsSuccessStatusCode)
            {
                var jsonString = await result.Content.ReadAsStringAsync();
                var errors = JsonConvert.DeserializeObject<IList<ErrorResponseDto>>(jsonString);
                return errors;
            }

            return null;
        }

        /// <summary>
        ///     Converts utility class <see cref="ReportData"/> to <see cref="MultipartFormDataContent"/>
        /// </summary>
        /// <param name="reportData">
        ///     The data to be converted.
        /// </param>
        /// <returns>
        ///     Instance of <see cref="Task{TResult}"/> containing an instance of <see cref="MultipartFormDataContent"/>
        /// </returns>
        private async Task<MultipartFormDataContent> CreatePostContent(ReportData reportData)
        {
            var multipartFormDataContent = new MultipartFormDataContent();

            foreach (var reportDataImage in reportData.Images)
            {
                StreamImageSource streamImageSource = (StreamImageSource)reportDataImage.Source;
                Stream imageSourceStream = await streamImageSource.Stream(CancellationToken.None);

                byte[] bytes;
                using (var ms = new MemoryStream())
                {
                    await imageSourceStream.CopyToAsync(ms);
                    bytes = ms.ToArray();
                }

                var byteContent = new ByteArrayContent(bytes);
                byteContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

                multipartFormDataContent.Add(byteContent, "files", reportDataImage.Source.Id.ToString());
            }

            multipartFormDataContent.Add(new StringContent(JsonConvert.SerializeObject(reportData.Report)), "value");
            return multipartFormDataContent;
        }
    }
}
