using Microsoft.Data.SqlClient;
using MicroservicioCursos.Models;

namespace MicroservicioCursos.Data
{
    public class CursoRepository : ICursoRepository
    {
        private readonly string _connectionString;

        public CursoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CursosDb")!;
        }

        public List<Curso> ObtenerTodos()
        {
            var lista = new List<Curso>();

            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();

            var query = "SELECT id, codigo, nombre, creditos, docente FROM cursos";
            using var comando = new SqlCommand(query, conexion);
            using var lector = comando.ExecuteReader();

            while (lector.Read())
            {
                lista.Add(new Curso
                {
                    Id = lector.GetInt32(0),
                    Codigo = lector.GetString(1),
                    Nombre = lector.GetString(2),
                    Creditos = lector.GetInt32(3),
                    Docente = lector.GetString(4)
                });
            }

            return lista;
        }

        public Curso? ObtenerPorId(int id)
        {
            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();

            var query = "SELECT id, codigo, nombre, creditos, docente FROM cursos WHERE id = @id";
            using var comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@id", id);

            using var lector = comando.ExecuteReader();

            if (lector.Read())
            {
                return new Curso
                {
                    Id = lector.GetInt32(0),
                    Codigo = lector.GetString(1),
                    Nombre = lector.GetString(2),
                    Creditos = lector.GetInt32(3),
                    Docente = lector.GetString(4)
                };
            }

            return null;
        }

        public int Crear(Curso curso)
        {
            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();

            var query = @"INSERT INTO cursos (codigo, nombre, creditos, docente)
                          OUTPUT INSERTED.id
                          VALUES (@codigo, @nombre, @creditos, @docente)";

            using var comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@codigo", curso.Codigo);
            comando.Parameters.AddWithValue("@nombre", curso.Nombre);
            comando.Parameters.AddWithValue("@creditos", curso.Creditos);
            comando.Parameters.AddWithValue("@docente", curso.Docente);

            var nuevoId = (int)comando.ExecuteScalar();
            return nuevoId;
        }

        public bool Actualizar(int id, Curso curso)
        {
            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();

            var query = @"UPDATE cursos
                          SET codigo = @codigo, nombre = @nombre, creditos = @creditos, docente = @docente
                          WHERE id = @id";

            using var comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@codigo", curso.Codigo);
            comando.Parameters.AddWithValue("@nombre", curso.Nombre);
            comando.Parameters.AddWithValue("@creditos", curso.Creditos);
            comando.Parameters.AddWithValue("@docente", curso.Docente);
            comando.Parameters.AddWithValue("@id", id);

            var filasAfectadas = comando.ExecuteNonQuery();
            return filasAfectadas > 0;
        }

        public bool Eliminar(int id)
        {
            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();

            var query = "DELETE FROM cursos WHERE id = @id";
            using var comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@id", id);

            var filasAfectadas = comando.ExecuteNonQuery();
            return filasAfectadas > 0;
        }
    }
}