using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers.Base
{
    public interface IGenericControllerActions<T, Y>
    {
        [HttpGet]
        public Task<ActionResult<List<T>>> GetAll();
        [HttpGet]
        public Task<ActionResult<T>> Get(Y id);
        [HttpPost]
        public Task<ActionResult> Post(T entity);
        [HttpPut]
        public Task<ActionResult> Put(T entity);
        [HttpDelete]
        Task<ActionResult> DeletePut(Y id);
    }
}
