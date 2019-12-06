using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Signawel.Domain.Enums;
using Signawel.Dto.Determination;
using Signawel.Mobile.Bootstrap.Abstract;
using Signawel.Mobile.Services.Abstract;
using Xamarin.Forms;

namespace Signawel.Mobile.ViewModels
{
    public class DeterminationGraphViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IDeterminationGraphService _determinationGraphService;

        public DeterminationNodeResponseDto Node { get; set; }

        public string Temp => Guid.NewGuid().ToString();

        public ICommand AnswerSelectedCommand => new Command<DeterminationAnswerResponseDto>(OnAnswerSelected);
        
        public DeterminationGraphViewModel(INavigationService navigationService, IDeterminationGraphService determinationGraphService)
        {
            _navigationService = navigationService;
            _determinationGraphService = determinationGraphService;
        }

        public override async Task InitializeAsync(object data)
        {
            if(data == null)
            {
                var graph = await _determinationGraphService.GetDeterminationGraph();
                Node = graph.Start;
            } else
            {
                Node = data as DeterminationNodeResponseDto;
            }
        }

        private void OnAnswerSelected(DeterminationAnswerResponseDto answer)
        {

            if (answer.Node.Type == DeterminationNodeType.Question)
            {
                _navigationService.NavigateToAsync<DeterminationGraphViewModel>(answer.Node);
            }
            else
            {
                _navigationService.NavigateToAsync<InteractiveSketchViewModel>(answer.Node.SchemaId);
            }
        }
    }
}