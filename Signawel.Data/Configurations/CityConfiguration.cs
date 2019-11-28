using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signawel.Domain.ReportGroups;

namespace Signawel.Data.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {

        private static readonly City[] CITIES =
            {
                new City("Alken"),
                new City("As"),
                new City("Beringen"),
                new City("Bilzen"),
                new City("Bocholt"),
                new City("Borgloon"),
                new City("Bree"),
                new City("Diepenbeek"),
                new City("Dilsen-Stokkem"),
                new City("Genk"),
                new City("Gingelom"),
                new City("Halen"),
                new City("Ham"),
                new City("Hamont-Achel"),
                new City("Hasselt"),
                new City("Hechelt-Eksel"),
                new City("Heers"),
                new City("Herk-de-Stad"),
                new City("Herstappe"),
                new City("Heusden-Zolder"),
                new City("Hoeselt"),
                new City("Houthalen-Helchteren"),
                new City("Kinrooi"),
                new City("Kortessem"),
                new City("Lanaken"),
                new City("Leopoldsburg"),
                new City("Lommel"),
                new City("Lummen"),
                new City("Maaseik"),
                new City("Maasmechelen"),
                new City("Nieuwerkerken"),
                new City("Oudsbergen"),
                new City("Peer"),
                new City("Pelt"),
                new City("Riemst"),
                new City("Sint-Truiden"),
                new City("Tessenderlo"),
                new City("Tongeren"),
                new City("Voeren"),
                new City("Wellen"),
                new City("Zonhoven"),
                new City("Zutendaal")
            };

        public void Configure(EntityTypeBuilder<City> builder)
        {
            

            builder.ToTable("cities");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("id");
            builder.Property(c => c.Name)
                .IsUnicode()
                .HasColumnName("name")
                .IsRequired();

            builder.HasData(CITIES);

        }

            
    }
}
