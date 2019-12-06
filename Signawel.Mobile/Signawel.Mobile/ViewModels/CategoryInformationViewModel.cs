using Signawel.Domain.Enums;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Signawel.Mobile.ViewModels
{
    public class CategoryInformationViewModel : ViewModelBase
    {

        public IList<CategoryInformation> Categories { get; set; }

        public double ImageWidth => Application.Current.MainPage.Width;

        public CategoryInformationViewModel()
        {
            Categories = new List<CategoryInformation>
            {
                new CategoryInformation(RoadworkCategory.Category1, "De werken ingeplant op autosnelwegen en op openbare wegen waar de maximum toegelaten snelheid hoger is dan 90 km/h."),
                new CategoryInformation(RoadworkCategory.Category2, "De werken ingeplant op openbare wegen waar de maximum toegelaten snelheid hoger is dan 50 km/h en lager dan of gelijk aan 90 km/h;"),
                new CategoryInformation(RoadworkCategory.Category3, "De werken ingeplant op openbare wegen waar de maximum toegelaten snelheid lager is dan of gelijk aan 50 km/h."),
                new CategoryInformation(RoadworkCategory.Category4, "De werken die ingeplant zijn buiten de rijbaan maar die een gevaar betekenen voor de voetgangers, de fietsers en de bestuurders van tweewielige bromfietsen."),
                new CategoryInformation(RoadworkCategory.Category5, "De werken die uitgevoerd worden tussen het aanbreken van de dag en het vallen van de avond en wanneer het mogelijk is duidelijk te zien tot op een afstand van ongeveer 200 m."),
                new CategoryInformation(RoadworkCategory.Category6, "De mobiele werken die vanwege hun relatief lage verplaatsingssnelheid of vanwege hun veelvuldig stilstaan voor het uitvoeren van werken slechts kortstondig het verkeer hinderen.")
            };
        }
    }

    public class CategoryInformation
    {

        private RoadworkCategory Category { get; set; }

        public ImageSource ImageSource { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public CategoryInformation(RoadworkCategory category, string description)
        {
            Category = category;
            Name = $"Categorie { (byte)Category }";
            ImageSource = ImageSource.FromResource($"Signawel.Mobile.Resources.Images.cat{ (byte)Category }.png", typeof(CategoryBiningObject).Assembly);
            Description = description;
        }

    }
}
