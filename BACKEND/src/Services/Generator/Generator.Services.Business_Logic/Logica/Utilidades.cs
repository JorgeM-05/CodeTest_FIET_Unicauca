namespace Generator.Services.business_logic.Logica
{
    public static class Utilidades
    {
        /// <summary>
        /// Basado en un renglon con unos ciertos vallores y una fuerza el retorna
        /// la posición en la matriz P de la columna de los valores de las columnas seleccionadas
        /// </summary>
        /// <param name="renglonCA">el renglon completo del CA</param>
        /// <param name="lineaDeJ">Me dice que columnas debo tener en cuenta, de todas del renglon</param>
        /// <param name="v">alfabeto de las columnas</param>
        /// <param name="t">fuerza del CA que se esta creando</param>
        /// <returns></returns>
        public static int DivisionSintetica(int[] renglonCA, int[] lineaDeJ, int[] v, int t)
        {
            
            var suma = renglonCA[lineaDeJ[0]];
            for (var i = 1; i < t; i++)
                suma = suma * v[lineaDeJ[i]] + renglonCA[lineaDeJ[i]];
            return suma;
        }

        /// <summary>
        /// Retorna la combinacion de valores de una columna especifica de P
        /// columnaP es el valor al que se le va a calcular la Multiplicacion Sintetica usando 
        /// el alfabeto v y la fuerza t.
        /// </summary>
        /// <param name="columnaP"></param>
        /// <param name="v"></param>
        /// <param name="lineaDeJ"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static int[] MultiplicacionSintetica(int columnaP, int[] v, int[] lineaDeJ, int t)
        {
            var valores =  new int[t];
            for (var i = t - 1; i > 0; i--)
            {
                valores[i] = columnaP % v[lineaDeJ[i]];
                columnaP = columnaP / v[lineaDeJ[i]];
            }
            valores[0] = columnaP;
            return valores;
        }

        /// <summary>
        /// Obtiene el número de combinatorias de n en r (numerado es n y denominador es r)
        /// </summary>
        /// <param name="numerador"></param>
        /// <param name="denominador"></param>
        /// <returns></returns>
        public static int Combinatoria(int numerador, int denominador)
        {
            if (denominador > numerador) return 0;

            var comb = 1.0;
            var menor = numerador - denominador;
            if (denominador < menor) menor = denominador;

            for (var i = 1; i <= menor; i++)
            {
                comb = (numerador * 1.0 / i) * comb;
                numerador--;
            }
            return (int)comb;
        }

        /// <summary>
        /// Crea un vector con el Polinomio Mayor Que basado en el vector de entrada y la fuerza t
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="t"></param>
        /// <returns></returns>

        public static int[] CrearPMQbasadoEnVEctor(int[] vector, int t)
        {
            var nuevo = new int[t];
            for (var i = 0; i < t; i++)
                nuevo[i] = vector[i];
            return nuevo;
        }

        public static int[] CopiaDe(int[] original)
        {
            var k = original.GetLength(0);
            var nuevo = new int[k];
            for (var columna = 0; columna < k; columna++)
                nuevo[columna] = original[columna];
            return nuevo;
        }
    }
}