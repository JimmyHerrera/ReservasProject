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

namespace PruebasUnitarias.TestControladorMesa
{
    public class ControllerTestMesa
    {
        private Mock<IMesa> authRepository;

        [SetUp]
        public void SetUp()
        {
            authRepository = new Mock<IMesa>();

        }
        [Test]
        public void TestIndexListaMesasIsOkCase01()
        {

            authRepository.Setup(a => a.getLista()).Returns(new List<Mesa>());

            var controller = new MesasController(authRepository.Object);

            var view = controller.Index() as ViewResult;

            Assert.IsInstanceOf<List<Mesa>>(view.Model);

        }

        [Test]
        public void TestCreateMesaIsOkCase02()
        {
            authRepository.Setup(a => a.createMesa(new Mesa()));

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
        
            tempData["mensaje"] = "La reserva se ha creado correctamente";

            var controller = new MesasController(authRepository.Object)
            {
                TempData = tempData
            };

            var view = controller.Create(new Mesa());

            Assert.IsInstanceOf<RedirectToActionResult>(view);
        }

        [Test]
        public void TestEditReturnMesaIsOkCase03()
        {
            authRepository.Setup(a => a.getMesa(5)).Returns(new Mesa());

            var controller = new MesasController(authRepository.Object);

            var view = controller.Edit(5) as ViewResult;

            Assert.IsInstanceOf<Mesa>(view.Model);
        }

        [Test]
        public void TestEditMesaIsOkCase04()
        {
            authRepository.Setup(a => a.createMesa(new Mesa()));

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            tempData["mensaje"] = "La mesa se ha editado correctamente";

            var controller = new MesasController(authRepository.Object)
            {
                TempData = tempData
            };

            var view = controller.Edit(new Mesa());

            Assert.IsInstanceOf<RedirectToActionResult>(view);
        }

        [Test]
        public void TestDeleteReturnMesaIsOkCase05()
        {
            authRepository.Setup(a => a.getMesa(5)).Returns(new Mesa());

            var controller = new MesasController(authRepository.Object);

            var view = controller.Delete(5) as ViewResult;

            Assert.IsInstanceOf<Mesa>(view.Model);
        }

        [Test]
        public void TestDeleteMesaIsOkCase06()
        {
            authRepository.Setup(a => a.deleteMesa(5));

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            tempData["mensaje"] = "La mesa se ha eliminado correctamente";

            var controller = new MesasController(authRepository.Object)
            {
                TempData = tempData
            };

            var view = controller.DeleteMesa(5);

            Assert.IsInstanceOf<RedirectToActionResult>(view);
        }
    }
}
