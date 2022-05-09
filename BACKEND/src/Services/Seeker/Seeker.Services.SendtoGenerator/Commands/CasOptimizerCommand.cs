using MediatR;

namespace Seeker.Services.Send.Commands
{
    public class CasOptimizerCommand : INotification 
    {
        public int Columns { get; set; } //Columnas
        public int Strength { get; set; } // fuerza
        public string Alphabet { get; set; } //Alfabeto
        public int Rows { get; set; } //Columnas
        //public string CA_notes { get; set; } // Arreglo de covertura
    }
}
