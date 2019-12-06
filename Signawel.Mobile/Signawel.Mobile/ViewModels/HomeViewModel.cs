using Signawel.Domain.Enums;
using Signawel.Mobile.Bootstrap;
using Signawel.Mobile.Bootstrap.Abstract;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Signawel.Mobile.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public ObservableCollection<CategoryBiningObject> Categories { get; set; }

        #region Commands

        public ICommand SelectCategoryCommand => new AsyncCommand<RoadworkCategory>(OnSelectCategoryCommand);

        public ICommand SelectDeterminationCommand => new AsyncCommand(OnSelectDeterminationCommand);

        public ICommand SelectCategoryInformationCommand => new AsyncCommand(OnSelectCategoryInformationCommand);

        #endregion

        public HomeViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            Categories = new ObservableCollection<CategoryBiningObject>
            {
                new CategoryBiningObject(RoadworkCategory.Category1),
                new CategoryBiningObject(RoadworkCategory.Category2),
                new CategoryBiningObject(RoadworkCategory.Category3),
                new CategoryBiningObject(RoadworkCategory.Category4),
                new CategoryBiningObject(RoadworkCategory.Category5),
                new CategoryBiningObject(RoadworkCategory.Category6)
            };
        }

        #region Command Execution

        private async Task OnSelectCategoryCommand(RoadworkCategory category)
        {
            await _navigationService.NavigateToAsync<RoadworkSchemaListViewModel>(category);
        }

        private async Task OnSelectDeterminationCommand()
        {
            await _navigationService.NavigateToAsync<DeterminationGraphViewModel>();
        }

        public async Task OnSelectCategoryInformationCommand()
        {
            await _navigationService.NavigateToAsync<CategoryInformationViewModel>();
        }

        #endregion

    }

    public class CategoryBiningObject
    {
        public RoadworkCategory Category { get; set; }

        public string Name { get; set; }

        public ImageSource ImageSource { get; set; }

        public CategoryBiningObject(RoadworkCategory category)
        {
            Category = category;
            Name = $"Categorie { (byte)Category }";
            ImageSource = ImageSource.FromResource($"Signawel.Mobile.Resources.Images.cat{ (byte)Category }.png", typeof(CategoryBiningObject).Assembly);
        }
    }
}
