using BBT_EstablecimientosDeSalud.Models.DB;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Xunit;
using System;
using System.Linq;

namespace Test_BBT_EStablecimientosDeSalud
{
    public class Test_EPS
    {
        [Fact]
        public void TestBuscarId()
        {
            // Arrange
            var eps = new Ep
            {
                Id = 3,
                Nombre = "LA POSITIVA",
            };

            // Act
            var resultado = eps.Listar();

            //Assert
            Assert.True(resultado.Count > 0);
            Assert.True(resultado.Where(x => x.Id == eps.Id).First().Id == eps.Id);
        }

        [Fact]
        public void TestListar()
        {
            Ep objEp = new Ep();
            var resultado = objEp.Listar();
            Assert.True(resultado.Count > 0);
        }
    }
}
