using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimizer.Services.BusinessLogic.Logic
{
    public  class CombinationRows
    {
        public CombinationRows()
        {
        }

        public void combinationsRows (int n, int k, int[][] MCa, int[] v, int t, out int[][] MCaux)
        {
         
            List<ElementVector> FinalElements = new List<ElementVector>();
            List<ElementVector> elements = new List<ElementVector>();
            ElementVector elemento;
            List<int> marcados = new List<int>();
            int[] A = new int[k];
            int[] B = new int[k];
            for (int i = 0; i < n; i++) 
            {
                A = this.getFila(MCa, k, i);
                if (!marcados.Contains(i))
                {
                    for (int j = i + 1; j < n; j++)
                    {
                        if (marcados.Contains(j)) continue;
                        elemento = new ElementVector(k);

                        B = this.getFila(MCa, k, j);
                        // cuantos comodines, que filas se mezclaron, cuantos en comun
                        elemento = SumaVectores(A, B, k, i, j);
                        // si llegan -1 -1 -1 -1 es porque no se pueden combinar
                        if (!elemento.IsEmpty(k)) elements.Add(elemento);//agrega a una lista de elementos
                    }
                    if (elements.Count != 0)
                    {
                        elemento = mejor(elements, k, t);
                        marcados.Add(elemento.filaB); 
                        FinalElements.Add(elemento);
                    }
                    else
                    {
                        elemento = new ElementVector(A, k, i, -1); 
                        FinalElements.Add(elemento);
                    }
                    elements.Clear();
                }
            }
            //mostrar(FinalElements, k);
            n = FinalElements.Count;
            MCaux = new int[n][];
            int col = 0;
            ElementVector el;
            for (int i = 0; i < n; i++)
            {
                MCaux[col] = new int[k];

                for (int j = 0; j < k; j++)
                {
                    MCaux[i][j] = FinalElements[i].contenido[j];
                }
                col++;
            }
          
        }


        public int[] getFila(int[][] Mca, int k, int index)
        {
            int[] vectFila = new int[k];
            for (int i = 0; i < k; i++)
            {
                vectFila[i] = Mca[index][i];
            }
            return vectFila;
        }


        public ElementVector SumaVectores(int[] a, int[] b, int k, int fa, int fb)
        {
            ElementVector e = new ElementVector(k);
            int comodines = 0;
            int[] C = new int[k];
            for (int i = 0; i < k; i++)
            {
                if (a[i] == b[i])
                {
                    if (a[i] != -1) e.comunes++;
                    C[i] = a[i];
                }
                else
                {
                    if (b[i] == -1)
                    {
                        C[i] = a[i];
                    }
                    else
                    {
                        if (a[i] == -1)
                        {
                            C[i] = b[i];
                        }
                        else
                        {
                            e.contenido = e.newEmptyElement(k);
                            return e;
                        }
                    }
                }
            }
            comodines = 0;
            for (int z = 0; z < k; z++)
            {
                if (C[z] == -1) comodines++;
            }
            e.comodines = comodines;
            e.filaA = fa;
            e.filaB = fb;
            e.contenido = C;
            return e;
        }


        /// <summary>
        /// toma una lista de elementos y retorma en que tenga mayores posiblidades de ser mezclado con otro
        /// </summary>
        /// <param name="elements">lista de elementos candidatos</param>
        /// <param name="k">tamaño de los elementos</param>
        /// <param name="t">fuerza</param>
        /// <returns>el mejor elemento encontrado</returns>
        private ElementVector mejor(List<ElementVector> elements, int k, int t)
        {
            //  return mejorMenosComodines(elements, k, t);
            return mejorMasComodines(elements, k, t);
        }


        /// <summary>
        /// devuelve como mejor el elemento que tenga la mayor cantidad de comodines
        /// </summary>
        /// <param name="elements">elementos a evaluar</param>
        /// <param name="k">tamaño de los elementos</param>
        /// <param name="t">fuerza</param>
        /// <returns>el mejor elemento encontrado</returns>
        private ElementVector mejorMasComodines(List<ElementVector> elements, int k, int t)
        {
            ElementVector e = new ElementVector(k);
            int masComunes = elements[0].comunes;
            List<ElementVector> mejores = new List<ElementVector>();
            for (int i = 1; i < elements.Count; i++)
            {
                if (elements[i].comunes > masComunes)
                {
                    masComunes = elements[i].comunes;
                    if (masComunes == k) break;
                }
            }
            int masComodines = 0;
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].comunes == masComunes)
                {
                    mejores.Add(elements[i]);
                    if (elements[i].comodines > masComodines)
                        masComodines = elements[i].comodines;
                }
            }
            List<ElementVector> mejoresMasComodines = new List<ElementVector>();
            for (int i = 0; i < mejores.Count; i++)
            {
                if (mejores[i].comodines == masComodines)
                {
                    mejoresMasComodines.Add(mejores[i]);
                    break;
                }
            }
            if (mejoresMasComodines.Count > 0)
            {
                e = mejoresMasComodines[0];
            }
            return e;
        }

        /// <summary>
        /// muestra la listade elementos que pasaron a la nueva matriz
        /// </summary>
        /// <param name="FinalElements">lista d eelelemntos sque pasaron</param>
        /// <param name="k">tamaño de ños elementos</param>
        private void mostrar(List<ElementVector> FinalElements, int k)
        {
            string s = "";
            for (int i = 0; i < FinalElements.Count; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    s = s + " " + FinalElements[i].contenido[j];
                }
                s = s.Replace("-1", "-");
                s = "";
            }
        }
    }
}
