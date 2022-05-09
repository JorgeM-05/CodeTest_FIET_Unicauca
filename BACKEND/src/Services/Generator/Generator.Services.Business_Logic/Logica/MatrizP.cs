using Generator.Services.business_logic.Logica;
using System;

namespace Generator.Services.business_logic.Logica
{
    public class MatrizP
    {
        public int T;
        public int[] V;  // dice el alfabeto de cada columna
        public int[] Valores;
        public String[][] MatrizValores;
        public int MaxJ;
        public int[][] J;
        public int[] Vt; // dice el tamaño de cada fila en la matriz P y J
        public int[][] P;
        public int N;

        public MatrizP(int t, int k, int[] v,int n)
        {
            N = n;
            T = t;
            V = new int[v.Length];
            Valores = new int[n];
            for (var fila = 0; fila < v.Length; fila++)
                V[fila] = v[fila];

            MaxJ = Utilidades.Combinatoria(k, t);    // combinaciones de t en k, t <= k
            ConstruirJ(k);
            Vt = new int[MaxJ];
            for (var fila = 0; fila < MaxJ; fila++)
                Vt[fila] = MayorDimensionP(J[fila]);
            P = new int[MaxJ][];
            for (var fila = 0; fila < MaxJ; fila++)
                P[fila] = new int[Vt[fila]];

            /////////////////////////////////////
       
            MatrizValores = new string[MaxJ][];
            for (var fila = 0; fila < MaxJ; fila++)
                MatrizValores[fila] = new String[Vt[fila]];
            for (var renglon = 0; renglon < MaxJ; renglon++)
            {
                for (var columna = 0; columna < Vt[renglon]; columna++)
                {
                    var respuesta = "";
                    var valores = Utilidades.MultiplicacionSintetica(columna, V, J[renglon], T);
                    for (var valor = 0; valor < T; valor++)
                    {
                        respuesta += valores[valor];
                    }
                    MatrizValores[renglon][columna] = respuesta;
                }
            }
        }
       
       
        /// <summary>
        /// Constructor de copia
        /// </summary>
        /// <param name="miPoriginal"></param>
        public MatrizP(MatrizP miPoriginal) 
        {
            Valores = new int[miPoriginal.N];
            for (var fila = 0; fila < miPoriginal.N; fila++)
                Valores[fila] = miPoriginal.Valores[fila];
            T = miPoriginal.T;
            V = new int[miPoriginal.V.Length];
            for (var fila = 0; fila < miPoriginal.V.Length; fila++)
                V[fila] = miPoriginal.V[fila];

            MaxJ = miPoriginal.MaxJ;

            J = new int[MaxJ][];
            for (var fila = 0; fila < MaxJ; fila++)
            {
                J[fila] = new int[T];
                for (var columna = 0; columna < T; columna++)
                    J[fila][columna] = miPoriginal.J[fila][columna];
            }

            Vt = new int[MaxJ];
            for (var fila = 0; fila < MaxJ; fila++)
                Vt[fila] = miPoriginal.Vt[fila];

            P = new int[MaxJ][];
            for (var fila = 0; fila < MaxJ; fila++)
            {
                P[fila] = new int[Vt[fila]];
                for (var columna = 0; columna < Vt[fila]; columna++)
                    P[fila][columna] = miPoriginal.P[fila][columna];
            }

            /////////// esta parte se agrego el dia 13/04 esto resulto al momento de crear el post
            MatrizValores = new String[MaxJ][];
            for (var fila = 0; fila < MaxJ; fila++)
            {
                MatrizValores[fila] = new String[Vt[fila]];
                for (var columna = 0; columna < Vt[fila]; columna++)
                    MatrizValores[fila][columna] = miPoriginal.MatrizValores[fila][columna];
            }
        }

        public void Limpiar()
        {
            for (var filaP = 0; filaP < MaxJ; filaP++)
                for (var columnaP = 0; columnaP < Vt[filaP]; columnaP++)
                    P[filaP][columnaP] = 0;
        }
        public void CalcularPeorFila(int filaCa, int fila, int columna)
        {
            int numCombinaciones = P[fila][columna];
            if (numCombinaciones > 1) {

                Valores[filaCa] += 1;
            }
        }
        public int PeorFila()
        {
            int valor = 0;
            int posicion = 0;
            for (int i = 0; i < Valores.Length; i++)
            {
                if (Valores[i] > valor)
                { 
                    valor = Valores[i];
                    posicion = i;
                }

            }
            return posicion;
        }
        public void IncrementarCelda(int fila, int columna)
        {
            P[fila][columna] += 1;
        }
        public void DecrementarCelda(int fila, int columna)
        {
            if (P[fila][columna] == 0)
                throw new Exception("ERROR no se puede decrementar esta posicion");

            P[fila][columna] -= 1;
        }

        /// <summary>
        ///  Construye la lista de valores J que se usan como indices de filas en la matriz P
        /// Se basa en el número de columnas del CA que se va a construir (k) y la fuerza (t)
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        private void ConstruirJ(int k)
        {
            int i;
            int iMax;
            var pos = 0;

            J = new int[MaxJ][];
            var lineaDeJ = new int[T];

            for (i = 0; i < T; i++)
                lineaDeJ[i] = i;

            J[pos] = Utilidades.CrearPMQbasadoEnVEctor(lineaDeJ, T);
            pos++;

            for (iMax = T - 1, i = 0; i < T; i++)
            {
                if (lineaDeJ[i] == k - T + i)
                {
                    iMax = i;
                    break;
                }
            }
            do
            {
                lineaDeJ[T - 1]++;
                if (lineaDeJ[T - 1] == k)
                {
                    if (iMax == 0) break;

                    lineaDeJ[iMax - 1]++;

                    for (i = iMax; i < T; i++)
                        lineaDeJ[i] = lineaDeJ[i - 1] + 1;

                    if (lineaDeJ[iMax - 1] == k - T + iMax - 1)
                        iMax = iMax - 1;
                    else
                        iMax = T - 1;
                }
                J[pos] = Utilidades.CrearPMQbasadoEnVEctor(lineaDeJ, T);
                pos++;
            } while (true);
        }

        /// <summary>
        /// Cuenta el número de ceros (t-adas faltantes) en la matriz P
        /// </summary>
        /// <returns></returns>
        public int ContarCerosEnP()
        {
            var total = 0;
            for (var filaP = 0; filaP < MaxJ; filaP++)
                for (var columnaP = 0; columnaP < Vt[filaP]; columnaP++)
                    if (P[filaP][columnaP] == 0) 
                    {
                        
                        total++;
                    }
            return total;
        }

        /// <summary>
        /// Genera una lista de las posiciones (fila, columna) de los ceros que existen
        /// en la Matriz P
        /// </summary>
        /// <returns></returns>
        public int[][] FaltanEnP()
        {
            var totalCeros = ContarCerosEnP();
            var lista = new int[totalCeros][];
            var pos = 0;
            for (var filaP = 0; filaP < MaxJ; filaP++)
                for (var columnaP = 0; columnaP < Vt[filaP]; columnaP++)
                    if (P[filaP][columnaP] == 0)
                    {
                        var posEnP = new int[2];
                        posEnP[0] = filaP;
                        posEnP[1] = columnaP;
                        lista[pos++] = posEnP;
                    }

            return lista;
        }

        public int[] SeleccionarCeroEnP(int[][] posicionesCeroEnP, Random rand)
        {
            var ceroSeleccionado = rand.Next(posicionesCeroEnP[0].Length);
            return posicionesCeroEnP[ceroSeleccionado];
        }
        

        public override string ToString()
        {
            var result = "Matriz P = \n";

            for (var renglon = 0; renglon < MaxJ; renglon++)
            {
                for (var columna = 0; columna < T; columna++)
                    result += J[renglon][columna] + " ";
                result += " -> (" + renglon + ") :";

                for (var columna = 0; columna < Vt[renglon]; columna++)
                {
                    var respuesta = "";
                    var valores = Utilidades.MultiplicacionSintetica(columna, V, J[renglon], T);
                    result += "[";
                    for (var valor = 0; valor < T; valor++)
                    {
                        result += valores[valor];
                        respuesta += valores[valor];
                    }

                    result += "]:" + string.Format("{0,2} ", P[renglon][columna]);
                    //MatrizValores[renglon][columna] = respuesta;
                }
                result += "\n";
            }
            return result;
        }

        int MayorDimensionP(int[] lineaDeJ)
        {
            var mul = 1;
            for (var i = 0; i < T; i++)
                mul = mul * V[lineaDeJ[i]];
            return mul;
        }
    }
}