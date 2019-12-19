using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Signawel.Dto.RoadworkSchema;
using Signawel.Mobile.Bootstrap;
using Signawel.Mobile.Bootstrap.Abstract;
using Signawel.Mobile.Constants;
using Signawel.Mobile.Services.Abstract;

namespace Signawel.Mobile.ViewModels
{
    public class InteractiveSketchViewModel : ViewModelBase
    {
        private readonly IRoadworkSchemaService _determinationSchemaService;
        private readonly IHttpService _httpService;
        private readonly INavigationService _navigationService;

        public event EventHandler OnLoaded;

        public RoadworkSchemaResponseDto Schema { get; set; }
        public bool Loading { get; set; }
        public bool ScetchVisibility { get; set; }

        public byte[] ImageUrlBytes { get; set; }

        public ICommand SelectedBoudingBoxCommand => new AsyncCommand<string>(OnSelectedBoudingBoxCommand);

        public InteractiveSketchViewModel(IRoadworkSchemaService determinationSchemaService, IHttpService httpService, INavigationService navigationService)
        {
            _determinationSchemaService = determinationSchemaService;
            _httpService = httpService;
            _navigationService = navigationService;
        }

        public override async Task InitializeAsync(object data)
        {
            if(data == null)
            {
                throw new ArgumentNullException();
            }

            if(data is string id)
            {
                Loading = true;
                Schema = await _determinationSchemaService.GetRoadworkSchema(id);
               

            } else if(data is RoadworkSchemaResponseDto dto)
            {
                Schema = dto;
            } else
            {
                throw new ArgumentException("argument data should be instance of 'string' (id of roadworkschema) or 'RoadworkSchemaResponseDto'.");
            }
            Loading = true;
            ImageUrlBytes = await RetrieveBitmap(Schema.ImageId);
            Loading = false;
            ScetchVisibility = true;
            OnLoaded?.Invoke(this, new EventArgs());
        }

        private async Task<byte[]> RetrieveBitmap(string id)
        {
            try
            {
                return await _httpService.GetByteArrayAsync(ApiConstants.GetImage(id));
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task OnSelectedBoudingBoxCommand(string bboxId)
        {
            await _navigationService.NavigateToAsync<ReportViewModel>();
        }
    }
}
