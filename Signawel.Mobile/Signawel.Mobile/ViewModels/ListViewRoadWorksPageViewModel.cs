using Signawel.Mobile.Bootstrap.Abstract;
using Signawel.Mobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Signawel.Mobile.Bootstrap;

namespace Signawel.Mobile.ViewModels
{
    public class ListViewRoadWorksPageViewModel : ViewModelBase
    {

        public List<RoadWork> RoadWorks { get; set; }

        public ICommand SelectedCommand => new AsyncCommand<RoadWork>(async (r) => await _navigationService.NavigateToAsync<ReportViewModel>(r));

        private readonly INavigationService _navigationService;

        public ListViewRoadWorksPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }


        public override Task InitializeAsync(object data)
        {
            RoadWorks = data as List<RoadWork>;

            return Task.CompletedTask;
        }
    }

    
}
