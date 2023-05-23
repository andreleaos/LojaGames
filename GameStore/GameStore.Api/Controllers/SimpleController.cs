using GameStore.Domain.Entities;
using GameStore.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimpleController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                Produto produto = GetProductExample();
                return Ok(produto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Produto GetProductExample()
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

            return produto;
        }
    }
}
