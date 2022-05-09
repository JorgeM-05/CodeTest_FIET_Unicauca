using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Gateway.Models.seeker.Commands
{
    public class CasCreateCommandSeeker 
    {
        public int CAID { get; set; } //Id de arreglo
        public int Columns { get; set; } //Columnas
        public int Strength { get; set; } // fuerza
        public string Alphabet { get; set; } //Alfabeto
        public int Rows { get; set; } //Columnas
        public string CA_notes { get; set; } // Arreglo de covertura

    }
}
