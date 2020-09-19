using domain.Entities;
using domain.Enums;
using Microsoft.AspNetCore.Mvc;
using service;
using System.Threading.Tasks;

namespace app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CursoController : ControllerBase
    {
        private readonly ICursoService cursoService;

        public CursoController(ICursoService cursoService)
        {
            this.cursoService = cursoService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Curso curso)
        {
            var cursoCreated = await cursoService.SetNewAsync(curso);

            if (cursoCreated.code == Code.BUSY_PERIOD)
                return BadRequest(new { cursoCreated.code, message = "Existe(m) curso(s) planejados(s) dentro do período informado." });

            if (cursoCreated.code == Code.INVALID_DATE_RANGE)
                return BadRequest(new { cursoCreated.code, message = "Data de início do curso não pode ser superior à data final." });

            return Created("", cursoCreated.result);
        }

        [HttpGet]
        public async Task<IActionResult> ReadAllAsync()
        {
            var cursos = await cursoService.GetAllAsync();

            if (cursos.code == Code.UNLISTED_ITEMS)
                return BadRequest(new { cursos.code, message = "Não foi possível realizar a busca dos cursos." });

            if (cursos.code == Code.ITENS_NOT_FOUND)
                return NotFound(new { cursos.code, message = "Não existem cursos cadastrados." });

            return Ok(cursos.result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ReadByIdAsync(int id)
        {
            var curso = await cursoService.GetByIdAsync(id);

            if (curso.code == Code.ITEM_DOES_NOT_EXIST)
                return NotFound(new { curso.code, message = "Nenhum curso encontrado." });

            return Ok(curso.result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Curso curso)
        {
            curso.Id = id;
            var cursoUpdated = await cursoService.AlterAsync(curso);

            if (cursoUpdated.code == Code.BUSY_PERIOD)
                return BadRequest(new { cursoUpdated.code, message = "Existe(m) curso(s) planejados(s) dentro do período informado." });

            if (cursoUpdated.code == Code.ITEM_DOES_NOT_EXIST)
                return BadRequest(new { cursoUpdated.code, message = "Não foi possível realizar a atualização do curso, pois ele não existe." });

            if (cursoUpdated.code == Code.INVALID_DATE_RANGE)
                return BadRequest(new { cursoUpdated.code, message = "Data de início do curso não pode ser superior à data final." });

            return Ok(cursoUpdated.result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await cursoService.DeleteAsync(id);

            if (result.code == Code.ITEM_NOT_DELETED)
                return BadRequest(new { result.code, message = "Não foi possível deletar o curso." });

            return Ok("Curso deletado com sucesso.");
        }
    }
}
