using Microsoft.AspNetCore.Authorization;
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
        private readonly IMesa mesasInterface;


        public ReservaController(ReservasDbContext context, IReserva reservaInterface,IMesa mesasInterface)
        {
            _context = context;
            this.reservasInterface = reservaInterface;
            this.mesasInterface = mesasInterface;
        }

        [Authorize]
        //Http Get Index
        public IActionResult Index()
        {
            var listaMesas = reservasInterface.getLista();
            //IEnumerable<Mesa> listMesas = _context.Mesa;
            return View(listaMesas);
        }

        [Authorize]
        //Http Get Create
        public IActionResult Create()
        {
            ViewBag.Mesa = _context.Mesa.Where(m=> m.Estado == 1).ToList();
            return View();
        }

        [Authorize]
        //Http Post Create
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
        //Http Get Edit
        public IActionResult Edit(int? id)
        {
            var reserva = reservasInterface.getReserva(id);
            ViewBag.Mesa = mesasInterface.getMesaById(id);
            return View(reserva);
        }
        [Authorize]
        //Http Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Reserva reserva,int id)
        {
            if (ModelState.IsValid)
            {
                if(id == reserva.MesaId)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var a = _context.Reserva.Where(i => id == reserva.Id).FirstOrDefault();
                    a.MesaId = reserva.MesaId;
                    _context.Reserva.Update(a);
                    _context.SaveChanges();

                   var tmp = _context.Mesa.Where(a => a.Id == id).FirstOrDefault();
                    tmp.Estado = 1;

                    _context.Update(tmp);
                    _context.SaveChanges();

                    var mesa = _context.Mesa.Where(a => a.Id == reserva.MesaId).FirstOrDefault();
                    mesa.Estado = 3;
                    
                    _context.Update(mesa);
                    _context.SaveChanges();

                   
                }


                TempData["mensaje"] = "La reserva se ha editado correctamente";
                return RedirectToAction("Index");
            }

            return View();
        }
        [Authorize]
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
        [Authorize]
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
