namespace MicroservicioEstudiantes.Models
{
    public class Estudiante
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Carrera { get; set; } = string.Empty;
    }
}