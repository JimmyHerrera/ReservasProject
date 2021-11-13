using Microsoft.AspNetCore.Mvc;
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
        private readonly ReservasDbContext _context;
        private readonly IReserva reservasInterface;

        public ReservaController(ReservasDbContext context, IReserva reservaInterface)
        {
            _context = context;
            this.reservasInterface = reservaInterface;
        }

        //Http Get Index
        public IActionResult Index()
        {
            var listaMesas = reservasInterface.getLista();
            //IEnumerable<Mesa> listMesas = _context.Mesa;
            return View(listaMesas);
        }

        //Http Get Create
        public IActionResult Create()
        {
            ViewBag.Mesa = _context.Mesa.Where(m=> m.Estado == 1).ToList();
            return View();
        }

        //Http Post Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                reservasInterface.createReserva(reserva);
                reserva.Mesa.Estado = 2;
                TempData["mensaje"] = "La reserva se ha creado correctamente";
                return RedirectToAction("Index");
            }

            return View();
        }

        //Http Get Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //Obtener el reserva

            var reserva = _context.Reserva.Find(id);

            if (reserva == null)
            {
                return NotFound();
            }
            ViewBag.Mesa = _context.Mesa.Where(m => m.Estado == 1).ToList();
            return View(reserva);
        }

        //Http Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                _context.Reserva.Update(reserva);
                _context.SaveChanges();

                TempData["mensaje"] = "La reserva se ha editado correctamente";
                return RedirectToAction("Index");
            }

            return View();
        }

        //Http Get Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //Obtener la mesa

            var reserva = _context.Reserva.Find(id);

            if (reserva == null)
            {
                return NotFound();
            }
            ViewBag.Mesa = _context.Mesa.Where(m => m.Estado == 1).ToList();

            return View(reserva);
        }

        //Http Post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteReserva(int? id)
        {
            //Obtener mesa por id

            var reserva = _context.Reserva.Find(id);

            if (reserva == null)
            {
                return NotFound();
            }


            _context.Reserva.Remove(reserva);
            _context.SaveChanges();

            TempData["mensaje"] = "La reserva se ha eliminado correctamente";
            return RedirectToAction("Index");

        }

    }
}
