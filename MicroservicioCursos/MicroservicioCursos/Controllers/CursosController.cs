using Microsoft.AspNetCore.Mvc;
using MicroservicioCursos.Data;
using MicroservicioCursos.Models;

namespace MicroservicioCursos.Controllers
{
    [ApiController]
    [Route("api/cursos")]
    public class CursosController : ControllerBase
    {
        private readonly ICursoRepository _repository;

        public CursosController(ICursoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var cursos = _repository.ObtenerTodos();
            return Ok(cursos);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            var curso = _repository.ObtenerPorId(id);

            if (curso == null)
            {
                return NotFound(new { mensaje = "Curso no encontrado" });
            }

            return Ok(curso);
        }

        [HttpPost]
        public IActionResult Crear([FromBody] Curso curso)
        {
            var nuevoId = _repository.Crear(curso);
            curso.Id = nuevoId;
            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevoId }, curso);
        }

        [HttpPut("{id}")]
        public IActionResult Actualizar(int id, [FromBody] Curso curso)
        {
            var actualizado = _repository.Actualizar(id, curso);

            if (!actualizado)
            {
                return NotFound(new { mensaje = "Curso no encontrado" });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            var eliminado = _repository.Eliminar(id);

            if (!eliminado)
            {
                return NotFound(new { mensaje = "Curso no encontrado" });
            }

            return NoContent();
        }
    }
}