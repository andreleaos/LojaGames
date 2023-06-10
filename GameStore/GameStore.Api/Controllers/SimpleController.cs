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

            Produto produto = new Produto(1, "Fifa 23 PS4", 79.90, CategoriaProduto.Game, imagem);

            return produto;
        }
    }
}
