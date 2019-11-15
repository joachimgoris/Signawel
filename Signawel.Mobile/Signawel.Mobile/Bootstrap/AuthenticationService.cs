using Newtonsoft.Json;
using Signawel.Dto.Authentication;
using Signawel.Mobile.Bootstrap.Abstract;
using Signawel.Mobile.Constants;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Signawel.Mobile.Bootstrap
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpService _httpService;

        public AuthenticationService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        /// <inheritdoc cref="IAuthenticationService.LoginAsync(string, string)"/>
        public async Task<TokenResponseDto> LoginAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var model = new LoginRequestDto
            {
                Email = email,
                Password = password
            };

            var response = await PostAuthAsync(ApiConstants.LoginEndpoint, model);

            TokenResponseDto tokenResponseDto = JsonConvert.DeserializeObject<TokenResponseDto>(response);

            _httpService.SetToken(tokenResponseDto.Token);

            return tokenResponseDto;
        }

        /// <inheritdoc cref="IAuthenticationService.RegisterAsync(string, string, string)"/>
        public async Task<RegisterResponseDto> RegisterAsync(string email, string password, string passwordRepeat)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(passwordRepeat))
                return null;

            var model = new RegisterRequestDto
            {
                Email = email,
                Password = password,
                PasswordRepeat = passwordRepeat
            };

            var response = await PostAuthAsync(ApiConstants.RegisterEndpoint, model);

            RegisterResponseDto registerResponseDto = JsonConvert.DeserializeObject<RegisterResponseDto>(response);
            return registerResponseDto;
        }

        #region Private Methods

        private async Task<string> PostAuthAsync(string endpoint, object body)
        {
            var json = JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpService.PostAsync(endpoint, content);
            return await response.Content.ReadAsStringAsync();
        }

        #endregion
    }
}
