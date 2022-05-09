using System.ComponentModel.DataAnnotations;

namespace API.Seeker.Models
{
    public class CaRepositoryDto
    {
        [Key]
        
        public long CAID { get; set; } //Id de arreglo
        public int Columns { get; set; } //Columnas
        public int Strength { get; set; } // fuerza
        public string Alphabet { get; set; } //Alfabeto
        public int Rows { get; set; } //Columnas
        public string CA_notes { get; set; } // Arreglo de covertura
        public int Aux { get; set; } //auxiliar

    }
    // Dto nos sirve para proteger y evitar que se edite las clases del dominio
}
