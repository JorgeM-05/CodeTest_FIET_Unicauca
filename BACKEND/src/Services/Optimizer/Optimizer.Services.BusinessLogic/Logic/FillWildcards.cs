using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimizer.Services.BusinessLogic.Logic
{
    public class FillWildcards
    {
        public int[] Aux;
        public int[] Comodines;
      
        public FillWildcards()
        {
        }

        //1. leer y contar repeticiones y comodines
        // k se debe enviar lo mismo el valor de v
        public void CountReps(int k, int n, int v,int [][] MCA)
        { 
            Aux = new int[v];
            int [] AuxPromedio = new int[v];
            int rowActual;
            int ctnC =0;
            bool ini=true;
            int promedio = n/k;
            promedio = n / v; 
            int A = 0;
            int B = 0;
            

            for ( var row=0; row < n; row++)
            {
                rowActual = MCA[row][k];
                if(rowActual != -1)
                {
                    var ctn= Aux[rowActual];
                    Aux[rowActual]= ctn+1;
                }
                else
                {
                    Comodines[ctnC] = row;  
                    ctnC++;
                }
            }

            while (Comodines.Length != 0)
            {
                for(var i = 0; i < Aux.Length; i++)
                {
                    if(Aux[i] < promedio)  
                    {
                        
                        if (ini)//solo se ejecuta la primera vez
                        {
                            A = Aux[i];
                            ini = false;
                            continue;
                        }
                        if(Aux[i] < A)  
                        {
                            A = Aux[i];//+posicion A[][] = [cantidada][posicionAux]
                        }   
                    }
                    else
                    {
                        AuxPromedio[i] = -1;
                        if (AuxPromedio.Length == v && Comodines.Length != 0)
                        {
                            AsignarAleatorio(n, MCA, k, v);
                        }
                    }
                }
                if(A != 0)
                {   
                    var tam = Comodines.Length;
                    MCA[Comodines[tam]][k] = A; // asiganmos comodin
                    //Aux[A[A][posicion]] = A + 1;
                    //eliminar una casilla del array de comodines
                }
                
            }            
        }



        public void AsignarAleatorio(int n, int [][] MCA, int k, int v)
        {
            // pregunta?? asignar valores de abajo hacia arriba o vicebersa ??
            for(int i = 0; i < n; i++)
            {
                if(MCA[i][k] == -1)
                {
                    Random rnd = new Random();
                    MCA[i][k] = rnd.Next(v-1);
                    //eliminar una casilla del array de comodines
                    if (Comodines.Length != 0) break;

                }
            }
        }
    }



}
