using Signawel.Domain.Enums;
using Signawel.Dto.RoadworkSchema;
using Signawel.Mobile.Bootstrap;
using Signawel.Mobile.Bootstrap.Abstract;
using Signawel.Mobile.Constants;
using Signawel.Mobile.Services.Abstract;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Signawel.Mobile.ViewModels
{
    public class RoadworkSchemaListViewModel : ViewModelBase
    {
        private readonly IRoadworkSchemaService _roadworkSchemaService;
        private readonly INavigationService _navigationService;
        private readonly IHttpService _httpService;

        public ObservableCollection<RoadworkSchemaListItem> Schemas { get; set; }

        public ICommand SelectRoadworkSchemaCommand => new AsyncCommand<RoadworkSchemaResponseDto>(OnSelectRoadworkSchemaCommand);

        public RoadworkSchemaListViewModel(IRoadworkSchemaService roadworkSchemaService, INavigationService navigationService, IHttpService httpService)
        {
            _roadworkSchemaService = roadworkSchemaService;
            _navigationService = navigationService;
            _httpService = httpService;

            Schemas = new ObservableCollection<RoadworkSchemaListItem>();
        }

        public override async Task InitializeAsync(object data)
        {
            if(data == null)
            {
                throw new ArgumentNullException();
            }

            if(!(data is RoadworkCategory category))
            {
                throw new ArgumentException("argument data should be of type 'RoadworkCategory'");
            }

            var schemas = await _roadworkSchemaService.GetRoadworkSchemaByCategory(category);

            foreach(var schema in schemas)
            {
                var ms = new MemoryStream(await _httpService.GetByteArrayAsync(ApiConstants.GetImage(schema.ImageId)));
                
                Schemas.Add(new RoadworkSchemaListItem
                {
                    Schema = schema,
                    ImageSource = ImageSource.FromStream(() => ms)
                });
            }
        }

        private async Task OnSelectRoadworkSchemaCommand(RoadworkSchemaResponseDto schema)
        {
            await _navigationService.NavigateToAsync<InteractiveSketchViewModel>(schema);
        }

    }

    public class RoadworkSchemaListItem : ViewModelBase
    {
        public RoadworkSchemaResponseDto Schema { get; set; }

        public ImageSource ImageSource { get; set; }
    }

}
