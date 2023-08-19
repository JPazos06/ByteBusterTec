using BBT_EstablecimientosDeSalud.Models.DB;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Xunit;
using System;
using System.Linq;

namespace Test_BBT_EStablecimientosDeSalud
{
    public class Test_EstablecimientoDeSalud
    {
        [Fact]
        public void TestBuscar()
        {
            // Arrange
            var criterio = "AMBULATORIA";
            var epsid = 1;

            var establecimientoDeSalud = new EstablecimientoDeSalud();

            // Act
            var result = establecimientoDeSalud.Buscar(criterio, epsid);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Contains(result, e => e.Id == 1);
            Assert.Contains(result, e => e.Id == 2);
        }

        [Fact]
        public void TestBuscarId()
        {
            // Arrange
            var estId = 7;

            var expectedEstablecimiento = new EstablecimientoDeSalud
            {
                Ciudad = "TACNA",
                Descripcion = "ODONTOLOGIA",
                Direccion = "CL. MILLER 50-2DO PISO",
                Id = 7,
                Imagen = "https://imgs.deperu.com/lugares/medico_consultorio.jpg",
                Latitud = Convert.ToDecimal(-18.01353332),
                Longitud = Convert.ToDecimal(-70.24763036),
                Nombre = "CONSORCIO DENTAL SUIZA"
            };
            var establecimientoDeSalud = new EstablecimientoDeSalud();

            // Act
            var result = establecimientoDeSalud.BuscarId(estId);

            // Assert
            Assert.Equal(expectedEstablecimiento.Imagen, result.Imagen);
            Assert.Equal(expectedEstablecimiento.Nombre, result.Nombre);
            Assert.Equal(expectedEstablecimiento.Descripcion, result.Descripcion);
            Assert.Equal(expectedEstablecimiento.Direccion, result.Direccion);
            Assert.Equal(expectedEstablecimiento.Latitud, result.Latitud);
            Assert.Equal(expectedEstablecimiento.Id, result.Id);
            Assert.Equal(expectedEstablecimiento.Ciudad, result.Ciudad);
            Assert.Equal(expectedEstablecimiento.Longitud, result.Longitud);
        }

        [Fact]
        public void TestListar()
        {
            EstablecimientoDeSalud objEst = new EstablecimientoDeSalud();
            var resultado = objEst.Listar(epsid: 1);
            Assert.True(resultado.Count > 0);
        }

        [Fact]
        public void TestListarMap()
        {
            EstablecimientoDeSalud objEst = new EstablecimientoDeSalud();
            var resultado = objEst.ListarMap();
            Assert.True(resultado.Count > 0);
        }
    }
}
