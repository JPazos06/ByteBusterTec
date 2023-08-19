using BBT_EstablecimientosDeSalud.Models.DB;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Xunit;
using System;
using System.Linq;

namespace Test_BBT_EStablecimientosDeSalud
{
    public class Test_Usuario
    {
        [Fact]
        public void TestRegistrar()
        {
            // Arrange
            var usuario = new Usuario
            {
                Nombre = "Test",
                Apellido = "Test",
                Email = "Test@test.pe",
                Contrasena = "Test",
            };

            // Act
            usuario.Registrar();

            // Assert
            Assert.NotEqual(0, usuario.Id);
        }
        [Fact]
        public void TestLogin()
        {
            // Arrange
            var expectedusuario = new Usuario
            {
                Email = "Test@test.pe",
                Contrasena = "Test"
            };

            var objTest = new Usuario();
            // Act
            var resultado = objTest.Login(expectedusuario.Email, expectedusuario.Contrasena);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(expectedusuario.Email, resultado.Email);
            Assert.Equal(expectedusuario.Contrasena, resultado.Contrasena);
        }
        [Fact]
        public void TestBuscarId()
        {
            var usId = 2;
            // Arrange
            var expectedusuario = new Usuario
            {
                Nombre = "ALEJANDRA MARIA",
                Apellido = "VALDIVIA GUZMAN",
                Email = "alevaldiviag@upt.pe"
            };

            var objTest = new Usuario();
            // Act
            var resultado = objTest.BuscarId(usId);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(expectedusuario.Nombre, resultado.Nombre);
            Assert.Equal(expectedusuario.Apellido, resultado.Apellido);
            Assert.Equal(expectedusuario.Email, resultado.Email);
        }
    }
}
