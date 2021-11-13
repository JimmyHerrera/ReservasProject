using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservas.Models
{
    public class HorarioMesa
    {
        public int ID { get; set; }
        public  DateTime horaFecha { get; set; }
        public bool state { get; set; }
    }
}
