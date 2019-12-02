using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Signawel.Dto.Category;
using Xamarin.Forms;

namespace Signawel.Mobile.ViewModels
{
    public class CategoriesViewModel : ViewModelBase
    {
        public ICommand CategorySelectedCommand = new Command<CategoryResponseDto>(OnCommandSelected);

        public List<CategoryResponseDto> Categories;

        public override Task InitializeAsync(object data)
        {
            Categories = data as List<CategoryResponseDto>; 
            return base.InitializeAsync(data);
        }

        private static void OnCommandSelected(CategoryResponseDto categoryResponseDto)
        {
            throw new NotImplementedException();
        }

    }
}
