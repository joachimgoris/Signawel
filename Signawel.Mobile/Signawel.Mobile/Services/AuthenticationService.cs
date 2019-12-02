using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Signawel.Dto.Authentication;
using Signawel.Mobile.Bootstrap.Abstract;
using Signawel.Mobile.Constants;
using Signawel.Mobile.Services.Abstract;
using Signawel.Mobile.ViewModels;
using Signawel.MobileData;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Signawel.Mobile.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpService _httpService;
        private readonly SignawelMobileContext _context;
        private readonly INavigationService _navigationService;

        public AuthenticationService(IHttpService httpService, SignawelMobileContext context, INavigationService navigationService)
        {
            _httpService = httpService;
            _context = context;
            _navigationService = navigationService;
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

            if (response == null)
                return null;

            var tokenResponseDto = JsonConvert.DeserializeObject<TokenResponseDto>(response);
            await _httpService.SetTokens(tokenResponseDto);

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

            if (response == null)
                return null;

            RegisterResponseDto registerResponseDto = JsonConvert.DeserializeObject<RegisterResponseDto>(response);
            return registerResponseDto;
        }

        /// <inheritdoc cref="IAuthenticationService.IsAuthenticatedAsync"/>
        public async Task<bool> IsAuthenticatedAsync()
        {
            var token = (await _context.DbToken.FirstOrDefaultAsync())?.Token;

            if(token == null)
            {
                return false;
            }

            return true;
        }
        public async Task Logout()
        {
            var tokens = await _context.DbToken.FirstOrDefaultAsync();

            if(tokens == null)
            {
                return;
            }

            _context.DbToken.Remove(tokens);
            await _context.SaveChangesAsync();

            await _navigationService.NavigateToAsync<MainViewModel>();
        }

        #region Private Methods

        private async Task<string> PostAuthAsync(string endpoint, object body)
        {
            var json = JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpService.PostAsync(endpoint, content);

            if(response == null || (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent))
            {
                return null;
            }

            return await response.Content.ReadAsStringAsync();
        }

        #endregion
    }
}
