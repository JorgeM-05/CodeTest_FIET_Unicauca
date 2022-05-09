using Optimizer.Services.business_logic.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimizer.Services.BusinessLogic.Logic
{
    public class SeekerWildcard
    {
        public SeekerWildcard()
        {

        }
       


        public bool seekerCom(int n, int[][] MCa, int[] v, int t)
        {

            AdaptationMca adapcion = new AdaptationMca();
            int k = MCa.GetUpperBound(0)+1; // este paso se puede omitir ya que ya se tiene k

            int [] Actual = new int[k];
            bool comodinEncontrado = false;
            List<int[]> combinaciones;
            int ctn =0;
            int[] index;
            int[] values = new int[t];


            for (int row = n-1; (row > -1 && !comodinEncontrado); row--)
            {
                Actual = adapcion.getFila(MCa, k, row);

                for(int colum = 0; colum<k; colum++)
                {
                    ctn = 0;
                    //MatrizP matrizP = new MatrizP(t, k, v);
                    combinaciones = combinarColum(Actual, colum, t, v);
                    //combinaciones = combinacionColunmna(Actual, colum,k, t);

                    for (var i = 0; i < combinaciones.Count; i++)
                    {
                        index = combinaciones[i];// posicion de j
                        for (int x = 0; x < index.Length; x++)
                        {
                            values[x] = Actual[index[x]];
                        }

                        //values es el valor seleccionado del la fila ej: 1 -1 0 -1     1  0
                        bool band = TestMCA.estaRepetida(index, values);
                        if (band)
                        {
                            ctn++;
                        }
                    }
                    if (ctn == combinaciones.Count && ctn != 0)
                    { //el valor de la columna es comodin
                        comodinEncontrado = true;
                        Actual[colum] = -1;
                        //m[filaActual, columna] = -1;
                    }

                }
            }
            

            return false;
        }

        private List<int[]> combinacionColunmna(int[] A, int columna, int k, int t)
        {
            List<int[]> comparaciones = new List<int[]>();
            for (int i = 0; i < k; i++)
            {
                if (t == 2 && A[columna] != -1)
                {
                    if (columna != i && columna < i && A[i] != -1)
                    {
                        int[] vec = new int[t];
                        vec[0] = columna;
                        vec[1] = i;
                        comparaciones.Add(vec);
                        vec = null;
                    }
                    if (columna != i && columna > i && A[i] != -1)
                    {
                        int[] vec = new int[t];
                        vec[0] = i;
                        vec[1] = columna;
                        comparaciones.Add(vec);
                        vec = null;
                    }
                }
            }
            return comparaciones;
        }


        private static List<int[]> combinarColum(int[] A, int columna, int t,int [] v)
        {

            int cont = 0;
            bool band = false;
            List<int[]> comparaciones = new List<int[]>();
            var tarea = new SeekerWildcard();

            int k = A.Length;
            //var lista = tarea.construirJ(k, t);
            MatrizP matrizP = new MatrizP(t, k, v, 2);
            var lista = matrizP.J;
           //compara  la matriz j con cada valor de la fila
            if (A[columna] != -1) 
                foreach (var lineaDeJ in lista)
                {
                    band = false;
                    cont = 0;
                    for (int j = 0; j < t; j++)
                    {
                        if (lineaDeJ[j] == columna)
                            band = true;

                        if (A[lineaDeJ[j]] != -1)
                            cont++;

                        if (cont == t && band)
                            comparaciones.Add(lineaDeJ);
                    }
                }
            return comparaciones;
        }



        public IEnumerable<PolinomioMayorQue> construirJ(int k, int t)
        {
            var j = new List<PolinomioMayorQue>();
            var lineaDeJ = new int[t];
            int i;
            int iMax;
            for (i = 0; i < t; i++)
            {
                lineaDeJ[i] = i;
            }
            var primero = new PolinomioMayorQue(t, lineaDeJ);
            j.Add(primero);
            for (iMax = t - 1, i = 0; i < t; i++)
            {
                if (lineaDeJ[i] == k - t + i)
                {
                    iMax = i;
                    break;
                }
            }
            do
            {
                lineaDeJ[t - 1]++;
                if (lineaDeJ[t - 1] == k)
                {
                    if (iMax == 0) break;
                    lineaDeJ[iMax - 1]++;
                    for (i = iMax; i < t; i++)
                        lineaDeJ[i] = lineaDeJ[i - 1] + 1;
                    if (lineaDeJ[iMax - 1] == k - t + iMax - 1)
                        iMax = iMax - 1;
                    else
                        iMax = t - 1;
                }
                var nuevo = new PolinomioMayorQue(t, lineaDeJ);
                j.Add(nuevo);
            } while (true);
            j.Sort();
            return j;
        }


        public static int Combinatoria(int numerador, int denominador)
        {
            if (denominador > numerador) return 0;
            var comb = 1.0;
            int menor = numerador - denominador;
            if (denominador < menor) menor = denominador;
            for (var i = 1; i <= menor; i++)
            {
                comb = (numerador * 1.0 / i) * comb;
                numerador--;
            }
            return int.Parse(comb.ToString());
        }

    }
}
