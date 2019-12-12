using System;
using System.Threading.Tasks;
using Signawel.Mobile.Constants;
using Signawel.Mobile.Services.Abstract;
using Xamarin.Forms;

namespace Signawel.Mobile.Services
{
    /// <inheritdoc cref="IMessageBoxService"/>
    public class MessageBoxService : IMessageBoxService
    {
        private static Page CurrentMainPage => Application.Current.MainPage;

        public async void ShowAlert(string title, string message, Action onClosed = null)
        {
            await CurrentMainPage.DisplayAlert(title, message, TextConstants.OkButton);
            onClosed?.Invoke();
        }

        public async Task<bool> ShowYesNoAlert(string title, string message, string accept, string deny)
        {
            var response = await CurrentMainPage.DisplayAlert(title, message, accept,deny);
            return response;
        }

        public async Task<string> ShowActionSheet(string title, string cancel, string destruction, string[] buttons = null)
        {
            var displayButtons = buttons ?? new string[] { };
            var action = await CurrentMainPage.DisplayActionSheet(title, cancel, destruction, displayButtons);
            return action;
        }
    }
}
