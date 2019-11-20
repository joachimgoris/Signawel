using System.Threading.Tasks;
using System.Windows.Input;

namespace Signawel.SignawelCommand.Abstractions
{
    public interface IAsynCommand : ICommand
    {
        Task ExecuteAsync();
        bool CanExecute();
    }
}
