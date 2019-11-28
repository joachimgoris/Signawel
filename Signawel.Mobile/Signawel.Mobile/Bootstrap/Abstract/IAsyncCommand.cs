using System.Threading.Tasks;
using System.Windows.Input;

namespace Signawel.Mobile.Bootstrap.Abstract
{
    public interface IAsynCommand : ICommand
    {
        Task ExecuteAsync();
        bool CanExecute();
    }
}
