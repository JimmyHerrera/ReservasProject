using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;
using Reservas.Controllers;
using Reservas.Interfaces;
using Reservas.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PruebasUnitarias.TestControladorReserva
{
    public class controllerTestReserva
    {
        private Mock<IReserva> reservaRepository;
        private Mock<IMesa> mesaRepository;


        [SetUp]
        public void SetUp()
        {
            reservaRepository = new Mock<IReserva>();
            mesaRepository = new Mock<IMesa>();
        }

        [Test]
        public void TestIndexListaReservasIsOkCase01()
        {

            reservaRepository.Setup(a => a.getLista("")).Returns(new List<Reserva>());

            var controller = new ReservaController(reservaRepository.Object, null);

            var view = controller.Index("") as ViewResult;

            Assert.IsInstanceOf<List<Reserva>>(view.Model);

        }

        [Test]
        public void TestCreateReservaIsOkCase02()
        {
            reservaRepository.Setup(a => a.createReserva(new Reserva()));

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            tempData["mensaje"] = "La reserva se ha creado correctamente";

            var controller = new ReservaController(reservaRepository.Object,null)
            {
                TempData = tempData
            };

            var view = controller.Create(new Reserva());

            Assert.IsInstanceOf<RedirectToActionResult>(view);
        }

    }
}
