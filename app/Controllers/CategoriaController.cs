using domain.Entities;
using domain.Enums;
using Microsoft.AspNetCore.Mvc;
using service;
using System.Threading.Tasks;

namespace app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            this.categoriaService = categoriaService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Categoria categoria)
        {
            var categoriaCreated = await categoriaService.SetNewAsync(categoria);

            if (categoriaCreated.code != Code.SUCCESS)
                return BadRequest(categoriaCreated);

            return Created("", categoriaCreated.result);
        }

        [HttpGet]
        public async Task<IActionResult> ReadAllAsync()
        {
            var categorias = await categoriaService.GetAllAsync();

            if (categorias.code == Code.UNLISTED_ITEMS)
                return BadRequest(new { categorias.code, message = "Não foi possível realizar a busca das categorias." });

            if (categorias.code == Code.ITENS_NOT_FOUND)
                return NotFound(new { categorias.code, message = "Não existem categorias cadastradas." });

            return Ok(categorias.result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ReadByIdAsync(int id)
        {
            var categoria = await categoriaService.GetByIdAsync(id);

            if (categoria.code == Code.ITEM_DOES_NOT_EXIST)
                return NotFound(new { categoria.code, message = "Nenhuma categoria encontrada." });

            return Ok(categoria.result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Categoria categoria)
        {
            categoria.Id = id;
            var categoriaUpdated = await categoriaService.AlterAsync(categoria);

            if (categoriaUpdated.code == Code.ITEM_DOES_NOT_EXIST)
                return BadRequest(new { categoriaUpdated.code, message = "Não foi possível realizar a atualização da categoria, pois ela não existe." });

            return Ok(categoriaUpdated.result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await categoriaService.DeleteAsync(id);

            if (result.code == Code.ITEM_NOT_DELETED)
                return BadRequest(new { result.code, message = "Não foi possível deletar a categoria." });

            return Ok("Categoria deletada com sucesso.");
        }
    }
}
