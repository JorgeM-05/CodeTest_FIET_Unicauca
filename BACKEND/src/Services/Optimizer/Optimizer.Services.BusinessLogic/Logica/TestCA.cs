using System;

namespace Optimizer.Services.business_logic.Logica
{
    public class TestCA
    {
        public bool EsCA;
        public int Faltantes;
        public int[][] repeticiones;

        public void Validar(CA miCA, MatrizP miP, int Ni)
        {
            miP.Limpiar();

            int ctn =0;
            //repeticiones = new int[miP.MaxJ*][];
            for (var filaJ = 0; filaJ < miP.MaxJ; filaJ++)
            {
                
                for (int filaCA = 0; filaCA < miCA.N; filaCA++)
                {
                    
                    var columnaEnP = Utilidades.DivisionSintetica(miCA.Matriz[filaCA], miP.J[filaJ], miCA.V, miCA.T,out int comodines);
                    if (comodines >= 1) continue;
                    if (columnaEnP != -1) {
                        miP.IncrementarCelda(filaJ, columnaEnP);
                        miP.CalcularPeorFila(filaCA, filaJ, columnaEnP);
                       //cuando hay mas de dos repeticiones 
                        //if (miP.P[filaJ][columnaEnP] >= 2 && Ni == 1) {
                        //    miCA.Matriz[filaCA][miP.J[filaJ][0]] = -1;
                            
                        //}
                       
                    }
                    else
                        throw new Exception("ERROR no se puede incrementar esta posicion");
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
                columnaEnP = Utilidades.DivisionSintetica(renglonViejo, miP.J[filaJ], v, t, out int comodin);
                miP.DecrementarCelda(filaJ, columnaEnP);
                logRestados[filaJ] = columnaEnP;
            }
            for (filaJ = 0; filaJ < miP.MaxJ; filaJ++)
            {
                columnaEnP = Utilidades.DivisionSintetica(renglonNuevo, miP.J[filaJ], v, t, out int comodin);
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
