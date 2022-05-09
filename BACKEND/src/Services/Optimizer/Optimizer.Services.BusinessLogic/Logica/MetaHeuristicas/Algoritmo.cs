namespace Optimizer.Services.business_logic.Logica.MetaHeuristicas
{
    public abstract class Algoritmo
    {
        public readonly int NI;
        public readonly int MaxIterSA;
        public readonly int N;
        public readonly int K;
        public readonly int[] V;
        public readonly int T;
        public readonly string Nombre;
        public readonly int[][] Mca;
        public int Semilla;

        public Solucion Best;

        protected Algoritmo(int ni, int maxiterSA, int n, int k, int[] v, int t, int semilla, string nombre,int[][] MCA)
        {
            NI = ni;
            MaxIterSA = maxiterSA;
            N = n;
            K = k;
            V = v;
            T = t;
            Semilla = semilla;
            Nombre = nombre;
            Mca = MCA;
        }

        public abstract void Execute();
    }
}