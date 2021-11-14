﻿using Microsoft.EntityFrameworkCore;
using Moq;
using Reservas.Data;
using Reservas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PruebasUnitarias
{
    class ApplicationMockContext
    {
        public static Mock<ReservasDbContext> getListaContextMock()
        {
            IQueryable<Mesa> mesa = getMesa();

            var mockDbSetMesa = new Mock<DbSet<Mesa>>();
            mockDbSetMesa.As<IQueryable<Mesa>>().Setup(m => m.Provider).Returns(mesa.Provider);
            mockDbSetMesa.As<IQueryable<Mesa>>().Setup(m => m.Expression).Returns(mesa.Expression);
            mockDbSetMesa.As<IQueryable<Mesa>>().Setup(m => m.ElementType).Returns(mesa.ElementType);
            mockDbSetMesa.As<IQueryable<Mesa>>().Setup(m => m.GetEnumerator()).Returns(mesa.GetEnumerator());
            mockDbSetMesa.Setup(m => m.AsQueryable()).Returns(mesa);


            var mockContext = new Mock<ReservasDbContext>(new DbContextOptions<ReservasDbContext>());
            mockContext.Setup(c => c.Mesa).Returns(mockDbSetMesa.Object);

            return mockContext;
        }

        private static IQueryable<Mesa> getMesa()
        {
            return new List<Mesa>
            {
               new Mesa { Id = 1, Descripcion = "Prueba 1", Estado = 2 ,NumeroMesa = "2",NumeroPersonas = 4},
               new Mesa { Id = 2, Descripcion = "Prueba 2", Estado = 3 ,NumeroMesa = "4",NumeroPersonas = 5},
               new Mesa { Id = 3, Descripcion = "Prueba 3", Estado = 1 ,NumeroMesa = "5",NumeroPersonas = 6},
               new Mesa { Id = 4, Descripcion = "Prueba 4", Estado = 1 ,NumeroMesa = "6",NumeroPersonas = 7}

            }.AsQueryable();
        }

        public static Mock<ReservasDbContext> getListaReservaContextMock()
        {
            IQueryable<Reserva> reserva = getReserva();

            var mockDbSetMesa = new Mock<DbSet<Reserva>>();
            mockDbSetMesa.As<IQueryable<Reserva>>().Setup(m => m.Provider).Returns(reserva.Provider);
            mockDbSetMesa.As<IQueryable<Reserva>>().Setup(m => m.Expression).Returns(reserva.Expression);
            mockDbSetMesa.As<IQueryable<Reserva>>().Setup(m => m.ElementType).Returns(reserva.ElementType);
            mockDbSetMesa.As<IQueryable<Reserva>>().Setup(m => m.GetEnumerator()).Returns(reserva.GetEnumerator());
            mockDbSetMesa.Setup(m => m.AsQueryable()).Returns(reserva);


            var mockContext = new Mock<ReservasDbContext>(new DbContextOptions<ReservasDbContext>());
            mockContext.Setup(c => c.Reserva).Returns(mockDbSetMesa.Object);

            return mockContext;
        }

        private static IQueryable<Reserva> getReserva()
        {
            return new List<Reserva>
            {
               new Reserva { Id = 1, NombreCliente="Juan Perez Soza", Celular = "963258741", FechaReserva = DateTime.Now, MesaId = 1, Mesa = new Mesa(){ } },
               new Reserva { Id = 2, NombreCliente="Jose Perez Soza", Celular = "456987412", FechaReserva = DateTime.Now, MesaId = 2, Mesa = new Mesa(){ } },
               new Reserva { Id = 3, NombreCliente="Mario Perez Soza", Celular = "963258745", FechaReserva = DateTime.Now, MesaId = 3, Mesa = new Mesa(){ } },
               new Reserva { Id = 4, NombreCliente="Juan Galvez Soza", Celular = "963258345", FechaReserva = DateTime.Now, MesaId = 4, Mesa = new Mesa(){ } },

            }.AsQueryable();
        }
    }
}
