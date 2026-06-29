using MicroservicioEstudiantes.Models;

namespace MicroservicioEstudiantes.Data
{
    public interface IEstudianteRepository
    {
        List<Estudiante> ObtenerTodos();
        Estudiante? ObtenerPorId(int id);
        int Crear(Estudiante estudiante);
        bool Actualizar(int id, Estudiante estudiante);
        bool Eliminar(int id);
    }
}