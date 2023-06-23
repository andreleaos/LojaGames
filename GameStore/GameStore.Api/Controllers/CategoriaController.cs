using GameStore.Domain.Contracts.Base;
using GameStore.Domain.Dtos;
using GameStore.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public List<CategoriaDto> GetAll()
        {
            return _categoriaService.GetAll();
        }

    }
}
