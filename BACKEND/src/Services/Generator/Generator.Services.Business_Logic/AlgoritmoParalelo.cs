

using Generator.Services.business_logic.Logica.MetaHeuristicas;
using Generator.Services.business_logic.Logica.MetaHeuristicas.BusquedaArmonica;
using Generator.Services.business_logic.Logica.MetaHeuristicas.IteradosSA;

namespace Generator.Services.business_logic
{
    public class AlgoritmoParalelo
    {
        private readonly int _tarSemilla;
        private readonly string _tarAlgoritmo;
        private readonly string _tarCARequerido;
        private readonly string _tarParametros;

        public AlgoritmoParalelo(string algoritmo, string caRequerido,
            int semilla, string parametros)
        {
            _tarAlgoritmo = algoritmo;
            _tarCARequerido = caRequerido;
            _tarSemilla = semilla;
            _tarParametros = parametros;
        }

        public Solucion Ejecutar()
        {
            var parametros = _tarParametros.Split(' ');

            ConfiguracionCA(_tarCARequerido, out int n, out int k, out int t, out int[] v);

            Algoritmo miAlgoritmo = null;
            switch (_tarAlgoritmo)
            {
                case "ISA":
                    miAlgoritmo = new ISA(int.Parse(parametros[0]), int.Parse(parametros[1]), n, k, v, t,
                                          _tarSemilla, _tarCARequerido);
                    break;
                case "ISAE":
                    miAlgoritmo = new ISAE(int.Parse(parametros[0]), int.Parse(parametros[1]), n, k, v, t,
                                         _tarSemilla, _tarCARequerido);
                    break;
                case "GHSSA":
                    miAlgoritmo = new GHSSA(int.Parse(parametros[0]), int.Parse(parametros[1]), n, k, v, t,
                                        _tarSemilla, _tarCARequerido,
                                        /*hms=*/ 4, 0.85f, 0.1f, 0.4f);
                    break;
            }
            if (miAlgoritmo == null)
                return null;

            miAlgoritmo.Execute();
            return miAlgoritmo.Best;
        }

        static void ConfiguracionCA(string fileName, out int n, out int k, out int t, out int[] v)
        {
            var posicion = fileName.IndexOf('K') - 1;
            var res = fileName.Substring(1, posicion);
            n = int.Parse(res);

            fileName = fileName.Substring(posicion + 1);
            posicion = fileName.IndexOf('V') - 1;
            res = fileName.Substring(1, posicion);
            k = int.Parse(res);

            fileName = fileName.Substring(posicion + 1);
            posicion = fileName.IndexOf('t') - 1;
            var values = fileName.Substring(1, posicion);

            fileName = fileName.Substring(posicion + 1);
            posicion = fileName.IndexOf('.') - 1;
            res = fileName.Substring(1, posicion);
            t = int.Parse(res);

            v = new int[k];
            var valuesdiv = values.Split('-');
            var pos = 0;
            foreach (var actual in valuesdiv)
            {
                var div = actual.Split('^');
                var val = int.Parse(div[0]);
                var col = int.Parse(div[1]);
                for (var i = 0; i < col; i++)
                    v[pos++] = val;
            }
        }
    }
}
