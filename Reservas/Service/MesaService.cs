using Reservas.Data;
using Reservas.Interfaces;
using Reservas.Migrations;
using Reservas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservas.Service
{
    public class MesaService : IMesa
    {
        private readonly ReservasDbContext _context;

        public MesaService(ReservasDbContext _context)
        {
            this._context = _context;
        }

        public void createMesa(Mesa mesa)
        {
            _context.Mesa.Add(mesa);
            _context.SaveChanges();
        }

        public IEnumerable<Mesa> getLista()
        {
            return _context.Mesa;
        }

        public IEnumerable<Mesa> getMesaById(int? id)
        {
            return _context.Mesa.Where(m => m.Estado == 1).ToList();
        }

    }
}
