using Newtonsoft.Json;
using Signawel.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Linq;
using System.Windows.Input;
using Signawel.Mobile.Bootstrap.Abstract;
using Signawel.Mobile.Bootstrap;

namespace Signawel.Mobile.ViewModels
{
    public class MapPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IHttpService _httpService;

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public List<RoadWork> RoadWorks { get; set; }

        public GridLength DetailsWorkRowHeight { get; set; }

        public RoadWork SelectedItem { get; set; }

        public Xamarin.Forms.Maps.Map Map { get; private set; }

        public bool SearchbarIsEnabled { get; set; }

        public string SearchbarText { get; set; }

        public int SliderValue { get; set; }

        public string SliderText => $"Zoeken werken in een straal van {SliderValue}km rond de gegeven locatie.";

        public bool SeeListButtonIsVisible { get; set; }

        public float LocationButtonOpacity { get; set; }

        public ICommand SetMyLocationCommand => new AsyncCommand(SetMyLocation);

        public ICommand ShowRoadWorksCommand => new AsyncCommand(ShowRoadWorks);

        public ICommand NavigateToListCommand => new AsyncCommand(NavigateToList);



        public MapPageViewModel(INavigationService navigationService, IHttpService httpService)
        {
            _navigationService = navigationService;
            _httpService = httpService;

            SliderValue = 1;
            SearchbarText = "";
            DetailsWorkRowHeight = 0;
            LocationButtonOpacity = 1;
            SeeListButtonIsVisible = false;
            SearchbarIsEnabled = true;
            RoadWorks = new List<RoadWork>();
            Map = new Xamarin.Forms.Maps.Map
            {
                MapType = MapType.Street,
                HasZoomEnabled = true
            };
        }


        // Calculate distance between two coordinates
        public double CalculateDistanceBetweenCoordinates(double lat1, double long1, double lat2, double long2)
        {
            return Math.Round(Location.CalculateDistance(
                new Location(lat1, long1), new Location(lat2, long2),
                DistanceUnits.Kilometers), 2);
        }

        // Search for roadworks and place them on the map
        public async Task ShowRoadWorks()
        {
            // Everytime the user searches at a new place, the previous pins are removed
            foreach (Pin pin in Map.Pins)
            {
                Map.Pins.Remove(pin);
            }

            // When the searchbar is enabled the user searchers roadworks by address
            if (SearchbarIsEnabled)
            {
                var coordinates = JsonConvert.DeserializeObject<AddressToCoordinate>(
                    await AccessTheWebAsync($"http://loc.geopunt.be/v4/Location?q={SearchbarText}"));

                if (coordinates.locationResult.Count == 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Fout adres!", "Het opgegeven adres bestaat niet of is in een fout formaat ingegeven, zorg ervoor dat u het adres in dit formaat ingeeft : straat nr, stad of enkel een straat/stad", "OK");
                    return;
                }

                Latitude = Convert.ToDouble(coordinates.locationResult[0].location.LatWGS84);
                Longitude = Convert.ToDouble(coordinates.locationResult[0].location.LonWGS84);
            }

            var roadWorksBeforeCheck = JsonConvert.DeserializeObject<List<RoadWork>>(
                await AccessTheWebAsync("http://api.gipod.vlaanderen.be/ws/v1/WorkAssignment?point={Longitude},{Latitude}&radius={(SliderValue*1000)}"));

            foreach (var roadWork in roadWorksBeforeCheck)
            {
                var distance = CalculateDistanceBetweenCoordinates(Latitude, Longitude, roadWork.coordinate.coordinates[1], roadWork.coordinate.coordinates[0]);

                // Sometimes Gipod returns a roadwork that's out of the specified range
                if (distance < SliderValue)
                {
                    RoadWorks.Add(roadWork);
                    roadWork.distanceToDevice = distance;

                    Pin pin = new Pin
                    {
                        Address = roadWork.description,
                        Label = roadWork.cities[0],
                        Position = new Position(roadWork.coordinate.coordinates[1], roadWork.coordinate.coordinates[0])
                    };
                    pin.MarkerClicked += RoadWorkTapped;

                    Map.Pins.Add(pin);
                }
            }

            if (Map.Pins.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Geen werken gevonden", "Er zijn geen werken in de buurt van de gegeven locatie", "OK");
                return;
            }

            SeeListButtonIsVisible = true;
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Latitude, Longitude), Distance.FromKilometers(6)));
        }

        // Get the devices location
        public async Task SetMyLocation()
        {
            if (SearchbarIsEnabled)
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Best);
                var location = await Geolocation.GetLocationAsync(request);

                Latitude = location.Latitude;
                Longitude = location.Longitude;

                Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Latitude, Longitude), Distance.FromKilometers(2)));

                SearchbarText = "Mijn Locatie";
                SearchbarIsEnabled = false;
                LocationButtonOpacity = (float)0.5;
            }
            else
            {
                SearchbarText = "";
                SearchbarIsEnabled = true;
                LocationButtonOpacity = 1;
            }
        }

        // Navigate to ListViewRoadWorksPage and pass the RoadWork objects
        private async Task NavigateToList()
        {
            await _navigationService.NavigateToAsync<ListViewRoadWorksPageViewModel>(
                RoadWorks.OrderBy(r => r.distanceToDevice).ToList());
        }

        // When a pin is tapped, add a row to the grid with the details of the roadwork
        private void RoadWorkTapped(object sender, PinClickedEventArgs e)
        {
            Pin pin = (Pin)sender;
            
            SelectedItem = RoadWorks.Where(r => r.description.Equals(pin.Address)).FirstOrDefault();
            DetailsWorkRowHeight = new GridLength(1.2, GridUnitType.Star);
            e.HideInfoWindow = true;
        }

        #region PrivateMethods

        // Method to make Http-requests
        private async Task<string> AccessTheWebAsync(string url)
        {
            return await _httpService.GetAsync(url)
                .Result
                .Content
                .ReadAsStringAsync();
        }

        #endregion
    }
}