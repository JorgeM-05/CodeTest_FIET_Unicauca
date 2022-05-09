
using Optimizer.Services.business_logic.Logica;
using Optimizer.Services.business_logic.Logica.MetaHeuristicas;
using Optimizer.Services.BusinessLogic.Logic;
using System;
using Optimizer.Services.business_logic.Logica.MetaHeuristicas.IteradosSA;

namespace Optimizer.Services.BusinessLogic
{
    public class AlgoritmoPostOptimicer
    {
        
        private readonly string _CaRequired;
        private readonly string _MCa;
        private readonly string _TarjetAlphabet;
        private readonly int _num_Variables;
        public AlgoritmoPostOptimicer(string MAtrizCA, string theRequiredCA, string TarjetAlphabet, int num_Variables)
        {
            _CaRequired = theRequiredCA;
            _MCa = MAtrizCA;  
            _TarjetAlphabet = TarjetAlphabet;
            _num_Variables = num_Variables;
        }
        
        public Solucion Ejecutar()
        {

            ConfiguracionCA(_MCa, _CaRequired, _TarjetAlphabet,_num_Variables, out int n, out int k, out int t, out int[] v, out int[] tarjetV, out int[][] MCa);

            var adpatationMca = new AdaptationMca();
            var combinationRow = new CombinationRows();
            var seekerComodines = new SeekerWildcard();
            Algoritmo miPost = null;
           
            k = tarjetV.Length;
            adpatationMca.convertirMC(_CaRequired, n, k, MCa, tarjetV); //convierte el ca a comodines
            
            //1. borrar filas con k-t comodines y ordenarlas

            adpatationMca.borrark_t(n, k, MCa, tarjetV, t, out int[][] NewMca);
            MCa = NewMca;
            n = NewMca.Length;

            //2. verificar y buscar comodines un ca

            miPost = new ISA(1, 0, n, k, tarjetV, t, 1, _CaRequired, MCa);
            miPost.Execute();
            //if (miPost.Best.Fitness > 0)
            //{
            //    //crear excepcion que no es posible "no es CA"
            //    return null;
            //}

            //seekerComodines.seekerCom(n, MCa, v, t);

            //4. Ordena y  borrar filas con k-t comodines 
            adpatationMca.borrark_t(n, k, MCa, v, t, out int[][] NewMca2);
            MCa = NewMca2;
            n = MCa.Length;

            //5. combinar filas
            //repetir (1,2,..n) veces el mismo procedimiento
            int nanterior = n;
            int nactual = 0;
            while (nanterior != nactual) {
                nanterior = n;
                combinationRow.combinationsRows(n, k, MCa, v, t, out int [][] MCaux);
                
                n = MCaux.Length;
                nactual = n;
                MCa = MCaux;
            }

            //6. verificar y asignar nuevos valores a los comodines sobrantes
            miPost = new ISA(2, 0, n, k, tarjetV, t, 1, _CaRequired, MCa);

            miPost.Execute();
            
            if (miPost.Best.Fitness > 0)
            {
                //crear excepcion que no es posible "no es CA"
                return miPost.Best;
            }
            //7. completar comodines con valores faltantes


            return miPost.Best;
        }

        static void ConfiguracionCA(string fileMcas, string fileName, string TarjetAlphabet,int _num_Variables, out int n, out int k, out int t, out int[] v, out int[] tarjetV, out int[][] MCAs)
        {
            var posicion = fileName.IndexOf('K') -1;
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

            tarjetV = new int[_num_Variables];
            var valuesdiv2 = TarjetAlphabet.Split('-');
            var pos2 = 0;
            foreach (var actual2 in valuesdiv2)
            {
                var div = actual2.Split('^');
                var val = int.Parse(div[0]);
                var col = int.Parse(div[1]);
                for (var i = 0; i < col; i++)
                    tarjetV[pos2++] = val;
            }



            var valuediv = fileMcas.Split('\n');
            var tam=0;
            MCAs = new int[n][];
          
            foreach (var aux in valuediv)
            {
                if(aux != "")
                {
                    MCAs[tam] = new int[_num_Variables];
                    var auxI = aux.Split(' ');

                    for (var j = 0; j < _num_Variables; j++)
                    {
                        MCAs[tam][j] = int.Parse(aux.Split(' ')[j]);
                    }
                    tam++;
                }
            }

        }
    }
}
