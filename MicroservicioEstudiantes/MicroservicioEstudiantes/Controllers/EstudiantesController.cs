using Microsoft.AspNetCore.Mvc;
using MicroservicioEstudiantes.Data;
using MicroservicioEstudiantes.Models;

namespace MicroservicioEstudiantes.Controllers
{
    [ApiController]
    [Route("api/estudiantes")]
    public class EstudiantesController : ControllerBase
    {
        private readonly IEstudianteRepository _repository;

        public EstudiantesController(IEstudianteRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var estudiantes = _repository.ObtenerTodos();
            return Ok(estudiantes);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            var estudiante = _repository.ObtenerPorId(id);

            if (estudiante == null)
            {
                return NotFound(new { mensaje = "Estudiante no encontrado" });
            }

            return Ok(estudiante);
        }

        [HttpPost]
        public IActionResult Crear([FromBody] Estudiante estudiante)
        {
            var nuevoId = _repository.Crear(estudiante);
            estudiante.Id = nuevoId;
            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevoId }, estudiante);
        }

        [HttpPut("{id}")]
        public IActionResult Actualizar(int id, [FromBody] Estudiante estudiante)
        {
            var actualizado = _repository.Actualizar(id, estudiante);

            if (!actualizado)
            {
                return NotFound(new { mensaje = "Estudiante no encontrado" });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            var eliminado = _repository.Eliminar(id);

            if (!eliminado)
            {
                return NotFound(new { mensaje = "Estudiante no encontrado" });
            }

            return NoContent();
        }
    }
}