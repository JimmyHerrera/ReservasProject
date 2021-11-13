
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
            var id = reserva.MesaId;

            var ad = _context.Reserva.Where(a => a.FechaReserva == reserva.FechaReserva).ToList();

            Mesa mesae = _context.Mesa.Where(a => a.Id == id).FirstOrDefault();
            mesae.Estado = 3;
            _context.Update(mesae);
            _context.SaveChanges();
        }

    

        public IEnumerable<Reserva> getLista()
        {
            return _context.Reserva.Include(m => m.Mesa).ToList();
        }

        public IEnumerable<Reserva> getLista(string criterio)
        {
            var query = from p in _context.Reserva
                        select p;

            if (!string.IsNullOrEmpty(criterio))
            {
                query = from p in query
                        where p.NombreCliente.ToUpper().Contains(criterio)
                        select p;
            }

            return query.Include(a => a.Mesa).ToList();

        }

        public Reserva getReserva(int? id)
        {
            return _context.Reserva.Where(a => a.Id == id).Include(m => m.Mesa).FirstOrDefault();
        }


    }
}
