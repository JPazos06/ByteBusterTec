using BBT_EstablecimientosDeSalud.Models.DB;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Xunit;
using System;
using System.Linq;

namespace Test_BBT_EStablecimientosDeSalud
{
    public class Test_EpsEstablecimientoDeSalud
    {
        [Fact]
        public void TestBuscarId()
        {
            var EstId = 1;
            // Arrange
            var expectedEst = new EpsEstablecimientoDeSalud
            {
                Id = 1,
                EpsId = 1,
                EstablecimientoId = 1
            };

            // Act
            var resultado = expectedEst.BuscarId(EstId);

            //Assert
            Assert.Equal(expectedEst.Id, resultado.Id);
        }

        [Fact]
        public void TestBuscarIdEps()
        {
            var EstId = 1;
            // Arrange
            var expectedEp = new Ep
            {
                Id = 1,
                Nombre = "LA POSITIVA"
            };
            var objEpsEst = new EpsEstablecimientoDeSalud();
            // Act
            var resultado = objEpsEst.BuscarIdEps(EstId);

            //Assert
            Assert.Equal(expectedEp.Id, resultado.Id);
        }
    }
}
