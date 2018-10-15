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

            double tiempoInicial =
                double.Parse(txtTiempoInicial.Text);
            double tiempoFinal =
                double.Parse(txtTiempoFinal.Text);
            double frecuenciaMuestreo =
                double.Parse(txtFrecuenciaMuestreo.Text);

            Señal señal;
            Señal segundaSeñal;

            switch (cbTipoSeñal.SelectedIndex)
            {
                //Senoidal
                case 0:
                    double amplitud = double.Parse(((ConfiguracionSeñalSenoidal)(panelConfiguracion.Children[0])).txtAmplitud.Text);
                    double fase = double.Parse(((ConfiguracionSeñalSenoidal)(panelConfiguracion.Children[0])).txtFase.Text);
                    double frecuencia = double.Parse(((ConfiguracionSeñalSenoidal)(panelConfiguracion.Children[0])).txtFrecuencia.Text);
                    señal = new SeñalSenoidal(amplitud, fase, frecuencia);
                    break;
                //Rampa
                case 1:
                    señal = new SeñalRampa();
                    break;
                case 2:
                    double alpha = double.Parse(((ConfiguracionSeñalExponencial)(panelConfiguracion.Children[0])).txtAlpha.Text);
                    señal = new SeñalExponencial(alpha);
                    break;
                default:
                    señal = null;
                    break;
            }
            señal.TiempoInicial = tiempoInicial;
            señal.TiempoFinal = tiempoFinal;
            señal.FrecuenciaMuestreo = frecuenciaMuestreo;

            

            switch (cbTipoSeñal_Segunda.SelectedIndex)
            {
                //Senoidal
                case 0:
                    double amplitud = double.Parse(((ConfiguracionSeñalSenoidal)(panelConfiguracion_Segunda.Children[0])).txtAmplitud.Text);
                    double fase = double.Parse(((ConfiguracionSeñalSenoidal)(panelConfiguracion_Segunda.Children[0])).txtFase.Text);
                    double frecuencia = double.Parse(((ConfiguracionSeñalSenoidal)(panelConfiguracion_Segunda.Children[0])).txtFrecuencia.Text);
                    segundaSeñal = new SeñalSenoidal(amplitud, fase, frecuencia);
                    break;
                //Rampa
                case 1:
                    segundaSeñal = new SeñalRampa();
                    break;
                case 2:
                    double alpha = double.Parse(((ConfiguracionSeñalExponencial)(panelConfiguracion.Children[0])).txtAlpha.Text);
                    segundaSeñal = new SeñalExponencial(alpha);
                    break;
                default:
                    segundaSeñal = null;
                    break;
            }
            segundaSeñal.TiempoInicial = tiempoInicial;
            segundaSeñal.TiempoFinal = tiempoFinal;
            segundaSeñal.FrecuenciaMuestreo = frecuenciaMuestreo;

            señal.construirSeñalDigital();
            segundaSeñal.construirSeñalDigital();

            //Escalar 
            if ((bool)chEscalaAmplitud.IsChecked)
            {
                double FactorEscala = double.Parse(txtEscalaAmplitud.Text);
                señal.escalar(FactorEscala);
            }
            if ((bool)chDesplazarY.IsChecked)
            {
                double FactorDesplazarY = double.Parse(txtDesplazarY.Text);
                señal.desplazarY(FactorDesplazarY);
                //
            }
            if ((bool)chTruncar.IsChecked)
            { 
            //truncar
                float FactorUmbral = float.Parse(txtUmbral.Text);
                señal.truncar(FactorUmbral);
            }
            //

            if ((bool)chEscalaAmplitud_Segunda.IsChecked)
            {
                double FactorEscala = double.Parse(txtEscalaAmplitud_Segunda.Text);
                segundaSeñal.escalar(FactorEscala);
            }
            if ((bool)chDesplazarY_Segunda.IsChecked)
            {
                double FactorDesplazarY = double.Parse(txtDesplazarY_Segunda.Text);
                segundaSeñal.desplazarY(FactorDesplazarY);
                //
            }
            if ((bool)chTruncar_Segunda.IsChecked)
            {
                //truncar
                float FactorUmbral = float.Parse(txtUmbral_Segunda.Text);
                segundaSeñal.truncar(FactorUmbral);
            }
            //

            señal.actualizarAmplitudMaxima();
            
            plnGrafica.Points.Clear();

           
            if(señal != null)
            {
                //Recorrer una  coleccion o arreglo
                foreach (Muestra muestra in señal.Muestras)
                {

                    plnGrafica.Points.Add(new Point((muestra.X - tiempoInicial) * scrContenedor.Width, ((muestra.Y / señal.AmplitudMaxima) * ((scrContenedor.Height / 2.0) - 30) * -1)
                    + (scrContenedor.Height / 2)));
                }
                lblAmplitudMaximaY.Text = señal.AmplitudMaxima.ToString("F");
                lblAmplitudMaximaNegativaY.Text = "-" + señal.AmplitudMaxima.ToString("F");
            }
            
            plnEjeX.Points.Clear();
            //Punto del Principio
            plnEjeX.Points.Add(new Point(0, (scrContenedor.Height / 2)));
            //Punto del Fin
            plnEjeX.Points.Add(new Point(((tiempoFinal - tiempoInicial) * scrContenedor.Width), (scrContenedor.Height / 2)));

            //Punto del Principio
            plnEjeY.Points.Add(new Point(0 - tiempoInicial * scrContenedor.Width, scrContenedor.Height));
            //Punto del Fin
            plnEjeY.Points.Add(new Point(0-tiempoInicial*scrContenedor.Width,scrContenedor.Height*-1));

      



            

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

        private void cbTipoSeñal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            panelConfiguracion.Children.Clear();
            if (panelConfiguracion != null)
            {
                panelConfiguracion.Children.Clear();

                switch (cbTipoSeñal.SelectedIndex)
                {
                    case 0: //Senoidal
                        panelConfiguracion.Children.Add(new ConfiguracionSeñalSenoidal());
                        break;

                    case 1:
                        break;

                    case 2://Exponencial
                        panelConfiguracion.Children.Add(new ConfiguracionSeñalExponencial());
                        break;

                    default:
                        break;
                }
            }

        }

        private void cbTipoSeñal_Segunda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            panelConfiguracion_Segunda.Children.Clear();
            switch(cbTipoSeñal_Segunda.SelectedIndex)
            {
                case 0:
                    panelConfiguracion_Segunda.Children.Add(new ConfiguracionSeñalSenoidal());
                    break;
                case 1:
                    break;
                case 2:
                    panelConfiguracion_Segunda.Children.Add(new ConfiguracionSeñalExponencial());
                    break;
                default:
                    break;
            }
        }

        private void chDesplazarY_Checked(object sender, RoutedEventArgs e)
        {
            txtDesplazarY.IsEnabled = true;
        }
        private void chDesplazarY_Unchecked(object sender, RoutedEventArgs e)
        {
            txtDesplazarY.IsEnabled = false;
        }

        private void chEscalaAmplitud_Checked(object sender, RoutedEventArgs e)
        {
            txtEscalaAmplitud.IsEnabled= true;
        }
        private void chEscalaAmplitud_Unchecked(object sender, RoutedEventArgs e)
        {
            txtEscalaAmplitud.IsEnabled = false;
        }

        //
        private void chTruncar_Checked(object sender, RoutedEventArgs e)
        {
            txtUmbral.IsEnabled = true;
        }
        private void chTruncar_Unchecked(object sender, RoutedEventArgs e)
        {
            txtUmbral.IsEnabled = false;
        }



        





        //
    }
}
