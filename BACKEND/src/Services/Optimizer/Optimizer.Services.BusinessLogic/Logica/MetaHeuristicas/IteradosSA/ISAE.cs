//using System;
//using System.IO;
//using Optimizer.Services.business_logic.Logica.MetaHeuristicas.BaseSA;

//namespace Optimizer.Services.business_logic.Logica.MetaHeuristicas.IteradosSA
//{
//    public class ISAE:Algoritmo
//    {
//        public MatrizP MiP;

//        public ISAE(int ni, int maxiterSA, int n, int k, int[] v, int t, 
//            int semilla, string nombre) :
//            base(ni, maxiterSA, n, k, v, t, semilla, nombre)
//        {
//            MiP = new MatrizP(T, K, V);
//        }

//        public override void Execute()
//        {
//            var rnd = new Random(Semilla);
//            // 1. Inicializar el CA
//            Best = new Solucion(N, K, V, T);
//            Best.GenerarAletario(rnd, 5);
//            MiP.Limpiar();
//            Best.CalcularFitness(MiP);

//            // Se optimiza con recocido simulado
//            var copiaArmonia = new Solucion(Best);
//            var miP2 = new MatrizP(MiP);

//            // 2. Llamadas iterativas de SA para optimizar la solucion
//            for (var i = 0; i < NI; i++)
//            {
//                if (Best.Fitness == 0)
//                    break; // Termina porque se encontró un CA valido para los parametros establecidos                

//                var miSA = new SAInterno(copiaArmonia, miP2, rnd, MaxIterSA, Nombre);
//                miSA.Optimizar();
//                if (File.Exists(Nombre)) break;

//                // Si la version optimizada es mejor, se remplaza la original
//                if (copiaArmonia.Fitness < Best.Fitness)
//                {
//                    copiaArmonia.MiCA.OrdenarFilas();
//                    Best = new Solucion(copiaArmonia); //copia de variablles
//                    MiP = new MatrizP(miP2); // copia de variables
//                }
//                Console.WriteLine("iteracion = " + i + " Fitness = " + Best.Fitness);
//            }
//        }
//    }
//}