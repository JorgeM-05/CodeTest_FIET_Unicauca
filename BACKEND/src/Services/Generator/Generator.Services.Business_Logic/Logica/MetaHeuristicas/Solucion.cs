using System;

namespace Generator.Services.business_logic.Logica.MetaHeuristicas
{
    public class Solucion:IEquatable<Solucion>
    { 
        public CA MiCA;
        public int Fitness; // t-adas faltantes

        public Solucion(int n, int k, int[] v, int t) {
            MiCA = new CA(n, k, v, t);
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

        /// <summary>
        /// Genera el CA candidato en forma aleatoria, luego limpia P y calcula el fitness
        /// </summary>
        /// <param name="rand"></param>
        /// <param name="numeroCandidatos"></param>
        public void GenerarAletario(Random rand, int numeroCandidatos)
        {
            MiCA.GenerarAletario(rand, numeroCandidatos);
        }
      

        public void CalcularFitness(MatrizP miP)
        {
            var validador = new TestCA();
            validador.Validar(MiCA, miP);
            Fitness = validador.Faltantes;            
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
