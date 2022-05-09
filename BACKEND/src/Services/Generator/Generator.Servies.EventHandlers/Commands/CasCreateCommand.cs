using MediatR;

namespace Generator.Servies.SendtoSeeker.Commands
{
    
    public class CasCreateCommand: INotification
    {
        public long CAID { get; set; } //Id de arreglo
        public int Columns { get; set; } //Columnas
        public int Strength { get; set; } // fuerza
        public string Alphabet { get; set; } //Alfabeto
        public int Rows { get; set; } //Columnas
        public string CA_notes { get; set; } // Arreglo de covertura

    }
}
