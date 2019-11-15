using Newtonsoft.Json;
using Signawel.Dto.Authentication;
using Signawel.Mobile.Bootstrap.Abstract;
using Signawel.MobileData;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Signawel.Mobile.Bootstrap
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService()
        {
            // TODO: Acquire a legit SSL cert
            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            _httpClient = new HttpClient(clientHandler);
        }

        public void SetToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            if (string.IsNullOrEmpty(url))
                return null;

            var response = await _httpClient.GetAsync(url);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await AttemptTokenRefreshAsync();
                response = await _httpClient.GetAsync(url);
            }
            return response;
        }

        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            if (CheckIfNull(url, content))
                return null;

            var response = await _httpClient.PostAsync(url, content);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await AttemptTokenRefreshAsync();
                response = await _httpClient.PostAsync(url, content);
            }
            return response;
        }

        public async Task<HttpResponseMessage> PutAsync(string url, HttpContent content)
        {
            if (CheckIfNull(url, content))
                return null;

            var response = await _httpClient.PutAsync(url, content);

            if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await AttemptTokenRefreshAsync();
                response = await _httpClient.PutAsync(url, content);
            }
            return response;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            if (string.IsNullOrEmpty(url))
                return null;

            var response = await _httpClient.DeleteAsync(url);

            if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await AttemptTokenRefreshAsync();
                response = await _httpClient.DeleteAsync(url);
            }
            return response;
        }

        #region Private Methods

        private bool CheckIfNull(string url, HttpContent content)
        {
            if (string.IsNullOrEmpty(url) || content == null)
                return false;
            return true;
        }

        private async Task AttemptTokenRefreshAsync()
        {
            var oldToken = _httpClient.DefaultRequestHeaders.Authorization.Parameter;
            RefreshToken refreshToken;
            using (var context = new SignawelMobileContext())
            {
                refreshToken = context.RefreshTokens.FirstOrDefault();
            }

            if (string.IsNullOrEmpty(oldToken.ToString()))
            {
                var model = new RefreshRequestDto
                {
                    JwtToken = oldToken.ToString(),
                    RefreshToken = refreshToken.Token
                };

                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("authentication/refresh", content);

                var tokenResponseDto = JsonConvert.DeserializeObject<TokenResponseDto>(response.Content.ReadAsStringAsync().Result);
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenResponseDto.Token);
            }
        }

        #endregion
    }
}
