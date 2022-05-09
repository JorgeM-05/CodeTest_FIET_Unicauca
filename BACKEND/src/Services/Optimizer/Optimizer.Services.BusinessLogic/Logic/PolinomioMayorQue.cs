using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimizer.Services.BusinessLogic.Logic
{
    public class PolinomioMayorQue : IComparable<PolinomioMayorQue>
    {
        public int Indice;
        public int[] PosicionDeColumnas;

        public PolinomioMayorQue(int t, int[] vector)
        {
            PosicionDeColumnas = new int[t];
            vector.CopyTo(PosicionDeColumnas, 0);
            CalcularIndice(t);
        }

        private void CalcularIndice(int t)
        {
            var suma = 0;
            for (var c = 1; c <= t; c++)
            {
                suma += SeekerWildcard.Combinatoria(PosicionDeColumnas[c - 1], c);
            }
            Indice = suma;
        }

        public int CompareTo(PolinomioMayorQue other)
        {
            return Indice.CompareTo(other.Indice);
        }

        public override string ToString()
        {
            var salida = Indice + " - ";
            foreach (var posicion in PosicionDeColumnas)
            {
                salida += posicion + " ";
            }
            return salida;
        }
    }
}
