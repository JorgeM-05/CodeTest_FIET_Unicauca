namespace Api.Gateway.Models.seeker.Commands
{
    public class CasOptCommandSeeker
    {
        public int CAID { get; set; }
        
        public int Columns { get; set; } 

        public int Strength { get; set; }
        
        public string Alphabet { get; set; } 

        public int Rows { get; set; }

        public string CA_notes { get; set; } 

        public int Aux { get; set; }

        public string TarjetAlphabet { get; set; }
        public int TarjetColumns { get; set; }

    }
}
