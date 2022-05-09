using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Gateway.Models.seeker.Commands
{
    public class CasSeekerCommandProxies
    {
        public int Columns { get; set; } //Columnas
        public int Strength { get; set; } // fuerza
        public string Alphabet { get; set; } //Alfabeto
        public string Alphabet_user { get; set; }
                //public string CA_notes { get; set; } // Arreglo de covertura
    }
}
