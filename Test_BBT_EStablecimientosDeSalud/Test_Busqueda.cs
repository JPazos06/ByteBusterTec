using BBT_EstablecimientosDeSalud.Models.DB;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Xunit;
using System;
using System.Linq;

namespace Test_BBT_EStablecimientosDeSalud
{
    public class Test_Busqueda
    {
        [Fact]
        public void TestRegistrar()
        {
            // Arrange
            var busqueda = new Busquedum
            {
                UsuarioId = 1,
                TerminoBusqueda = "Término de búsqueda",
                Fecha = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))
            };

            // Act
            busqueda.Registrar();
            var listarBusqueda = busqueda.ListarBusqueda().Where(x => x.Fecha == busqueda.Fecha).FirstOrDefault();

            //Assert
            Assert.Equal(busqueda.Fecha, listarBusqueda.Fecha);
        }

        [Fact]
        public void TestListar()
        {
            // Arrange
            var busqueda = new Busquedum
            {
                Id = 64,
                UsuarioId = 7,
                TerminoBusqueda = "PACIFICO odontologia",
                Fecha = DateTime.Parse("2023 - 05 - 19 02:21:59.097")
            };

            // Act
            var resultado = busqueda.ListarBusqueda();

            //Assert
            Assert.True(resultado.Count > 0);
            Assert.True(resultado.Where(x => x.Id == busqueda.Id).First().Id == busqueda.Id);
        }
    }
}
