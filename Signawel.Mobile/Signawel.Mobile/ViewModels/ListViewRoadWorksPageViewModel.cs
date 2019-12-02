using Signawel.Mobile.Bootstrap.Abstract;
using Signawel.Mobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Signawel.Mobile.ViewModels
{
    public class ListViewRoadWorksPageViewModel : ViewModelBase
    {

        public List<RoadWork> RoadWorks { get; set; }
        private RoadWork _SelectedRoadWork;
        public RoadWork SelectedRoadWork
        {
            get
            {
                return _SelectedRoadWork;
            }
            set
            {
                _SelectedRoadWork = value;
                _navigationService.NavigateToAsync<ReportViewModel>(SelectedRoadWork);
            }
        }

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
