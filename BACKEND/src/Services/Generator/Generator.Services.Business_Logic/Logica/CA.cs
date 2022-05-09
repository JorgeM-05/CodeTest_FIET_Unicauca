using System;
using System.Collections.Generic;

namespace Generator.Services.business_logic.Logica
{
    public class CA : IEquatable<CA>
    {
        public int N { get; set; }
        public int K { get; set; }
        public int[] V { get; set; }
        public int T { get; set; }
        

        public int[][] Matriz;

        public CA(int n, int k, int[] v, int t)
        {
            N = n;
            K = k;
            V = v;
            T = t;
            Matriz = new int[n][];
            for (var fila = 0; fila < n; fila++)
                Matriz[fila] = new int[k];
        }

        public CA(CA original)
        {
            N = original.N;
            K = original.K;
            V = original.V;
            T = original.T;
            Matriz = new int[N][];
            for (var fila = 0; fila < N; fila++)
            {
                Matriz[fila] = new int[K];
                for (var columna = 0; columna < K; columna++)
                    Matriz[fila][columna] = original.Matriz[fila][columna];
            }
        }

        public void GenerarAletario(Random rand, int numeroCandidatos)
        {
            for (var fila = 0; fila < N; fila++)
            {
                var renglonElegido = GenerarRenglonAleatorio(rand, numeroCandidatos, fila);
                AsignarRenglon(renglonElegido, fila);
                for (var columna = 0; columna < K; columna++)
                    Matriz[fila][columna] = renglonElegido[columna];
            }
        }

        public void AsignarRenglon(int[] renglon, int fila)
        {
            for (var columna = 0; columna < K; columna++)
                Matriz[fila][columna] = renglon[columna];
        }

        public int[] GenerarRenglonAleatorio(Random rand, int numeroCandidatos, int renglonesActuales)
        {
            var renglonElegido = new int[K];
            if (renglonesActuales == 0)
            {
                for (var columna = 0; columna < K; columna++)
                    renglonElegido[columna] = rand.Next(V[columna]);
            }
            else
            {
                var listaCandidatos = new List<RenglonAuxiliar>();
                for (var c = 0; c < numeroCandidatos; c++)
                {
                    var nuevo = new RenglonAuxiliar(K, V, rand);
                    nuevo.CalcularSimilitudes(Matriz, renglonesActuales);
                    listaCandidatos.Add(nuevo);
                }
                listaCandidatos.Sort();
                renglonElegido = listaCandidatos[0].Renglon;
            }
            return renglonElegido;
        }

        public int[] SeleccionarRenglonAleatorio(Random rand, int numeroCandidatos, int renglonesActuales, CA caFuente)
        {
            var renglonElegido = new int[K];
            if (renglonesActuales == 0)
            {
                var pos = rand.Next(N);
                for (var columna = 0; columna < K; columna++)
                    renglonElegido[columna] = caFuente.Matriz[pos][columna];
            }
            else
            {
                var listaCandidatos = new List<RenglonAuxiliar>();
                while (true)
                {
                    for (var c = 0; c < numeroCandidatos; c++)
                    {
                        var nuevo = new RenglonAuxiliar(K);
                        var pos = rand.Next(N);
                        nuevo.Fijar(caFuente.Matriz[pos]);
                        nuevo.CalcularSimilitudes(Matriz, renglonesActuales);
                        listaCandidatos.Add(nuevo);
                    }
                    listaCandidatos.Sort();
                    if (!ExisteLinea(listaCandidatos[0], renglonesActuales)) break;
                    listaCandidatos.Clear();
                }
                renglonElegido = listaCandidatos[0].Renglon;
            }
            return renglonElegido;
        }

        public bool ExisteLinea(RenglonAuxiliar renglon, int totalRenglones)
        {
            for (var fila = 0; fila < totalRenglones; fila++)
            {
                var iguales = true;
                for (var columna = 0; columna < K; columna++)
                    if (Matriz[fila][columna] != renglon.Renglon[columna])
                    {
                        iguales = false;
                        break;
                    }
                if (iguales) return true;
            }
            return false;
        } 

        public bool EsMenor(int[] fila1, int[] fila2)
        {
            for (var columna = 0; columna < K; columna++)
                if (fila1[columna] < fila2[columna])
                    return true;
                else if (fila1[columna] > fila2[columna])
                    return false;
            return false;
        }

        public void OrdenarFilas()
        {
            var temporal = new int[K];
            for (var i = 0; i < N - 1; i++)
                for (var j = i + 1; j < N; j++)
                    if (EsMenor(Matriz[i], Matriz[j]))
                        for (var columna = 0; columna < K; columna++)
                        {
                            temporal[columna] = Matriz[i][columna];
                            Matriz[i][columna] = Matriz[j][columna];
                            Matriz[j][columna] = temporal[columna];
                        }
        }

        public bool Equals(CA other)
        {
            for (var fila = 0; fila < N; fila++)
                for (var columna = 0; columna < K; columna++)
                    if (Matriz[fila][columna] != other.Matriz[fila][columna])
                        return false;
            return true;
        }

        public override string ToString()
        {
            var result = "";
            for (var renglon = 0; renglon < N; renglon++)
            {
                for (var columna = 0; columna < K; columna++)
                    result += Matriz[renglon][columna] + " ";
                result += "\n";
            }
            return result;
        }
    }
}