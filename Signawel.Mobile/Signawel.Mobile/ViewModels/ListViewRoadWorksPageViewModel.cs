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
                //TODO navigate back to report
            }
        }

        private readonly INavigationService _navigationService;

        public ListViewRoadWorksPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }


        public override async Task InitializeAsync(object data)
        {
            RoadWorks = data as List<RoadWork>;
        }
    }

    
}
