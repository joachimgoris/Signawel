using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Signawel.Dto;
using Signawel.Mobile.Services.Abstract;
using Signawel.Mobile.Extensions;
using Xamarin.Forms;
using System.Windows.Input;
using System.Diagnostics;
using System;

namespace Signawel.Mobile.ViewModels
{
    public class CategoryViewModel : ViewModelBase
    {
        private readonly ICategoryService _categoryService;

        private ObservableCollection<CategoryResponseDto> _categories;

        public ICommand CategorySelectedCommand => new Command<string>(OnCategorySelected);

        public ObservableCollection<CategoryResponseDto> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged("Categories");
            }
        }

        public CategoryViewModel(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        public override async Task InitializeAsync(object data)
        {
            try
            {
                Categories = (await _categoryService.GetCategoriesAsync()).ToObservableCollection();
            } catch(Exception exception)
            {
                Debug.WriteLine($"{exception.GetType().Name + " : " + exception.Message}");
            }
        }

        private void OnCategorySelected(string categoryId)
        {
            throw new NotImplementedException("This must be connected to the situations, as soon as that feature is ready.");
        }
    }
}
