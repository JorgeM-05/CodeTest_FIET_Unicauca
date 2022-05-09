using System;

namespace Generator.Services.business_logic.Logica.MetaHeuristicas.BusquedaArmonica
{
    public class Armonia:Solucion
    {
        public MatrizP MiP;

        public Armonia(int n, int k, int[] v, int t) : base (n, k, v, t)
        {
            MiP = new MatrizP(t, k, v, n);
        }

        /// <summary>
        /// Constructor de copia
        /// </summary>
        /// <param name="original"></param>
        public Armonia(Armonia original) : base(original){
            MiP = new MatrizP(original.MiP);
        }

        /// <summary>
        /// Constructor de copia con operador de macro-mutacion
        /// </summary>
        /// <param name="original"></param>
        /// <param name="rnd"></param>
        /// <param name="filasMutar"></param>
        public Armonia(Armonia original, Random rnd, int filasMutar) : base(original)
        {
            for (var i = 0; i < filasMutar; i++)
            {
                var posfila = rnd.Next(MiCA.N);
                var poscolumna = rnd.Next(MiCA.K);
                int valor;
                do{
                    valor = rnd.Next(MiCA.V[poscolumna]);
                } while (MiCA.Matriz[posfila][poscolumna] == valor);
                MiCA.Matriz[posfila][poscolumna] = valor;
            }

            MiP = new MatrizP(MiCA.T, MiCA.K, MiCA.V, MiCA.N);
            CalcularFitness();
        }

        public void CalcularFitness()
        {
            var validador = new TestCA();
            validador.Validar(MiCA, MiP);
            Fitness = validador.Faltantes;            
        }

        public override string ToString()
        {
            return Fitness + " CA = " +  MiCA;
        }
    }
}