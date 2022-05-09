using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimizer.Services.BusinessLogic.Commands
{
    public class DictionaryModel
    {
             public int Columns { get; set; }

            public int Strength { get; set; }

            public string Alphabet { get; set; }

            public string Alphabet_User { get; set; }

            public int Rows { get; set; }

            public string CA_notes { get; set; }

            public int Aux { get; set; }
    }
}
