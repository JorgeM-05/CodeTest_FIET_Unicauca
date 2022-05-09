using System.ComponentModel.DataAnnotations;

namespace API.Seeker.Models
{
    public class CaRepository
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
}
