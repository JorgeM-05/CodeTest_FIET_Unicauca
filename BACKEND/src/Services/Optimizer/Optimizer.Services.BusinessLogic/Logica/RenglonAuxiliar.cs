using System;

namespace Optimizer.Services.business_logic.Logica
{
    public class RenglonAuxiliar : IComparable<RenglonAuxiliar>
    {
        public int K;
        public int[] Renglon;
        public int Similitud;
        public int SimilitudMaxima;

        public RenglonAuxiliar(int k)
        {
            K = k;
            Renglon = new int[K];
        }

        public RenglonAuxiliar(int k, int[] v, Random rand)
        {
            K = k;
            Renglon = new int[K];
            for (var columna = 0; columna < K; columna++)
                Renglon[columna] = rand.Next(v[columna]);
        }

        public void Fijar(int[] renglon)
        {
            for (var columna = 0; columna < K; columna++)
                Renglon[columna] = renglon[columna];
        }

        public void CalcularSimilitudes (int[][] renglonesDelCA, int tope)
        {
            Similitud = 0;
            for (var fila = 0; fila < tope; fila++)
            {
                for (var columna = 0; columna < K; columna++)
                    if (Renglon[columna] == renglonesDelCA[fila][columna])
                       Similitud++;
            }
            Similitud = Similitud / tope;
        }

        public int CompareTo(RenglonAuxiliar other)
        {
            if (Similitud != other.Similitud)
                return Similitud.CompareTo(other.Similitud);
            return SimilitudMaxima.CompareTo(other.SimilitudMaxima);
        }
    }
}