using System;
using Optimizer.Services.business_logic.Logica.MetaHeuristicas.BaseSA;

namespace Optimizer.Services.business_logic.Logica.MetaHeuristicas.IteradosSA
{
    public class ISA:Algoritmo
    {
        public int Ni;
        public MatrizP MiP;
        
        public ISA(int ni, int maxiterSA, int n, int k, int[] v, int t, 
            int semilla, string nombre, int[][] MCA):
            base(ni, maxiterSA, n, k, v, t, semilla, nombre, MCA)
        {
            Ni = ni;
            MiP = new MatrizP(T, K, V, N);

        }

        public override void Execute()
        {
            // 1. Inicializar el CA
            Best = new Solucion(N, K, V, T,Mca); // Matriz donde esta el CA
            //Best.GenerarAletario(rnd, 5);
            MiP.Limpiar();
            Best.CalcularFitness(MiP, Ni);
            if (Ni == 2 && Best.Fitness > 0)
            {
                Best.ReemplazarComodines(MiP);
                Best.CalcularFitness(MiP, Ni);
                Best.Optimizar(MiP);
                Best.CalcularFitness(MiP, Ni);
            }
            else if (Ni == 2)
            {
                Best.ReemplazarComodinesCero(MiP);
            }
            
        }
    }
}