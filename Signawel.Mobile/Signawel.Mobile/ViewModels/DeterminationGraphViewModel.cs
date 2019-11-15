using System;
using System.Threading.Tasks;
using Signawel.Domain.Enums;
using Signawel.Dto.Determination;
using Signawel.Mobile.Bootstrap.Abstract;
using Signawel.Mobile.Services;
using Signawel.Mobile.Util;
using Xamarin.Forms;

namespace Signawel.Mobile.ViewModels
{
    public class DeterminationGraphViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public DeterminationNodeResponseDto Node { get; set; }

        public string Temp => Guid.NewGuid().ToString();

        public Command AnswerSelectedCommand => new Command<DeterminationAnswerResponseDto>(OnAnswerSelected);

        public override Task InitializeAsync(object data)
        {
            Node = data as DeterminationNodeResponseDto;

            return base.InitializeAsync(data);
        }

        public DeterminationGraphViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
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