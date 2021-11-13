using Microsoft.AspNetCore.Mvc;
using Reservas.Data;
using Reservas.Migrations;
using Reservas.Models;
using Reservas.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservas.Controllers
{
    public class MesasController : Controller
    {
        private readonly ReservasDbContext _context;
        private readonly IMesa mesaInterface;

        public MesasController(ReservasDbContext context, IMesa mesaInterface)
        {
            _context = context;
            this.mesaInterface = mesaInterface;
        }

        //Http Get Index
        public IActionResult Index()
        {
            var listaMesas = mesaInterface.getLista();
            return View(listaMesas);
        }

        //Http Get Create
        public IActionResult Create()
        {
            return View();
        }

        //Http Post Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Mesa mesa)
        {
            if (ModelState.IsValid)
            {
                mesaInterface.createMesa(mesa);
                TempData["mensaje"] = "La mesa se ha creado correctamente";
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

            //Obtener el libro

            var mesa = _context.Mesa.Find(id);

            if(mesa == null)
            {
                return NotFound();
            }

            return View(mesa);
        }

        //Http Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Mesa mesa)
        {
            if (ModelState.IsValid)
            {
                _context.Mesa.Update(mesa);
                _context.SaveChanges();

                TempData["mensaje"] = "La mesa se ha editado correctamente";
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

            var mesa = _context.Mesa.Find(id);

            if (mesa == null)
            {
                return NotFound();
            }

            return View(mesa);
        }

        //Http Post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteMesa(int? id)
        {
            //Obtener mesa por id

            var mesa = _context.Mesa.Find(id);

            if (mesa == null)
            {
                return NotFound();
            }


            _context.Mesa.Remove(mesa);
            _context.SaveChanges();

            TempData["mensaje"] = "La mesa se ha eliminado correctamente";
            return RedirectToAction("Index");

        }
    }
}
