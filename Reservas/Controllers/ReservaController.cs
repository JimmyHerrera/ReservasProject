using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reservas.Data;
using Reservas.Interfaces;
using Reservas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservas.Controllers
{
    public class ReservaController : Controller
    {
        private readonly IReserva reservasInterface;
        private readonly IMesa mesasInterface;


        public ReservaController(IReserva reservaInterface,IMesa mesasInterface)
        {
            this.reservasInterface = reservaInterface;
            this.mesasInterface = mesasInterface;
        }

        [Authorize]
        public IActionResult Index(string? criterio)
        {
           var listaMesas = reservasInterface.getLista(criterio);
            return View(listaMesas);
        }


        [Authorize]
        public IActionResult Create()
        {
            ViewBag.Mesa = mesasInterface.getMesaByState();
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                reservasInterface.createReserva(reserva);
                TempData["mensaje"] = "La reserva se ha creado correctamente";
                return RedirectToAction("Index");
            }

            return View();
        }

        [Authorize]
        public IActionResult Edit(int? id)
        {
            var reserva = reservasInterface.getReserva(id);
            ViewBag.Mesa = mesasInterface.getMesaByState();
            return View(reserva);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Reserva reserva,int idR)
        {
            if (ModelState.IsValid)
            {
                reservasInterface.updateReserva(reserva, idR);
                TempData["mensaje"] = "La reserva se ha editado correctamente";
                return RedirectToAction("Index");
            }

            return View();
        }

        [Authorize]
        public IActionResult Delete(int? id)
        {
            var reserva = reservasInterface.getReserva(id);
            return View(reserva);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteReserva(int? id)
        {
            reservasInterface.deleteReserva(id);

            TempData["mensaje"] = "La reserva se ha eliminado correctamente";
            return RedirectToAction("Index");

        }

    }
}
