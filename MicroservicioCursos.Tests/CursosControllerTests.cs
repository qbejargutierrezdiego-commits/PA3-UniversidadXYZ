using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using MicroservicioCursos.Controllers;
using MicroservicioCursos.Data;
using MicroservicioCursos.Models;

namespace MicroservicioCursos.Tests
{
    public class CursosControllerTests
    {
        [Fact]
        public void ObtenerTodos_DevuelveListaDeCursos()
        {
            // Arrange
            var mockRepository = new Mock<ICursoRepository>();
            var listaFalsa = new List<Curso>
            {
                new Curso { Id = 1, Codigo = "CS101", Nombre = "Programación I", Creditos = 4, Docente = "María López" }
            };
            mockRepository.Setup(repo => repo.ObtenerTodos()).Returns(listaFalsa);

            var controller = new CursosController(mockRepository.Object);

            // Act
            var resultado = controller.ObtenerTodos();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var cursos = Assert.IsAssignableFrom<List<Curso>>(okResult.Value);
            Assert.Single(cursos);
        }

        [Fact]
        public void ObtenerPorId_CuandoExiste_DevuelveCurso()
        {
            // Arrange
            var mockRepository = new Mock<ICursoRepository>();
            var cursoFalso = new Curso { Id = 1, Codigo = "CS101", Nombre = "Programación I", Creditos = 4, Docente = "María López" };
            mockRepository.Setup(repo => repo.ObtenerPorId(1)).Returns(cursoFalso);

            var controller = new CursosController(mockRepository.Object);

            // Act
            var resultado = controller.ObtenerPorId(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var curso = Assert.IsType<Curso>(okResult.Value);
            Assert.Equal("Programación I", curso.Nombre);
        }

        [Fact]
        public void ObtenerPorId_CuandoNoExiste_Devuelve404()
        {
            // Arrange
            var mockRepository = new Mock<ICursoRepository>();
            mockRepository.Setup(repo => repo.ObtenerPorId(99)).Returns((Curso?)null);

            var controller = new CursosController(mockRepository.Object);

            // Act
            var resultado = controller.ObtenerPorId(99);

            // Assert
            Assert.IsType<NotFoundObjectResult>(resultado);
        }

        [Fact]
        public void Crear_DevuelveCursoCreado()
        {
            // Arrange
            var mockRepository = new Mock<ICursoRepository>();
            mockRepository.Setup(repo => repo.Crear(It.IsAny<Curso>())).Returns(3);

            var controller = new CursosController(mockRepository.Object);
            var nuevoCurso = new Curso { Codigo = "CS102", Nombre = "Programación II", Creditos = 4, Docente = "Carlos Ruiz" };

            // Act
            var resultado = controller.Crear(nuevoCurso);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(resultado);
            var cursoCreado = Assert.IsType<Curso>(createdResult.Value);
            Assert.Equal(3, cursoCreado.Id);
        }

        [Fact]
        public void Eliminar_CuandoNoExiste_Devuelve404()
        {
            // Arrange
            var mockRepository = new Mock<ICursoRepository>();
            mockRepository.Setup(repo => repo.Eliminar(99)).Returns(false);

            var controller = new CursosController(mockRepository.Object);

            // Act
            var resultado = controller.Eliminar(99);

            // Assert
            Assert.IsType<NotFoundObjectResult>(resultado);
        }
    }
}