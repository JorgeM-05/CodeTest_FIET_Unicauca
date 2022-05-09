using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Generator.Services.Commands
{
    public class CasSeekertoGeneratorlogic : INotification
    {
        
        public int Columns { get; set; } //Columnas
        public int Strength { get; set; } // fuerza
        public string Alphabet { get; set; } //Alfabeto
        public int Rows { get; set; } //Columnas
        
    }
}
