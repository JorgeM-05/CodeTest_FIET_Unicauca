using System.ComponentModel.DataAnnotations;

namespace Generator.Api.Models
{
    public class CasBus
    {
        [Key]

        public int CAID { get; set; } 

        public int Columns { get; set; } 

        public int Strength { get; set; } 

        public string Alphabet { get; set; } 

        public int State { get; set; } 

        public int IdRepository { get; set; }
        
    }
}
