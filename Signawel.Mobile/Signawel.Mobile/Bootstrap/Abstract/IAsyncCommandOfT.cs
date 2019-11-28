using System.Threading.Tasks;
using System.Windows.Input;

namespace Signawel.Mobile.Bootstrap.Abstract
{
    public interface IAsyncCommand<in T> : ICommand
    {
        Task ExecuteAsync(T parameter);
        bool CanExecute(T parameter);
    }
}
