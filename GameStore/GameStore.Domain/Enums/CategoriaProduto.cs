using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Enums
{
    public enum CategoriaProduto
    {
        [Display(Name = "Nulo")]
        Nulo = 0,
        [Display(Name = "Console")]
        Console = 1,
        [Display(Name = "Game")]
        Game = 2,
        [Display(Name = "Acessorio")]
        Acessorio = 3,
        [Display(Name = "Periferico")]
        Periferico = 4
    }
}
