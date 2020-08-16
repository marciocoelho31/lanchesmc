using LanchesMC.Models;
using System.Collections.Generic;

namespace LanchesMC.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Lanche> LanchesPreferidos { get; set; }
    }
}
