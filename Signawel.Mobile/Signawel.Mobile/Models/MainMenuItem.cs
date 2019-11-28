using System;

namespace Signawel.Mobile.Models
{
    public class MainMenuItem
    {

        public string MenuText { get; set; }

        public Type ViewModelType { get; set; }
        
        public object ViewModelParameters { get; set; }

    }
}
