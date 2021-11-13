using Reservas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservas.Interfaces
{
    public interface IReserva
    {
        IEnumerable<Reserva> getLista();
        void createReserva(Reserva reserva);
        Reserva getReserva(int? id);
    }
}
