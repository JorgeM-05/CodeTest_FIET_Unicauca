using System;
using System.Collections.Generic;

namespace Optimizer.Services.business_logic.Logica.MetaHeuristicas.BaseSA
{
    public class SAInterno
    {
        public Solucion MiSolucion;
        public MatrizP MiP;
        public Random Rnd;
        public int MaxIterOptimizacion;
        public TestCA Validador;
        public readonly string Nombre;

        public SAInterno(Solucion miSolucion, MatrizP miP, Random rnd, int maxIterOptimizacion, string nombre)
        {
            MiSolucion = miSolucion;
            MiP = miP;
            Rnd = rnd;
            MaxIterOptimizacion = maxIterOptimizacion;
            Nombre = nombre;
            Validador = new TestCA();
        }

        public void Optimizar()
        {
            var best = new Solucion(MiSolucion);

            for (var iter = 0; iter < MaxIterOptimizacion; iter++)
            {
                if (MiSolucion.Fitness == 0)
                    break;

                var temperatura = (1.0 * (MaxIterOptimizacion - iter)) / MaxIterOptimizacion;

                // Selecciona una fila del CA para ser optimizada
                var renglonOriginal = new int[MiSolucion.MiCA.K];

                int[] renglonOptimizado;
                int renglonDelCA;

                if (Rnd.NextDouble() < 0.7)
                {
                    renglonOptimizado = PMisLocal1RepetidoEnP(MiSolucion, MiSolucion.Fitness, out renglonDelCA);
                    renglonOriginal = Utilidades.CopiaDe(MiSolucion.MiCA.Matriz[renglonDelCA]); ;
                }
                else
                { 
                    renglonDelCA = Rnd.Next(MiSolucion.MiCA.N);
                    renglonOriginal = Utilidades.CopiaDe(MiSolucion.MiCA.Matriz[renglonDelCA]);
                    renglonOptimizado = PMisLocal2RepetidoEnTodasColumnas(renglonOriginal, MiSolucion.Fitness);
                }

                // Se calcula el nuevo fitness usando un TestCA incremental, solo con renglon modificado (anterior y nueva version)
                var nuevoFitness = Validador.QuitarPonerRenglon(renglonOriginal, renglonOptimizado, MiP, MiSolucion.MiCA.V, MiSolucion.MiCA.T, false);

                var min = Minimo(nuevoFitness, MiSolucion.Fitness, temperatura);
                var ale = Rnd.NextDouble();

                if (nuevoFitness < best.Fitness)
                {
                    for (var columna = 0; columna < MiSolucion.MiCA.K; columna++)
                        best.MiCA.Matriz[renglonDelCA][columna] = renglonOptimizado[columna];
                    best.Fitness = nuevoFitness;
                }

                if ( ale < min || nuevoFitness < MiSolucion.Fitness)
                {
                    for (var columna = 0; columna < MiSolucion.MiCA.K; columna++)
                        MiSolucion.MiCA.Matriz[renglonDelCA][columna] = renglonOptimizado[columna];
                    MiSolucion.Fitness = Validador.QuitarPonerRenglon(renglonOriginal, renglonOptimizado, MiP, MiSolucion.MiCA.V, MiSolucion.MiCA.T, true);
                }
            }
            MiSolucion = new Solucion(best);
            MiP = new MatrizP(MiSolucion.MiCA.T, MiSolucion.MiCA.K, MiSolucion.MiCA.V, MiSolucion.MiCA.N);
            MiSolucion.CalcularFitness(MiP, 1);
        }

        /// <summary>
        /// A un valor del renglon le cambio por otro ... Mutación al azar
        /// </summary>
        /// <param name="renglon"></param>
        /// <returns></returns>
        public void Shake(int[] renglon)
        {
            var col = Rnd.Next(MiSolucion.MiCA.K); // escoge una columna en forma aleatoria
            renglon[col] = MiSolucion.MiCA.V[col] - renglon[col] - 1;
        }

        /// <summary>
        /// Prueba tratar de borrar un cero de la matriz P, calcula los faltantes con este cambio
        /// Repite lo anterior un maximo de 10 veces (o el numero ceros que hayan)
        /// Al final, escoge el cambio con el que mejor le fue
        /// </summary>
        /// <param name="renglonOriginal"></param>
        /// <returns></returns>
        public int[] PMisLocal1RepetidoEnP(Solucion miCA, int menoresfaltan, out int renglonElegido)
        {
            renglonElegido = Rnd.Next(miCA.MiCA.N);
            var totalCeros = MiP.ContarCerosEnP();
            if (totalCeros == 0) return new int[MiSolucion.MiCA.K]; // nunca debe ocurrir

            var lista = MiP.FaltanEnP(); // filas y columnas en P que tienen ceros
            var posCero = Rnd.Next(totalCeros);
            var mejorRenglon = new int[miCA.MiCA.K];
            var cambio = false;
            var posiciones = new List<int>(); for (var iter = 0; iter < miCA.MiCA.N; iter++) posiciones.Add(iter);

            while (posiciones.Count > 0)
            {
                var seleccionado = Rnd.Next(posiciones.Count);
                var iter = posiciones[seleccionado];
                posiciones.RemoveAt(seleccionado);
                var renglonOriginal = Utilidades.CopiaDe(miCA.MiCA.Matriz[iter]);
                var nuevoRenglon = Utilidades.CopiaDe(miCA.MiCA.Matriz[iter]);

                var filaP = lista[posCero][0];
                var columnaP = lista[posCero][1];

                // filaP permite sacar de J[filaP] la combinación de columnas implicadas en la celda seleccionada
                // La multiplicacion sintetica de la columnaP da los valores que faltan en la celda seleccionada
                var valoresP = Utilidades.MultiplicacionSintetica(columnaP, MiSolucion.MiCA.V, MiP.J[filaP], MiSolucion.MiCA.T);
                for (var columna = 0; columna < MiSolucion.MiCA.T; columna++)
                    nuevoRenglon[MiP.J[filaP][columna]] = valoresP[columna];

                var faltan = Validador.QuitarPonerRenglon(renglonOriginal, nuevoRenglon, MiP, MiSolucion.MiCA.V, MiSolucion.MiCA.T, false);
                if (faltan < menoresfaltan)
                {
                    renglonElegido = iter;
                    menoresfaltan = faltan;
                    mejorRenglon = Utilidades.CopiaDe(nuevoRenglon);
                    cambio = true; 
                }
            }
            if (cambio == false)
            {
                mejorRenglon = Utilidades.CopiaDe(miCA.MiCA.Matriz[renglonElegido]);
                Shake(mejorRenglon);
            }
            return mejorRenglon;
        }

        /// <summary>
        /// A un valor del renglon le cambio por todos los otros valores posibles y escoge el mejor cambio,
        /// es decir, el que menos fitness tenga
        /// </summary>
        /// <param name="renglonOriginal"></param>
        /// <returns></returns>
        public int[] PMisLocal2RepetidoEnTodasColumnas(int[] renglonOriginal, int menoresfaltan)
        {
            var nuevoRenglon = Utilidades.CopiaDe(renglonOriginal);
            var cambio = false;
            for (var col = 0; col < MiSolucion.MiCA.K; col++)
            {
                var mejorValor = nuevoRenglon[col];
                for (var val = 0; val < MiSolucion.MiCA.V[col]; val++)
                {
                    nuevoRenglon[col] = val;
                    if (renglonOriginal[col] == nuevoRenglon[col]) continue; //No prueba con el valor original del renglon
                    var faltan = Validador.QuitarPonerRenglon(renglonOriginal, nuevoRenglon, MiP, MiSolucion.MiCA.V, MiSolucion.MiCA.T, false);
                    if (faltan < menoresfaltan)
                    {
                        mejorValor = val;
                        menoresfaltan = faltan;
                        cambio = true;
                    }
                }
                nuevoRenglon[col] = mejorValor;
            }
            if (cambio == false) Shake(nuevoRenglon);
            return nuevoRenglon;
        }

        /// <summary>
        /// A un valor del renglon le cambio por todos los otros valores posibles y escoge el mejor cambio,
        /// es decir, el que menos fitness tenga
        /// </summary>
        /// <param name="renglonOriginal"></param>
        /// <returns></returns>
        public int[] PMisLocal2RepetidoEnColumna(int[] renglonOriginal)
        {
            var nuevoRenglon = Utilidades.CopiaDe(renglonOriginal);
            var col = Rnd.Next(MiSolucion.MiCA.K); // escoge una columna en forma aleatoria
            var actual = nuevoRenglon[col];
            var mejor = 0;
            var menoresfaltan = int.MaxValue;
            for (var val = 0; val < MiSolucion.MiCA.V[col]; val++)
            {
                if (val == actual) continue;//No prueba con el valor original del renglon
                nuevoRenglon[col] = val;
                var faltan = Validador.QuitarPonerRenglon(renglonOriginal, nuevoRenglon, MiP, MiSolucion.MiCA.V, MiSolucion.MiCA.T, false);
                if (faltan < menoresfaltan)
                {
                    mejor = val;
                    menoresfaltan = faltan;
                }
            }
            nuevoRenglon[col] = mejor;
            return nuevoRenglon;
        }

        #region A1
        /// <summary>
        /// Basado en un renglon actual de CA genera un nuevo renglon que trata de quitar un cero
        /// especifico de la martiz P
        /// </summary>
        /// <param name="renglonOriginal"></param>
        /// <returns></returns>
        public int[] PMisLocal1Simple(int[] renglonOriginal)
        {
            // Nuevo renglon basado en el original con el cambio del cero en P seleccionado
            var nuevo = new int[MiSolucion.MiCA.K];
            for (var columna = 0; columna < MiSolucion.MiCA.K; columna++)
                nuevo[columna] = renglonOriginal[columna];

            var totalCeros = MiP.ContarCerosEnP();
            if (totalCeros == 0) return nuevo;

            var lista = MiP.FaltanEnP();
            var cambios = Rnd.Next(lista.Length); // Selecciona un cero de P al azar
            var filaP = lista[cambios][0];
            var columnaP = lista[cambios][1];

            // filaP permite sacar de J[filaP] la combinación de columnas implicadas en la celda seleccionada
            // La multiplicacion sintetica de la columnaP da los valores que faltan en la celda seleccionada
            var valoresEnP = Utilidades.MultiplicacionSintetica(columnaP, MiSolucion.MiCA.V, MiP.J[filaP], MiSolucion.MiCA.T);
            for (var columna = 0; columna < MiSolucion.MiCA.T; columna++)
                nuevo[MiP.J[filaP][columna]] = valoresEnP[columna];
            return nuevo;
        }

        public int[] PMisLocal2Simple(int[] renglonOriginal)
        {
            // Nuevo renglon basado en el original con el cambio del cero en P seleccionado
            var nuevo = new int[MiSolucion.MiCA.K];
            for (var columna = 0; columna < MiSolucion.MiCA.K; columna++)
                nuevo[columna] = renglonOriginal[columna];

            var col = Rnd.Next(MiSolucion.MiCA.K);
            var actual = nuevo[col];
            do
            {
                nuevo[col] = Rnd.Next(MiSolucion.MiCA.V[col]);
            } while (nuevo[col] == actual);
            return nuevo;
        }

        public void PMisLocal1()
        {
            var renglonDelCA = Rnd.Next(MiSolucion.MiCA.N);
            var copiaRenglon = new int[MiSolucion.MiCA.K];
            var totalCeros = MiP.ContarCerosEnP();
            if (totalCeros == 0) return;

            var lista = MiP.FaltanEnP();
            var cambios = 0;
            while (cambios < totalCeros)
            {
                //buscar un cero en P aleatoriamente
                //SeleccionarCeroEnP (P, MaxJ, vt, totalCeros, &filaP, &columnaP);
                var filaP = lista[cambios][0];
                var columnaP = lista[cambios][1];
                cambios++;

                // filaP permite sacar de J[filaP] la combinación de columas implicadas en la celda seleccionada
                // La multiplicacion sintetica de la columnaP da los valores que faltan en la celda seleccionada
                var posicionEnP = Utilidades.DivisionSintetica(MiSolucion.MiCA.Matriz[renglonDelCA], MiP.J[filaP], MiSolucion.MiCA.V, MiSolucion.MiCA.T, out int comodin);
                if (posicionEnP == -1) continue; // Si la conversion esta mal
                if (MiP.P[filaP][posicionEnP] <= 1) continue; // Si el remplazo genera otro cero en P no lo haga

                var valoresEnP = Utilidades.MultiplicacionSintetica(columnaP, MiSolucion.MiCA.V, MiP.J[filaP], MiSolucion.MiCA.T);

                // filaP permite sacar de J[filaP] la combinación de columas implicadas en la celda seleccionada
                // La multiplicacion sintetica de la columnaP da los valores que faltan en la celda seleccionada
                for (var columna = 0; columna < MiSolucion.MiCA.K; columna++)
                    copiaRenglon[columna] = MiSolucion.MiCA.Matriz[renglonDelCA][columna];

                for (var columna = 0; columna < MiSolucion.MiCA.T; columna++)
                    MiSolucion.MiCA.Matriz[renglonDelCA][MiP.J[filaP][columna]] = valoresEnP[columna];

                var aportes = Validador.QuitarPonerRenglon(copiaRenglon, MiSolucion.MiCA.Matriz[renglonDelCA], MiP, MiSolucion.MiCA.V, MiSolucion.MiCA.T,
                    false);

                if (aportes > 0)
                {
                    Validador.QuitarPonerRenglon(copiaRenglon, MiSolucion.MiCA.Matriz[renglonDelCA], MiP, MiSolucion.MiCA.V, MiSolucion.MiCA.T, true);
                    totalCeros = MiP.ContarCerosEnP();
                    if (totalCeros == 0) break;
                    lista = MiP.FaltanEnP();
                }
                else
                {
                    for (var columna = 0; columna < MiSolucion.MiCA.K; columna++)
                        MiSolucion.MiCA.Matriz[renglonDelCA][columna] = copiaRenglon[columna];
                }
            }
        }
        #endregion 

        static double Minimo(int evalR, int evalS, double temp)
        {
            var exponente = -1.0 * (evalR - evalS) / temp;
            var r = Math.Exp(exponente);
            if (r < 1.0) return r;
            return 1.0;
        }
    }
}