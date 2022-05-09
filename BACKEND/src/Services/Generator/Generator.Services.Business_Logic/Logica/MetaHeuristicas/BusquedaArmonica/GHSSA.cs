using System;
using Generator.Services.business_logic.Logica.MetaHeuristicas.BaseSA;

namespace Generator.Services.business_logic.Logica.MetaHeuristicas.BusquedaArmonica
{
    public class GHSSA:Algoritmo
    {
        public int HMS;
        public float HMCR;
        public float ParMin;
        public float ParMax;
        public Armonia[] HM;

        public GHSSA(int ni, int maxiterSA, int n, int k, int[] v, int t, int semilla, 
            string nombre, int hms, float hmcr, float parmin, float parmax) : 
            base(ni, maxiterSA, n, k, v, t, semilla, nombre)
        {
            HMS = hms;
            HMCR = hmcr;
            ParMin = parmin;
            ParMax = parmax;
        }

        public override void Execute()
        {
            var rnd = new Random(Semilla);

            // 1. Inicializa HM
            HM = new Armonia[HMS];
            for (var i = 0; i < HMS; i++)
            {
                var nuevo = new Armonia(N, K, V, T);
                nuevo.GenerarAletario(rnd, 5);
                nuevo.CalcularFitness();
                var miSAInterno = new SAInterno(nuevo, nuevo.MiP, rnd, MaxIterSA, Nombre);
                miSAInterno.Optimizar();
                nuevo.MiCA.OrdenarFilas();
                HM[i] = nuevo;
            }

            var posPeor = BuscarPeor();
            var posBest = BuscarMejor();
            Best = HM[posBest];

            // 2. Proceso iterativo de improvisacion
            for (var i = 0; i < NI; i++)
            {
                if (Best.Fitness == 0)
                    break; // Termina porque se encontró un CA valido para los parametros establecidos                

                var par = Par(i);

                Armonia seleccionado;
                if (rnd.NextDouble() < HMCR)
                    seleccionado = rnd.NextDouble() < par ? new Armonia(HM[posBest]) : new Armonia(HM[rnd.Next(HMS)]);
                else
                    seleccionado = new Armonia(HM[rnd.Next(HMS)], rnd, (int) 0.10 * N);

                var miSA = new SA(seleccionado, seleccionado.MiP, rnd, MaxIterSA, Nombre);
                seleccionado.MiCA = miSA.MiSolucion.MiCA;
                seleccionado.Fitness = miSA.MiSolucion.Fitness;
                seleccionado.MiP = miSA.MiP;

                // Si la version optimizada es mejor que la peor, se remplaza la peor
                if (seleccionado.Fitness < HM[posPeor].Fitness && !Existe(seleccionado))
                {
                    seleccionado.MiCA.OrdenarFilas();
                    HM[posPeor] = seleccionado;
                    posPeor = BuscarPeor();
                    posBest = BuscarMejor();
                    Best = HM[posBest];
                }
                Console.WriteLine("iteracion = " + i + " Fitness = " + Best.Fitness);
            }
        }

        public bool Existe(Armonia actual)
        {
            for (var m=0; m<HMS; m++)
                if (actual.Equals(HM[m]))
                    return true;
            return false;
        }

        private double Par(int iteracion)
        {
            return ParMin + (((ParMax - ParMin) / NI) * iteracion);
        }

        private int BuscarMejor()
        {
            var posMejor = 0;
            var faltanMejor = HM[0].Fitness;

            for (var i = 1; i < HMS; i++)
            {
                if (HM[i].Fitness < faltanMejor)
                {
                    faltanMejor = HM[i].Fitness;
                    posMejor = i;
                }
            }
            return posMejor;
        }

        private int BuscarPeor()
        {
            var posPeor = 0;
            var faltanPeor = HM[0].Fitness;

            for (var i = 1; i < HMS; i++)
            {
                if (HM[i].Fitness > faltanPeor)
                {
                    faltanPeor = HM[i].Fitness;
                    posPeor = i;
                }
            }
            return posPeor;
        }
    }
}