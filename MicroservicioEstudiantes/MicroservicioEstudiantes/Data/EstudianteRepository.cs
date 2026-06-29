using Microsoft.Data.SqlClient;
using MicroservicioEstudiantes.Models;

namespace MicroservicioEstudiantes.Data
{
    public class EstudianteRepository : IEstudianteRepository
    {
        private readonly string _connectionString;

        public EstudianteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("EstudiantesDb")!;
        }

        public List<Estudiante> ObtenerTodos()
        {
            var lista = new List<Estudiante>();

            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();

            var query = "SELECT id, codigo, nombres, apellidos, correo, carrera FROM estudiantes";
            using var comando = new SqlCommand(query, conexion);
            using var lector = comando.ExecuteReader();

            while (lector.Read())
            {
                lista.Add(new Estudiante
                {
                    Id = lector.GetInt32(0),
                    Codigo = lector.GetString(1),
                    Nombres = lector.GetString(2),
                    Apellidos = lector.GetString(3),
                    Correo = lector.GetString(4),
                    Carrera = lector.GetString(5)
                });
            }

            return lista;
        }

        public Estudiante? ObtenerPorId(int id)
        {
            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();

            var query = "SELECT id, codigo, nombres, apellidos, correo, carrera FROM estudiantes WHERE id = @id";
            using var comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@id", id);

            using var lector = comando.ExecuteReader();

            if (lector.Read())
            {
                return new Estudiante
                {
                    Id = lector.GetInt32(0),
                    Codigo = lector.GetString(1),
                    Nombres = lector.GetString(2),
                    Apellidos = lector.GetString(3),
                    Correo = lector.GetString(4),
                    Carrera = lector.GetString(5)
                };
            }

            return null;
        }

        public int Crear(Estudiante estudiante)
        {
            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();

            var query = @"INSERT INTO estudiantes (codigo, nombres, apellidos, correo, carrera)
                          OUTPUT INSERTED.id
                          VALUES (@codigo, @nombres, @apellidos, @correo, @carrera)";

            using var comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@codigo", estudiante.Codigo);
            comando.Parameters.AddWithValue("@nombres", estudiante.Nombres);
            comando.Parameters.AddWithValue("@apellidos", estudiante.Apellidos);
            comando.Parameters.AddWithValue("@correo", estudiante.Correo);
            comando.Parameters.AddWithValue("@carrera", estudiante.Carrera);

            var nuevoId = (int)comando.ExecuteScalar();
            return nuevoId;
        }

        public bool Actualizar(int id, Estudiante estudiante)
        {
            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();

            var query = @"UPDATE estudiantes
                          SET codigo = @codigo, nombres = @nombres, apellidos = @apellidos,
                              correo = @correo, carrera = @carrera
                          WHERE id = @id";

            using var comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@codigo", estudiante.Codigo);
            comando.Parameters.AddWithValue("@nombres", estudiante.Nombres);
            comando.Parameters.AddWithValue("@apellidos", estudiante.Apellidos);
            comando.Parameters.AddWithValue("@correo", estudiante.Correo);
            comando.Parameters.AddWithValue("@carrera", estudiante.Carrera);
            comando.Parameters.AddWithValue("@id", id);

            var filasAfectadas = comando.ExecuteNonQuery();
            return filasAfectadas > 0;
        }

        public bool Eliminar(int id)
        {
            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();

            var query = "DELETE FROM estudiantes WHERE id = @id";
            using var comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@id", id);

            var filasAfectadas = comando.ExecuteNonQuery();
            return filasAfectadas > 0;
        }
    }
}