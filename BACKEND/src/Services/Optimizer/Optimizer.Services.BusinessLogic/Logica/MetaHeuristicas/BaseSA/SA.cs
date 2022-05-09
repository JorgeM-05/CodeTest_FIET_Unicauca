using System;

namespace Optimizer.Services.business_logic.Logica.MetaHeuristicas.BaseSA
{
    class SA
    {
        public Solucion MiSolucion;
        public MatrizP MiP;

        public SA(Solucion miSolucion, MatrizP miP, Random rnd, int maxIterOptimizacion, string nombre)
        {
            MiSolucion = miSolucion;
            MiP = miP;

            // Se optimiza con recocido simulado
            var copiaSolucion = new Solucion(MiSolucion);
            var miP2 = new MatrizP(MiP);

            var miSA = new SAInterno(copiaSolucion, miP2, rnd, maxIterOptimizacion, nombre);
            miSA.Optimizar();

            // Si la version optimizada es mejor, se remplaza la original
            if (copiaSolucion.Fitness < MiSolucion.Fitness)
            {
                copiaSolucion.MiCA.OrdenarFilas();
                MiSolucion = copiaSolucion;
                MiP = miP2;
            }
        }
    }
}
