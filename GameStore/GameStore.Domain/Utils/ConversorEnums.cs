using GameStore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Utils
{
    public static class ConversorEnums
    {
        public static CategoriaProduto ConvertToEnum(string categoria)
        {
            CategoriaProduto categoriaProduto = CategoriaProduto.Nulo;

            switch (categoria.ToLower())
            {
                case "console":
                    categoriaProduto = CategoriaProduto.Console;
                    break;

                case "game":
                    categoriaProduto = CategoriaProduto.Game;
                    break;

                case "acessorio":
                    categoriaProduto = CategoriaProduto.Acessorio;
                    break;

                case "periferico":
                    categoriaProduto = CategoriaProduto.Periferico;
                    break;
            }

            return categoriaProduto;
        }
    }
}
