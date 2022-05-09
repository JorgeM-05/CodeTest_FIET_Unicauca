using Optimizer.Services.business_logic.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimizer.Services.BusinessLogic.Logic
{
    public static class TestMCA
    {
        public static List<int[]> valoresRepetidos = new List<int[]>();
        public static List<int[]> indicesRepetidos = new List<int[]>();
        public static List<int[]> indicesFaltantes = new List<int[]>();
        public static List<int[]> valoresFaltantes = new List<int[]>();
        public static int T;
        public static int[] V;
        public static IEnumerable<PolinomioMayorQue> j;
        

        //public static bool Test(int[][] m, int[] v, int n, int k, int t)
        //{
        //    indicesRepetidos.Clear();
        //    valoresRepetidos.Clear();
        //    indicesFaltantes.Clear();
        //    valoresFaltantes.Clear();

        //    T = t;
        //    int[] posicion = new int[t];
        //    int[] valor = new int[t];
        //    var esCA = true;
        //    var ListaDeP = new List<int[]>();
        //    foreach (var lineaDeJ in j)
        //    {
        //        var tamanoP = MayorDimensionP(v,lineaDeJ.PosicionDeColumnas);

        //        var vectorP = new int[tamanoP];
        //        for (var fila = 0; fila < n; fila++)
        //        {
        //            var posicionEnP = DivisionSintetica(v, m, fila, lineaDeJ.PosicionDeColumnas);
        //            if (posicionEnP != -1)
        //                vectorP[posicionEnP] += 1;
        //        }
        //        ListaDeP.Add(vectorP);
        //        for (var pos = 0; pos < tamanoP; pos++)
        //        {
        //            if (vectorP[pos] == 0)
        //            {
        //                esCA = false;
        //                var x = MultiplicacionSintetica(v, lineaDeJ, pos, t);
        //                posicion = lineaDeJ.PosicionDeColumnas;
        //                valor = x;
        //                indicesFaltantes.Add(posicion);
        //                valoresFaltantes.Add(valor);
        //            }
        //        }
        //        for (var pos = 0; pos < tamanoP; pos++)
        //        {
        //            if (vectorP[pos] > 1)
        //            {
        //                for (var it = 1; it < vectorP[pos]; it++)
        //                {
        //                    var x = MultiplicacionSintetica(v, lineaDeJ, pos, t);
        //                    posicion = lineaDeJ.PosicionDeColumnas;
        //                    valor = x;
        //                    indicesRepetidos.Add(posicion);
        //                    valoresRepetidos.Add(valor);
        //                }
        //            }
        //        }
        //    }
        //    return esCA;
        //}
        
        public static bool estaRepetida(int[] index, int[] values)
        {
            int maxI = valoresRepetidos.Count;
            int maxJ = values.Length;
            int[] filaIndices;
            int contadorIndices;
            int contadorValores;
            for (int i = 0; i < maxI; i++)
            {
                filaIndices = indicesRepetidos[i];
                contadorIndices = 0;

                for (int j = 0; j < maxJ; j++)
                {
                    if (filaIndices[j] == index[j])
                    {
                        contadorIndices++;
                    }
                }
                if (contadorIndices == maxJ)
                {
                    var filaValores = valoresRepetidos[i];
                    contadorValores = 0;
                    for (int j = 0; j < maxJ; j++)
                    {
                        if (filaValores[j] == values[j])
                        {
                            contadorValores++;
                        }
                    }
                    if (contadorValores == maxJ)
                    {
                        return true;
                    }
                }
                contadorIndices = 0;
            }
            return false;
        }

        public static int MayorDimensionP(int[] v, int[] lineaDeJ)
        {
            var mul = 1;
            for (var i = 0; i < lineaDeJ.GetUpperBound(0) + 1; i++)
            {
                mul = mul * v[lineaDeJ[i]];
            }
            return mul;
        }

        public static int DivisionSintetica(int[] v, int[][] m, int fila, int[] lineaDeJ)
        {
            if (m[fila] [lineaDeJ[0]] == -1)
                return -1;
            var suma = m[fila][lineaDeJ[0]];
            for (var i = 1; i < lineaDeJ.GetUpperBound(0) + 1; i++)
            {
                if (m[fila][ lineaDeJ[i]] == -1)
                    return -1;
                suma = suma * v[lineaDeJ[i]] + m[fila][lineaDeJ[i]];
            }
            return suma;
        }

        public static int[] MultiplicacionSintetica(int[] v, int[] lineaDeJ, int pos, int t)
        {
            var valores = new int[t];
            for (var i = t - 1; i > 0; i--)
            {
                valores[i] = pos % v[lineaDeJ[i]];
                pos = pos / v[lineaDeJ[i]];
            }
            valores[0] = pos;
            return valores;
        }

        public static bool TestCA(int[][] m, int[] v, int n, int k, int t)
        {
            MatrizP matrizP = new MatrizP(t, k, v, n);
            var newJ = matrizP.J;
            indicesRepetidos.Clear();
            valoresRepetidos.Clear();
            indicesFaltantes.Clear();
            valoresFaltantes.Clear();
            int[] posicion = new int[t];
            int[] valor = new int[t];
            var esCA = true;
            var ListaDeP = new List<int[]>();
            foreach (var lineaDeJ in newJ)
            {
                var tamanoP = MayorDimensionP(v, lineaDeJ);
                var vectorP = new int[tamanoP];
                for (var fila = 0; fila < n; fila++)
                {
                    var posicionEnP = DivisionSintetica(v, m, fila, lineaDeJ);
                    if (posicionEnP != -1)
                        vectorP[posicionEnP] += 1;
                }
                ListaDeP.Add(vectorP);
                for (var pos = 0; pos < tamanoP; pos++)
                {
                    if (vectorP[pos] == 0)
                    {
                        esCA = false;
                        var x = MultiplicacionSintetica(v, lineaDeJ, pos, t);
                        posicion = lineaDeJ;
                        valor = x;
                        indicesFaltantes.Add(posicion);
                        valoresFaltantes.Add(valor);
                    }
                }
                for (var pos = 0; pos < tamanoP; pos++)
                {
                    if (vectorP[pos] > 1)
                    {
                        for (var it = 1; it < vectorP[pos]; it++)
                        {
                            var x = MultiplicacionSintetica(v, lineaDeJ, pos, t);
                            posicion = lineaDeJ;
                            valor = x;
                            indicesRepetidos.Add(posicion);
                            valoresRepetidos.Add(valor);
                        }
                    }
                }
            }
            return esCA;
        }
    }
}
