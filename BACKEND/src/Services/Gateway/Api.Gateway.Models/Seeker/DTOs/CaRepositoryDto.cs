using System.ComponentModel.DataAnnotations;

namespace Api.Gateway.Models.seeker.DTOs
{ 
    public class CaRepositoryDto
    {
        [Key]
        
        public long CAID { get; set; } 

        public int Columns { get; set; } 

        public int Strength { get; set; } 

        public string Alphabet { get; set; } 

        public int Rows { get; set; } 

        public string CA_notes { get; set; } 

        public int Aux { get; set; } 

    }
    // Dto nos sirve para proteger y evitar que se edite las clases del dominio
}
