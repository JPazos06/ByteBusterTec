using BBT_EstablecimientosDeSalud.Models.DB;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Xunit;
using System;
using System.Linq;

namespace Test_BBT_EStablecimientosDeSalud
{
    public class Test_Valoracion
    {
        [Fact]
        public void GuardarTest()
        {
            // Arrange
            var valoracion = new Valoracion
            {
                EstablecimientoId = 5,
                UsuarioId = 4,
                Comentario = "Este es un comentario de prueba",
                Valoracion1 = 5
            };

            // Act
            valoracion.Guardar();
            // Assert
            Assert.NotEqual(0, valoracion.Id);
        }
        [Fact]
        public void ListarIdTest()
        {
            // Arrange
            var expectedValo = new Valoracion
            {
                Id = 29,
                EstablecimientoId = 5,
                UsuarioId = 4,
                Comentario = "Este es un comentario de prueba",
                Valoracion1 = 5
            };

            var objTest = new Valoracion();

            // Act
            var resultado = objTest.ListarId(expectedValo.EstablecimientoId);

            //Assert
            Assert.True(resultado.Count > 0);
            Assert.True(resultado.Where(x => x.EstablecimientoId == expectedValo.EstablecimientoId).First().EstablecimientoId == expectedValo.EstablecimientoId);
        }
    }
}
