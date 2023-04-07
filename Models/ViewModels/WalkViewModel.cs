using System.Collections.Generic;

namespace DogGo.Models.ViewModels
{
    public class WalkViewModel
    {
        public Walk walk { get; set; }
        public List<Walker> walkers { get; set; }
        public List<Dog> dogs { get; set; }
    }
}
