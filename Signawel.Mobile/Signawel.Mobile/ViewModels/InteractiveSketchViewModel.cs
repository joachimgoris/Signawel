using System;
using System.Threading.Tasks;
using Signawel.Dto.RoadworkSchema;
using Signawel.Mobile.Bootstrap.Abstract;
using Signawel.Mobile.Constants;
using Signawel.Mobile.Services.Abstract;

namespace Signawel.Mobile.ViewModels
{
    public class InteractiveSketchViewModel : ViewModelBase
    {
        private readonly IDeterminationSchemaService _determinationSchemaService;
        private readonly IHttpService _httpService;

        public event EventHandler OnLoaded;

        //public IList<List<Point>> Points { get; set; }
        public RoadworkSchemaResponseDto Schema { get; set; }
        public byte[] ImageUrlBytes { get; set; }

        public InteractiveSketchViewModel(IDeterminationSchemaService determinationSchemaService, IHttpService httpService)
        {
            this._determinationSchemaService = determinationSchemaService;
            this._httpService = httpService;
        }

        private async Task<byte[]> RetrieveBitmap(string id)
        {
            try
            {
                return await _httpService.GetByteArrayAsync(ApiConstants.GetImage(id));
            } catch(Exception)
            {
                return null;
            }
        }

        public override async Task InitializeAsync(object data)
        {
            var id = data as string;
            
            Schema = await _determinationSchemaService.GetRoadworkSchema(id);
            ImageUrlBytes = await RetrieveBitmap(Schema.ImageId);

            OnLoaded?.Invoke(this, new EventArgs());
        }
    }
}
