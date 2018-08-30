using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSeñales
{
    class SeñalRampa
    {
        public double Amplitud { get; set; }
        public double Fase { get; set; }
        public double Frecuencia { get; set; }

        public List<Muestra> Muestras { get; set; }
        

        public SeñalRampa()
        {
            Frecuencia = 1.0;
            Muestras = new List<Muestra>();
        }


        public SeñalRampa(double amplitud, double fase, double frecuencia)
        {
            Frecuencia = frecuencia;
            Muestras = new List<Muestra>();
        }

        public double EvaluarRampa(double tiempo)
        {
            double resultado;
            if (tiempo >= 0) resultado = tiempo;
            else resultado = 0;
            return resultado;
        }
    }
}
