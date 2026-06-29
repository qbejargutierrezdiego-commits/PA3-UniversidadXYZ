using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using MicroservicioEstudiantes.Controllers;
using MicroservicioEstudiantes.Data;
using MicroservicioEstudiantes.Models;

namespace MicroservicioEstudiantes.Tests
{
    public class EstudiantesControllerTests
    {
        [Fact]
        public void ObtenerTodos_DevuelveListaDeEstudiantes()
        {
            // Arrange
            var mockRepository = new Mock<IEstudianteRepository>();
            var listaFalsa = new List<Estudiante>
            {
                new Estudiante { Id = 1, Codigo = "EST-0001", Nombres = "Ana", Apellidos = "Torres", Correo = "ana@test.com", Carrera = "Sistemas" }
            };
            mockRepository.Setup(repo => repo.ObtenerTodos()).Returns(listaFalsa);

            var controller = new EstudiantesController(mockRepository.Object);

            // Act
            var resultado = controller.ObtenerTodos();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var estudiantes = Assert.IsAssignableFrom<List<Estudiante>>(okResult.Value);
            Assert.Single(estudiantes);
        }

        [Fact]
        public void ObtenerPorId_CuandoExiste_DevuelveEstudiante()
        {
            // Arrange
            var mockRepository = new Mock<IEstudianteRepository>();
            var estudianteFalso = new Estudiante { Id = 1, Codigo = "EST-0001", Nombres = "Ana", Apellidos = "Torres", Correo = "ana@test.com", Carrera = "Sistemas" };
            mockRepository.Setup(repo => repo.ObtenerPorId(1)).Returns(estudianteFalso);

            var controller = new EstudiantesController(mockRepository.Object);

            // Act
            var resultado = controller.ObtenerPorId(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var estudiante = Assert.IsType<Estudiante>(okResult.Value);
            Assert.Equal("Ana", estudiante.Nombres);
        }

        [Fact]
        public void ObtenerPorId_CuandoNoExiste_Devuelve404()
        {
            // Arrange
            var mockRepository = new Mock<IEstudianteRepository>();
            mockRepository.Setup(repo => repo.ObtenerPorId(99)).Returns((Estudiante?)null);

            var controller = new EstudiantesController(mockRepository.Object);

            // Act
            var resultado = controller.ObtenerPorId(99);

            // Assert
            Assert.IsType<NotFoundObjectResult>(resultado);
        }

        [Fact]
        public void Crear_DevuelveEstudianteCreado()
        {
            // Arrange
            var mockRepository = new Mock<IEstudianteRepository>();
            mockRepository.Setup(repo => repo.Crear(It.IsAny<Estudiante>())).Returns(5);

            var controller = new EstudiantesController(mockRepository.Object);
            var nuevoEstudiante = new Estudiante { Codigo = "EST-0005", Nombres = "Luis", Apellidos = "Ramos", Correo = "luis@test.com", Carrera = "Software" };

            // Act
            var resultado = controller.Crear(nuevoEstudiante);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(resultado);
            var estudianteCreado = Assert.IsType<Estudiante>(createdResult.Value);
            Assert.Equal(5, estudianteCreado.Id);
        }

        [Fact]
        public void Eliminar_CuandoNoExiste_Devuelve404()
        {
            // Arrange
            var mockRepository = new Mock<IEstudianteRepository>();
            mockRepository.Setup(repo => repo.Eliminar(99)).Returns(false);

            var controller = new EstudiantesController(mockRepository.Object);

            // Act
            var resultado = controller.Eliminar(99);

            // Assert
            Assert.IsType<NotFoundObjectResult>(resultado);
        }
    }
}