using System;

namespace Generator.Services.business_logic.Logica
{
    public class TestCA
    {
        public bool EsCA;
        public int Faltantes;
        

        public void Validar(CA miCA, MatrizP miP)
        {
            miP.Limpiar();
            for (var filaJ = 0; filaJ < miP.MaxJ; filaJ++)
            {
                for (var filaCA = 0; filaCA < miCA.N; filaCA++)
                {
                    var columnaEnP = Utilidades.DivisionSintetica(miCA.Matriz[filaCA], miP.J[filaJ], miCA.V, miCA.T);
                    //contar comodines para dicha combinacion
                    if (columnaEnP != -1)
                    {
                        miP.IncrementarCelda(filaJ, columnaEnP);
                        miP.CalcularPeorFila(filaCA, filaJ, columnaEnP);
                    }
                    else
                    {
                        throw new Exception("ERROR no se puede incrementar esta posicion");
                    }
                }
            }
            EsCA = true;
            Faltantes = miP.ContarCerosEnP();

            if (Faltantes != 0) EsCA = false;
        }

        public int QuitarPonerRenglon(int[] renglonViejo, int[] renglonNuevo, MatrizP miP, 
                                      int[] v, int t, bool modificarP)
        {
            int filaJ;
            int columnaEnP;
            var logSumados = new int [miP.MaxJ];
            var logRestados = new int[miP.MaxJ];

            for (filaJ = 0; filaJ < miP.MaxJ; filaJ++)
            {
                columnaEnP = Utilidades.DivisionSintetica(renglonViejo, miP.J[filaJ], v, t);
                miP.DecrementarCelda(filaJ, columnaEnP);
                logRestados[filaJ] = columnaEnP;
            }
            for (filaJ = 0; filaJ < miP.MaxJ; filaJ++)
            {
                columnaEnP = Utilidades.DivisionSintetica(renglonNuevo, miP.J[filaJ], v, t);
                miP.IncrementarCelda(filaJ, columnaEnP);
                logSumados[filaJ] = columnaEnP;
            }

            var ceros = miP.ContarCerosEnP();

            if (modificarP == false)
            {
                // se devuelven las operaciones en P
                for (filaJ = 0; filaJ < miP.MaxJ; filaJ++)
                    miP.DecrementarCelda(filaJ, logSumados[filaJ]); // se quitan las insertadas
                for (filaJ = 0; filaJ < miP.MaxJ; filaJ++)
                    miP.IncrementarCelda(filaJ, logRestados[filaJ]); // se ponen las eliminadas
            }
            return ceros;
        }
    }
}
