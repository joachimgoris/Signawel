using PropertyChanged;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Signawel.Mobile.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public virtual Task InitializeAsync(object data)
        {
            return Task.FromResult(false);
        }
    }
}
