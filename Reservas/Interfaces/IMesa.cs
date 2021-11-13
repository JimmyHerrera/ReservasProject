﻿using Reservas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservas.Interfaces
{
    public interface IMesa
    {
        IEnumerable<Mesa> getLista();
        IEnumerable<Mesa> getMesaById(int? id);
        void createMesa(Mesa mesa);
    }
}
