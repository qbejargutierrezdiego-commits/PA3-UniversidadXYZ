namespace MicroservicioCursos.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int Creditos { get; set; }
        public string Docente { get; set; } = string.Empty;
    }
}