using Newtonsoft.Json;
using Signawel.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Linq;
using System.Windows.Input;
using Signawel.Mobile.Bootstrap.Abstract;
using Signawel.Mobile.Bootstrap;
using Signawel.Mobile.Services.Abstract;

namespace Signawel.Mobile.ViewModels
{
    public class MapPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IHttpService _httpService;
        private readonly IMessageBoxService _messageService;

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public List<RoadWork> RoadWorks { get; set; }

        public GridLength DetailsWorkRowHeight { get; set; }

        public RoadWork SelectedItem { get; set; }

        public Xamarin.Forms.Maps.Map Map { get; private set; }

        public bool SearchbarIsEnabled { get; set; }

        public string SearchbarText { get; set; }

        public int SliderValue { get; set; }

        public int LastRadius { get; set; }

        public ICommand NavigateToListCommand => new AsyncCommand(NavigateToList);

        public ICommand MyLocationResultsCommand => new AsyncCommand(MyLocationResults);

        public ICommand PickerValueChangedCommand => new AsyncCommand(PickerValueChanged);

        private ICommand _ShowRoadWorksCommand;

        public ICommand ShowRoadWorksCommand => _ShowRoadWorksCommand ?? (_ShowRoadWorksCommand = new Command<string>(async (text) =>
        {
            await ShowRoadWorks(text);
        }));

        public bool Loading { get; set; }
        public bool RequestValid { get; set; }
        public double MapOpacity { get; set; }

        public MapPageViewModel(INavigationService navigationService, IHttpService httpService,IMessageBoxService messageService)
        {
            _navigationService = navigationService;
            _httpService = httpService;
            _messageService = messageService;

            ClearMap();
        }

        public void ClearMap()
        {
            MapOpacity = 1;
            RequestValid = false;
            SliderValue = 0;
            SearchbarText = "";
            DetailsWorkRowHeight = 0;
            SearchbarIsEnabled = true;
            Loading = false;
            RoadWorks = new List<RoadWork>();
            Map = new Xamarin.Forms.Maps.Map
            {
                MapType = MapType.Street,
                HasZoomEnabled = true
            };
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(50.928047, 5.336753), Distance.FromKilometers(30)));
        }

        // Calculate distance between two coordinates
        public double CalculateDistanceBetweenCoordinates(double lat1, double long1, double lat2, double long2)
        {
            return Math.Round(Location.CalculateDistance(
                new Location(lat1, long1), new Location(lat2, long2),
                DistanceUnits.Kilometers), 2);
        }

        // Search for roadworks and place them on the map
        public async Task ShowRoadWorks(Object seachbarText)
        {
            Loading = true;
            MapOpacity = 0.2;

            var seachbar = (string)seachbarText;
            if(seachbar.Equals("Mijn locatie"))
            {
                await SetMyLocation();
                if(Latitude == 0 && Longitude == 0)
                {
                    Loading = false;
                    MapOpacity = 1;
                    return;
                }
            }
            // Everytime the user searches at a new place, the previous pins are removed
            foreach (Pin pin in Map.Pins.ToList())
            {
                Map.Pins.Remove(pin);
            }

            RoadWorks.Clear();

            // When the searchbar is enabled the user searchers roadworks by address
            if (!seachbar.Equals("Mijn locatie"))
            {
                var coordinates = JsonConvert.DeserializeObject<AddressToCoordinate>(
                    await AccessTheWebAsync($"http://loc.geopunt.be/v4/Location?q={SearchbarText}"));

                if (coordinates.locationResult.Count == 0)
                {
                    _messageService.ShowAlert("Fout adres!", "Het opgegeven adres bestaat niet of is in een fout formaat ingegeven, zorg ervoor dat u het adres in dit formaat ingeeft : straat nr, stad of enkel een straat/stad");
                    Loading = false;
                    RequestValid = false;
                    MapOpacity = 1;
                    return;
                }

                Latitude = Convert.ToDouble(coordinates.locationResult[0].location.LatWGS84, CultureInfo.InvariantCulture);
                Longitude = Convert.ToDouble(coordinates.locationResult[0].location.LonWGS84, CultureInfo.InvariantCulture);
            }

            var latString = Latitude.ToString(CultureInfo.InvariantCulture);
            var lngString = Longitude.ToString(CultureInfo.InvariantCulture);

            var roadWorksBeforeCheck = JsonConvert.DeserializeObject<List<RoadWork>>(
                await AccessTheWebAsync($"http://api.gipod.vlaanderen.be/ws/v1/WorkAssignment?point={lngString},{latString}&radius={((SliderValue+1)*1000)}"));

            foreach (var roadWork in roadWorksBeforeCheck)
            {
                var distance = CalculateDistanceBetweenCoordinates(Latitude, Longitude, roadWork.Coordinate.coordinates[1], roadWork.Coordinate.coordinates[0]);

                // Sometimes Gipod returns a roadwork that's out of the specified range
                if (distance < (SliderValue+1))
                {
                    RoadWorks.Add(roadWork);
                    roadWork.DistanceToDevice = distance;

                    Pin pin = new Pin
                    {
                        Address = roadWork.Description,
                        Label = roadWork.Cities[0],
                        Position = new Position(roadWork.Coordinate.coordinates[1], roadWork.Coordinate.coordinates[0])
                    };
                    pin.MarkerClicked += RoadWorkTapped;

                    Map.Pins.Add(pin);
                }
            }

            if (Map.Pins.Count == 0)
            {
                _messageService.ShowAlert("Geen werken gevonden", "Er zijn geen werken in de buurt van de gegeven locatie");
                Loading = false;
                RequestValid = true;
                MapOpacity = 1;
                return;
            }
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Latitude, Longitude), Distance.FromKilometers(SliderValue +1)));

            DetailsWorkRowHeight = new GridLength(1.2, GridUnitType.Star);
            LastRadius = SliderValue;
            Loading = false;
            RequestValid = true;
            MapOpacity = 1;
        }

        // Get the devices location
        public async Task SetMyLocation()
        {

                GeolocationRequest request;
                Location location;
                try
                {
                    request = new GeolocationRequest(GeolocationAccuracy.Best);
                    location = await Geolocation.GetLocationAsync(request);
                }
                catch (PermissionException)
                {
                _messageService.ShowAlert("Geen toegang", "De app heeft toegang nodig tot u locatie om werken in u buurt te vinden");
                    return;
                }

                Latitude = location.Latitude;
                Longitude = location.Longitude;


        }

        private async Task MyLocationResults()
        {
            SearchbarText = "Mijn locatie";
            await ShowRoadWorks("Mijn locatie");
        }


        // Navigate to ListViewRoadWorksPage and pass the RoadWork objects
        private async Task NavigateToList()
        {
            await _navigationService.NavigateToAsync<ListViewRoadWorksPageViewModel>(
                RoadWorks.OrderBy(r => r.DistanceToDevice).ToList());
        }

        // When a pin is tapped, add a row to the grid with the details of the roadwork
        private async void RoadWorkTapped(object sender, PinClickedEventArgs e)
        {
            Pin pin = (Pin)sender;
            SelectedItem = RoadWorks.FirstOrDefault(r => r.Description.Equals(pin.Address));

            e.HideInfoWindow = true;

            var ans = await _messageService.ShowYesNoAlert($"Werk: {SelectedItem.GipodId}", $"Wilt u dit werk aan uw report toevoegen?\nbeschrijving: {SelectedItem.Description}", "Ja", "Nee");
            if (ans)
            {
                await _navigationService.NavigateToAsync<ReportViewModel>(SelectedItem);
            }
        }

        private async Task PickerValueChanged()
        {
            if(RequestValid && SliderValue != LastRadius)
            {
                await ShowRoadWorks(SearchbarText);
            }
        }


        #region PrivateMethods

        // Method to make Http-requests
        private async Task<string> AccessTheWebAsync(string url)
        {
            var service = await _httpService.GetAsync(url);
            var content = service.Content;
            var text = content.ReadAsStringAsync();
            return text.Result;
        }

        #endregion
    }
}