using System;
using System.Collections.Generic;
using System.Text;

namespace Signawel.Mobile.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {

        public string AboutInfo => "SIGNAwell App is een applicatie voor het melden van signalisatie problemen bij wegenwerken. Zo geraken deze meldingen tot bij de juiste instanties en kunnen deze zo snel mogelijk opgelost worden.";

        public string Disclaimer => "Deze applicatie is enkel bruikbaar voor werken die ingegeven zijn in GiPOD.";

    }
}
