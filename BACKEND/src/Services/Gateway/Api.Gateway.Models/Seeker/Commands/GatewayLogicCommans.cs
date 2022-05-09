using MediatR;

namespace Api.Gateway.Logic
{
    public class GatewayLogicCommans : INotification
    {
        public int Columns { get; set; } //Columnas
        public int Strength { get; set; } // fuerza
        public string Alphabet { get; set; } //Alfabeto
    }
}