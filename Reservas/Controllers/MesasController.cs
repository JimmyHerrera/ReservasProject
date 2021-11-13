using Microsoft.AspNetCore.Mvc;
using Reservas.Data;
using Reservas.Migrations;
using Reservas.Models;
using Reservas.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Reservas.Controllers
{
    public class MesasController : Controller
    {

        private readonly IMesa mesaInterface;

        public MesasController(IMesa mesaInterface)
        {
            this.mesaInterface = mesaInterface;
        }

        [Authorize]
        public IActionResult Index()
        {
            var listaMesas = mesaInterface.getLista();
            return View(listaMesas);
        }

       
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
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


        [Authorize]
        public IActionResult Edit(int? id)
        {
            var mesa = mesaInterface.getMesa(id);
            return View(mesa);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Mesa mesa)
        {
            if (ModelState.IsValid)
            {
                mesaInterface.updateMesa(mesa);

                TempData["mensaje"] = "La mesa se ha editado correctamente";
                return RedirectToAction("Index");
            }

            return View();
        }

        
        [Authorize]
        public IActionResult Delete(int? id)
        {
            var mesa = mesaInterface.getMesa(id);
            return View(mesa);
        }

        
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteMesa(int? id)
        {
            mesaInterface.deleteMesa(id);

            TempData["mensaje"] = "La mesa se ha eliminado correctamente";
            return RedirectToAction("Index");

        }
    }
}
