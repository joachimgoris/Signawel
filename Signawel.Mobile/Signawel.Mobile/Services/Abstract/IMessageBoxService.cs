using System;
using System.Threading.Tasks;

namespace Signawel.Mobile.Services.Abstract
{
    public interface IMessageBoxService
    {
        /// <summary>
        ///     Creates a pop up and shows it on the users device
        /// </summary>
        /// <param name="title">
        ///     Title that displays at the top of the pop up.
        /// </param>
        /// <param name="message">
        ///     The message to be shown to the user
        /// </param>
        /// <param name="onClosed">
        ///     The action of the alert when it is closed. Standard value = null
        /// </param>
        void ShowAlert(string title, string message, Action onClosed = null);

        /// <summary>
        ///     Creates a pop up with options the user can choose from
        /// </summary>
        /// <param name="title">
        ///     Title that displays at the top of the pop up.
        /// </param>
        /// <param name="cancel">
        ///     The text to display for the cancel button
        /// </param>
        /// <param name="destruction">
        ///     The text to display for the remove button
        /// </param>
        /// <param name="buttons">
        ///     Buttons are created according to the array and displayed
        /// </param>
        /// <returns>
        ///     An instance of <see cref="Task{TResult}"/> containing an instance of <see cref="string"/>. Can be used to determine what options was clicked by the user.
        /// </returns>
        Task<string> ShowActionSheet(string title, string cancel, string destruction, string[] buttons = null);
    }
}
