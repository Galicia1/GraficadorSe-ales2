using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSeñales
{
    class SeñalRampa
    {


        public List<Muestra> Muestras { get; set; }
        

        public SeñalRampa()
        {
            Muestras = new List<Muestra>();
        }


        public double EvaluarRampa(double tiempo)
        {
            double resultado;
            if (tiempo >= 0) { resultado = tiempo; }
            else { resultado = 0; }
            return resultado;
        }
    }
}
