using Newtonsoft.Json;
using Signawel.Dto.Authentication;
using Signawel.Mobile.Bootstrap.Abstract;
using Signawel.MobileData;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Signawel.Mobile.Bootstrap
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly SignawelMobileContext _context;

        public HttpService(SignawelMobileContext context)
        {
            // TODO: Acquire a legit SSL cert
            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            _httpClient = new HttpClient(clientHandler);
            _context = context;
        }

        public void InitAuthHeader(DbToken dbToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", dbToken.Token);
        }

        public async Task SetTokens(TokenResponseDto dto)
        {
            var dbToken = _context.DbToken.FirstOrDefault();

            if (dbToken == null)
                dbToken = new DbToken();

            dbToken.Token = dto.Token;
            dbToken.RefreshToken = dto.RefreshToken;
            await _context.SaveChangesAsync();

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", dto.Token);
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            if (string.IsNullOrEmpty(url))
                return null;

            var response = await _httpClient.GetAsync(url);

            if (response.StatusCode == HttpStatusCode.Unauthorized && await AttemptTokenRefreshAsync())
            {
                response = await _httpClient.GetAsync(url);
            }
            return response;
        }

        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            if (CheckIfNull(url, content))
                return null;

            var response = await _httpClient.PostAsync(url, content);

            if (response.StatusCode == HttpStatusCode.Unauthorized  && await AttemptTokenRefreshAsync())
            {
                response = await _httpClient.PostAsync(url, content);
            }
            return response;
        }

        public async Task<HttpResponseMessage> PutAsync(string url, HttpContent content)
        {
            if (CheckIfNull(url, content))
                return null;

            var response = await _httpClient.PutAsync(url, content);

            if(response.StatusCode == HttpStatusCode.Unauthorized  && await AttemptTokenRefreshAsync())
            {
                response = await _httpClient.PutAsync(url, content);
            }
            return response;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            if (string.IsNullOrEmpty(url))
                return null;

            var response = await _httpClient.DeleteAsync(url);

            if(response.StatusCode == HttpStatusCode.Unauthorized && await AttemptTokenRefreshAsync())
            {
                response = await _httpClient.DeleteAsync(url);
            }
            return response;
        }

        public async Task<byte[]> GetByteArrayAsync(string url)
        {
            return await _httpClient.GetByteArrayAsync(url);
        }

        #region Private Methods

        private bool CheckIfNull(string url, HttpContent content)
        {
            if (string.IsNullOrEmpty(url) || content == null)
                return true;
            return false;
        }

        private async Task<bool> AttemptTokenRefreshAsync()
        {
            DbToken dbToken = _context.DbToken.FirstOrDefault();

            if (dbToken == null)
            {
                return false;
            }

            var model = new RefreshRequestDto
            {
                JwtToken = dbToken.Token,
                RefreshToken = dbToken.RefreshToken
            };

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("authentication/refresh", content);

            if (response == null || response.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            var tokenResponseDto = JsonConvert.DeserializeObject<TokenResponseDto>(await response.Content.ReadAsStringAsync());
            await SetTokens(tokenResponseDto);
            return true;
        }

        #endregion
    }
}
