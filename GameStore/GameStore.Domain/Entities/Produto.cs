using GameStore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public double PrecoUnitario { get; set; }
        public CategoriaProduto Categoria { get; set; }
        public ImagemProduto ImagemProduto { get; set; }
    }
}
