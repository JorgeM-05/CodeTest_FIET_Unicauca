using System;
using Generator.Services.business_logic.Logica.MetaHeuristicas.BaseSA;

namespace Generator.Services.business_logic.Logica.MetaHeuristicas.IteradosSA
{
    public class ISA:Algoritmo
    {
        public MatrizP MiP; 

        public ISA(int ni, int maxiterSA, int n, int k, int[] v, int t, 
            int semilla, string nombre):
            base(ni, maxiterSA, n, k, v, t, semilla, nombre)
        {
            MiP = new MatrizP(T, K, V, N);
        }

        public override void Execute()
        {
            var rnd = new Random(Semilla);

            // 1. Inicializar el CA
            Best = new Solucion(N, K, V, T); // Matriz donde esta el CA
            Best.GenerarAletario(rnd, 5); // Se genera el MCA inicializado aleatoriamente 
            MiP.Limpiar();
            Best.CalcularFitness(MiP); // se calcula el primer fitness

            // 2. Llamadas iterativas de SA para optimizar la solucion
            for (var i = 0; i < NI; i++)
            {
                if (Best.Fitness == 0)
                    break; // Termina porque se encontró un CA valido para los parametros establecidos                

                // Se optimiza con recocido simulado
                var copiaArmonia = new Solucion(Best);
                var miP2 = new MatrizP(MiP);
                var miSA = new SAInterno(copiaArmonia, miP2, rnd, MaxIterSA, Nombre);
                miSA.Optimizar();

                // Si la version optimizada es mejor, se remplaza la original
                if (copiaArmonia.Fitness < Best.Fitness)
                {
                    copiaArmonia.MiCA.OrdenarFilas();
                    Best = copiaArmonia; 
                    MiP = miP2; 
                }
            }
        }
    }
}