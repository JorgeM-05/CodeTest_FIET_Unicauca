using System;

namespace Optimizer.Services.business_logic.Logica.MetaHeuristicas
{
    public class Solucion:IEquatable<Solucion>
    {
        public CA MiCA;
        public int Fitness; // t-adas faltantes
        public int[][] repeticiones;

        public Solucion(int n, int k, int[] v, int t,int[][] MCa) {
            MiCA = new CA(n, k, v, t,MCa);
        }

        /// <summary>
        /// Constructor de copia
        /// </summary>
        /// <param name="original"></param>
        public Solucion(Solucion original)
        {
            MiCA = new CA(original.MiCA);
            Fitness = original.Fitness;
        }

        public void ReemplazarComodines(MatrizP miP)
        {
            var mejoresvalores = PMisLocal3OptimizarFila(miP);
            for (int i = 0; i < MiCA.N; i++) {
                for (var j = 0; j < MiCA.K; j++) {
                    if (MiCA.Matriz[i][j] == -1) {
                        MiCA.Matriz[i][j] = mejoresvalores[j];
                    }
                }   
              
            }
        }
        public void ReemplazarComodinesCero(MatrizP miP)
        {
            var seed = Environment.TickCount;
            var random = new Random(seed);
           
            for (int i = 0; i < MiCA.N; i++)
            {
                for (var j = 0; j < MiCA.K; j++)
                {
                    int value = random.Next(0, MiCA.V[j] - 1);

                    if (MiCA.Matriz[i][j] == -1)
                    {

                        MiCA.Matriz[i][j] = value;
                    }
                }

            }
        }

        public void Optimizar(MatrizP miP)
        {
            var mejoresvalores = PMisLocal3OptimizarFila(miP);
            var peorRenglon = miP.PeorFila();
            
                for (var j = 0; j < MiCA.K; j++)
                {
                   MiCA.Matriz[peorRenglon][j] = mejoresvalores[j];
                }
        }

        /// <summary>
        /// Genera el CA candidato en forma aleatoria, luego limpia P y calcula el fitness
        /// </summary>
        /// <param name="rand"></param>
        /// <param name="numeroCandidatos"></param>
        public void GenerarAletario(Random rand, int numeroCandidatos)
        {
            MiCA.GenerarAletario(rand, numeroCandidatos);
        }
        
        public void CalcularFitness(MatrizP miP, int Ni)
        {
            var validador = new TestCA();
            validador.Validar(MiCA, miP, Ni);
            Fitness = validador.Faltantes;
            repeticiones = validador.repeticiones;
        }
        public int[] PMisLocal3OptimizarFila(MatrizP MiP)
        {
            int columnas = MiCA.K;
            int NumCombinaciones =MiCA.T;
            var nuevoRenglon = new int[columnas];
            int[] cont = new int[columnas];
            var lista = MiP.FaltanEnP();
            int[][] U = new int[columnas][];
            for (int m = 0; m < columnas; m++)
            {
                U[m] = new int[lista.Length];
            }
            for (int i = 0; i < lista.GetLength(0); i++)
            {
                for (int j = 0; j < NumCombinaciones; j++)
                {
                    var columnaMCA = MiP.J[lista[i][0]][j];
                    int[] coordenadasenP = lista[i];
                    int[] valoresFaltantes = valoresfaltantes(coordenadasenP, MiP);
                    U[columnaMCA][cont[columnaMCA]] = valoresFaltantes[j];
                    cont[columnaMCA] += 1;
                }
            }
            for (int k = 0; k < U.Length; k++)
            {
                nuevoRenglon[k] = calularModa(U[k], cont[k]);
            }
            return nuevoRenglon;
        }
        public int[] valoresfaltantes(int[] faltanenP, MatrizP MiP)
        {
            String value = MiP.MatrizValores[faltanenP[0]][faltanenP[1]];
            int[] valores = new int[MiCA.T];
            for (var i = 0; i < value.Length; i++)
            {
                valores[i] = Int32.Parse(value.Substring(i, 1));
            }
            return valores;
        }
        public int calularModa(int[] vector, int numRep)
        {

            int maxNumero = vector[0];
            int maxVeces = 0;
            int i = 0;
            while (i < numRep)
            {
                int numVeces = 0;
                int j = 0;
                while (j < numRep)
                {
                    if (vector[j] == vector[i]) numVeces++;
                    j++;
                }

                if (numVeces >= maxVeces)
                {
                    maxNumero = vector[i];
                    maxVeces = numVeces;
                }
                i++;
            }
            return maxNumero;
        }



        public bool Equals(Solucion other)
        {
            if (Fitness != other.Fitness)
                return false;
            for (var f=0; f<MiCA.N; f++)
                for (var c=0; c< MiCA.K; c++)
                    if (MiCA.Matriz[f][c] != other.MiCA.Matriz[f][c])
                        return false;
            return true;
        }
    }
}
