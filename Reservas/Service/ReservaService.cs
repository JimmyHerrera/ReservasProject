
using Microsoft.EntityFrameworkCore;
using Reservas.Data;
using Reservas.Interfaces;
using Reservas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservas.Service
{
    public class ReservaService : IReserva
    {
        private readonly ReservasDbContext _context;

        public ReservaService(ReservasDbContext _context)
        {
            this._context = _context;
        }

        public void createReserva(Reserva reserva)
        {
            _context.Reserva.Add(reserva);
            _context.SaveChanges();
        }

        public IEnumerable<Reserva> getLista()
        {
            return _context.Reserva.Include(m => m.Mesa).ToList();
        }

    }
}
