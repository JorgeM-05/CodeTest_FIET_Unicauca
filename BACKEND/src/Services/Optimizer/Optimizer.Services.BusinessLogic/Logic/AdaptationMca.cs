using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimizer.Services.BusinessLogic.Logic
{
    public  class AdaptationMca
    {
        public int k;
        public int n;
        public string Ca;
        public int[] v;
        private string caRequired;
        private int[][] mCa;

        public AdaptationMca()
        {
        }

        public void convertirMC(string _CaRequired, int n, int k, int [][] MCa, int [] v)
        {
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < k; j++)
                {
                    if (MCa[i][j] >= v[j])
                        MCa[i][j] = -1;
                }
            }
        }

        public void borrark_t(int n, int k, int[][] MCa, int[] v, int t, out int[][] NewMca)
        {

            int comodineskt = k-t+1;
            int[] fila;
            int tamArray;
            List<int[]> arraylist = new List<int[]>();
            List<int>[] indexComodin = new List<int>[k];

            //primer ciclo para llenar el array de comodines
            for (int i=0; i < k; i++)
            {
                indexComodin[i] = new List<int>();
            }
            // ciclo para llenar un nuevo array sin filas repetidas y con comodines menores a k-t
            for(int i= 0; i< n; i++)
            {
                fila = getFila(MCa, k, i);
                if (!Contains(arraylist,fila,k))
                {
                    int comodinesfila = 0;
                    //verificamos la cantidad de comodines y si son menores k-t NO se adicionan al nuevo arraylist 
                    for(int j=0; j< k; j++)
                    {
                        if(fila[j] == -1)
                        {
                            comodinesfila++;
                        }
                    }
                    if (comodinesfila < comodineskt)
                    {
                        tamArray = arraylist.Count;
                        indexComodin[comodinesfila].Add(tamArray );
                        //indexComodin[comodinesfila].Add(i);
                        arraylist.Add(fila);
                    }

                    //indexComodin[comodinesfila].Add(i);
                    //arraylist.Add(fila);
                    //tamArray = arraylist.Count;
                    //indexComodin[comodinesfila].Add(tamArray-1);
                }
            }
            int n1 = arraylist.Count; //nueva tamaño de filas
            //int fil = 0;
            //int[][] matriz = new int[n1][];
            NewMca = new int[n1][];
            reconstruirMca(n1, NewMca, indexComodin, arraylist, k);
            // como ultimo paso queda guardar paramentros en MCa 
            // todo de pende de si se ofrece el original mas adelante....investigar.
        }
        public void borrarRepetida(int n, int k, int[][] MCa, int[] v)
        {

        }

        //metodo para llenar un array con la posicion i del Mca.
        public int[] getFila(int[][] Mca, int k, int index)
        {
            int[] vectFila = new int[k];
            for (int i = 0; i < k; i++)
            {
                vectFila[i] = Mca[index][i];
            }
            return vectFila;
        }

        //metodo encargado de buscar si dentro del la lista ya se encuentra el mismo array
        private bool Contains(List<int[]> arraylist, int[] fila, int k)
        {
            int max = arraylist.Count;
            int count = 0;
            // si el arraylist esta vacio devolvemos un false para que guarde.
            if(max == 0)
            {
                return false;
            }
            else{
                 for (int i = 0; i < max; i++)
                 {
                        count = 0;
                        for (int j = 0; j < k; j++)
                        {
                            if (arraylist[i][j] == fila[j])
                            {
                                count++;
                            }
                            //else
                            //{// sino es igual no es necesario recorrer el resto del vector
                            //    break;
                            //}
                        }
                        if (count == k) return true;
                        count = 0;
                  }
                return false;
            }
            
            
        }

        private void reconstruirMca(int n1, int[][] NewMca, List<int>[] indexComodin, List<int[]> arraylist, int k)
        {
            int fil = 0;
            int[][] matriz = new int[n1][];
            //NewMca = new int[n1][];
            
            for (int c = 0; c < indexComodin.Length; c++)
            {
                for (int iCom = 0; iCom < indexComodin[c].Count; iCom++)
                {
                    NewMca[fil] = new int[k];
                    for (int col = 0; col < k; col++)
                    {
                        //arraylist[indexComodin[c][iCom]][col]
                        NewMca[fil][col] = arraylist[indexComodin[c][iCom]][col];
                    }
                    fil++;
                }
            }
        }
    }
}
