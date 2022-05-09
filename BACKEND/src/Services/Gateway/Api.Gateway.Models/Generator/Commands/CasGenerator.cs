using MediatR;

namespace Api.Gateway.Models.seeker.Commands
{
    public class CasGenerator
    {
        
        public int Columns { get; set; } //Columnas
        public int Strength { get; set; } // fuerza
        public string Alphabet { get; set; } //Alfabeto
        public int Rows { get; set; } //Columnas
        
    }
}
