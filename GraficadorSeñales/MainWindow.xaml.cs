using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraficadorSeñales
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            
            
           
        }

        private void btnGraficar_Click(object sender, RoutedEventArgs e)
        {
            double amplitud =
                double.Parse(txtAplitud.Text);
            double fase =
                double.Parse(txtFase.Text);
            double frecuencia =
                double.Parse(txtFrecuencia.Text);

            double tiempoInicial =
                double.Parse(txtTiempoInicial.Text);
            double tiempoFinal =
                double.Parse(txtTiempoFinal.Text);
            double frecuenciaMuestreo =
                double.Parse(txtFrecuenciaMuestreo.Text);

            Señal señal;
            switch (cbTipoSeñal.SelectedIndex)
            {
                //Senoidal
                case 0:
                señal = new SeñalSenoidal(amplitud, fase, frecuencia);
                break;
                case 1:
                señal = new SeñalRampa();
                break;
                default:
                    señal = null;
               break;
            }
            señal.TiempoInicial = tiempoInicial;
            señal.TiempoFinal = tiempoFinal;
            señal.FrecuenciaMuestreo = frecuenciaMuestreo;

            señal.construirSeñalDigital();

            plnGrafica.Points.Clear();

           
            if(señal != null)
            {
                //Recorrer una  coleccion o arreglo
                foreach (Muestra muestra in señal.Muestras)
                {

                    plnGrafica.Points.Add(new Point((muestra.X - tiempoInicial) * scrContenedor.Width, ((muestra.Y / señal.AmplitudMaxima) * ((scrContenedor.Height / 2.0) - 30) * -1)
                    + (scrContenedor.Height / 2)));
                }
                lblAmplitudMaximaY.Text = señal.AmplitudMaxima.ToString();
                lblAmplitudMaximaNegativaY.Text = "-" + señal.AmplitudMaxima.ToString();
            }
            
            plnEjeX.Points.Clear();
            //Punto del Principio
            plnEjeX.Points.Add(new Point(0, (scrContenedor.Height / 2)));
            //Punto del Fin
            plnEjeX.Points.Add(new Point(((tiempoFinal - tiempoInicial) * scrContenedor.Width), (scrContenedor.Height / 2)));

            //Punto del Principio
            plnEjeY.Points.Add(new Point((0-tiempoInicial)*scrContenedor.Width, //COORDENDA X PUNTO INICIAL
                ((señal.AmplitudMaxima) * ((scrContenedor.Height / 2.0) - 30) * -1) //VOORDENADA Y PUNTO FINAL 
                + (scrContenedor.Height / 2)));
            //Punto del Fin
            plnEjeY.Points.Add(new Point(((0 - tiempoInicial) * scrContenedor.Width), //X FINAL
                (-señal.AmplitudMaxima)*(((scrContenedor.Height / 2.0) - 30) * -1) //Y final
                + (scrContenedor.Height / 2)));



            

        }

        private void btnGraficarRampa_Click(object sender, RoutedEventArgs e)
        {
            //todas las señales ocupan estas 3
            double tiempoInicial =
                double.Parse(txtTiempoInicial.Text);
            double tiempoFinal =
                double.Parse(txtTiempoFinal.Text);
            double frecuenciaMuestreo =
                double.Parse(txtFrecuenciaMuestreo.Text);

            SeñalRampa señal =
                new SeñalRampa();

            double periodoMuestreo = 1 / frecuenciaMuestreo;

            plnGrafica.Points.Clear();

            for (double i = tiempoInicial; i <= tiempoFinal; i += periodoMuestreo)
            {
                double valorMuestra = señal.evaluar(i);

                señal.Muestras.Add(new Muestra(i, valorMuestra));
                //Recorrer una  coleccion o arreglo Aqui se agregan los puntos
                
            }
            //Recorrer una  coleccion o arreglo Aqui se agregan los puntos
            foreach (Muestra muestra in señal.Muestras)
            {
                plnGrafica.Points.Add(new Point(muestra.X * scrContenedor.Width, (muestra.Y * ((scrContenedor.Height / 2.0) - 30) * -1)
                + (scrContenedor.Height / 2)));
            }
        }
    }
}
