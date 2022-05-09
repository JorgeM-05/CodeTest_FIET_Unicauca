using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Api.Gateway.Services.Commands
{
    public class CasOptLogic 
    {

        public int CAID { get; set; }

        public int Columns { get; set; }

        public int Strength { get; set; }

        public string Alphabet { get; set; }

        public string TarjetAlphabet { get; set; }

        public int Rows { get; set; }

        public string CA_notes { get; set; }

        public int Aux { get; set; }

    }
}
