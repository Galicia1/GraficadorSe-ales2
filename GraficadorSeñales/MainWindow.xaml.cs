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

            plnGrafica.Points.Add(new Point(0,10));
            plnGrafica.Points.Add(new Point(50,20));
            plnGrafica.Points.Add(new Point(150,10));
            plnGrafica.Points.Add(new Point(250,50));
            plnGrafica.Points.Add(new Point(300,0));
            plnGrafica.Points.Add(new Point(350,100));
            plnGrafica.Points.Add(new Point(450,50));
            plnGrafica.Points.Add(new Point(500,0));
            plnGrafica.Points.Add(new Point(550,100));
            plnGrafica.Points.Add(new Point(650,50));
            plnGrafica.Points.Add(new Point(700,0));
            plnGrafica.Points.Add(new Point(850,50));
            plnGrafica.Points.Add(new Point(900,0));
            plnGrafica.Points.Add(new Point(1150,100));
            
           
        }
    }
}
