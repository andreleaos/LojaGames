using GameStore.Domain.Entities;
using GameStore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.Data.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private static List<Produto> _produtos;

        public ProdutoRepository()
        {
            _produtos = new List<Produto>();
            CarregarDados();
        }

        private void CarregarDados()
        {
            string url = @"C:\Repo\LojaGames\GameStore\GameStore.Infrastructure\Images\Fifa23.png";

            ImagemProduto imagem = new ImagemProduto(1, url);

            Produto produto = new Produto()
            {
                Id = 1,
                Descricao = "Fifa 23 PS4",
                PrecoUnitario = 79.90,
                Categoria = CategoriaProduto.Game,
                ImagemProduto = imagem
            };
            _produtos.Add(produto);

            url = @"C:\Repo\LojaGames\GameStore\GameStore.Infrastructure\Images\f1_22_ps4.jpg";

            imagem = new ImagemProduto(2, url);

            produto = new Produto()
            {
                Id = 2,
                Descricao = "F1 22 PS4",
                PrecoUnitario = 129.90,
                Categoria = CategoriaProduto.Game,
                ImagemProduto = imagem
            };
            _produtos.Add(produto);

            url = @"C:\Repo\LojaGames\GameStore\GameStore.Infrastructure\Images\mk_11_ps4.jpg";

            imagem = new ImagemProduto(3, url);

            produto = new Produto()
            {
                Id = 3,
                Descricao = "Mortal Kombat PS4",
                PrecoUnitario = 109.90,
                Categoria = CategoriaProduto.Game,
                ImagemProduto = imagem
            };
            _produtos.Add(produto);

            url = @"C:\Repo\LojaGames\GameStore\GameStore.Infrastructure\Images\sfv_ps4.jpg";

            imagem = new ImagemProduto(4, url);

            produto = new Produto()
            {
                Id = 4,
                Descricao = "Street Fighter V PS4",
                PrecoUnitario = 89.90,
                Categoria = CategoriaProduto.Game,
                ImagemProduto = imagem
            };
            _produtos.Add(produto);
        }

        public void Create(Produto entity)
        {
            _produtos.Add(entity);
        }

        public bool Delete(int id)
        {
            Produto? produto = GetById(id);
            if (produto != null)
            {
                _produtos.Remove(produto);
                return true;
            }

            return false;
        }

        public List<Produto> GetAll()
        {
            return _produtos.OrderBy(p => p.Descricao).ToList();
        }

        public Produto? GetById(int id)
        {
            return _produtos.FirstOrDefault(p => p.Id == id);
        }

        public void Update(Produto entity)
        {
            Produto? produto = GetById(entity.Id);
            if (produto != null)
            {
                _produtos.Remove(produto);
                _produtos.Add(entity);
            }
        }
    }
}
