using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimizer.Services.BusinessLogic.Logic
{
    public class ElementVector
    {
        public int[] contenido;
        public int comunes;
        public int comodines;
        public int filaA;
        public int filaB;

        public ElementVector(int k)
        {
            contenido = new int[k];
            comunes = 0;
            comodines = 0;
            filaA = 0;
            filaB = 0;
        }

        public ElementVector(int[] Contenido, int k, int argFilaA, int argFilaB)
        {
            this.contenido = new int[k];
            this.contenido = Contenido;
            filaA = argFilaA;
            filaB = argFilaB;
        }



        internal bool IsEmpty(int k)
        {
            for (int i = 0; i < k; i++)
            {
                if (this.contenido[i] != -1)
                    return false;
            }
            return true;
        }

        internal int[] newEmptyElement(int k)
        {
            int[] r = new int[k];
            for (int i = 0; i < k; i++) r[i] = -1;
            return r;
        }
    }
}
