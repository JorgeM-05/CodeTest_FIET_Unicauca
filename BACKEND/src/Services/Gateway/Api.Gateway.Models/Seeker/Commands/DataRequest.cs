using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Api.Gateway.Models.seeker.Commands
{
    public class DataRequest
    {
        
     
        public IDataProject DataProject { get; set; }
        public IPropertiesProject PropertiesProject { get; set; }
        //public ICollection<IPropertiesProject> PropertiesProject { get; set; }

        public ICollection<IVariables> Variables { get; set; }
  
        public string valores { get; set; }


}
    [Serializable]
    public class IDataProject
    {
        public string _idProject { get; set; }
        public string metodo { get; set; }
        public bool estatico { get; set; }
        public string strength { get; set; }
        public string tipo_de_metodo { get; set; }

    }

    [Serializable]
    public class IPropertiesProject
    {
        public string company { get; set; }
        public string fecha { get; set; }
        public string nombre { get; set; }
        public string _idlist { get; set; }

    }

    [Serializable]
    public class IVariables
    {
        public string nombre_var { get; set; }
        public string tipo_de_variable { get; set; }
        public string valores { get; set; }
        public string _idproject { get; set; }
        public string _idvariable { get; set; }

    }


}
