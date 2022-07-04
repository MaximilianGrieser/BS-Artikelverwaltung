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
using Benutzerverwaltung;

namespace BS_Artikelverwaltung {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            List<Kunde> kunden = new List<Kunde>();
            kunden.Add(new Kunde("18293;Kiesel;Imre;1966-11-02;Gelsenkirchen"));
            kunden.Add(new Kunde("18293;Kiesel;Imre;1966-11-02;Gelsenkirchen"));
            kunden.Add(new Kunde("18293;Kiesel;Imre;1966-11-02;Gelsenkirchen"));
            kunden.Add(new Kunde("18293;Kiesel;Imre;1966-11-02;Gelsenkirchen"));

            Kundenliste.ItemsSource = kunden;

        }
    }
}
