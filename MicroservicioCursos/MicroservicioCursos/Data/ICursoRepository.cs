using MicroservicioCursos.Models;

namespace MicroservicioCursos.Data
{
    public interface ICursoRepository
    {
        List<Curso> ObtenerTodos();
        Curso? ObtenerPorId(int id);
        int Crear(Curso curso);
        bool Actualizar(int id, Curso curso);
        bool Eliminar(int id);
    }
}