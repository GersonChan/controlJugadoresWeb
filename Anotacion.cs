using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace controlJugadoresWeb
{
    public class Anotacion
    {
        public string noIdentificacion { get; set; }
        public DateTime fecha { get; set; }
        public string golEquipo { get; set; }
        public int golAnotados { get; set; }
    }
}