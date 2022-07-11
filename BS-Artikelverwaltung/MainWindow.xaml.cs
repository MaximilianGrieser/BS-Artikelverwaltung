using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private Supplier supply = new Supplier();

        public MainWindow() {
            InitializeComponent();
            kunden.ItemsSource = supply.getKunden();
        }

        private void kunden_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bestellpos.UnselectAll();
            bestellungen.UnselectAll();
            Kunde dummy = supply.getKundeFromID(kunden.SelectedIndex);

            txtIDKunde.Text = dummy.id.ToString();
            txtVorname.Text = dummy.vorname;
            txtNachname.Text = dummy.nachname;
            txtgeburt.Text = dummy.geburtsdatum.ToString();
            txtStadt.Text = dummy.stadt;

            if (dummy.bestellungen != null)
            {
                bestellungen.ItemsSource = dummy.bestellungen;
            }
        }

        private void bestellungen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bestellpos.UnselectAll();
            Kunde dummy = supply.getKundeFromID(kunden.SelectedIndex);
            Bestellung dully = dummy.bestellungen[bestellungen.SelectedIndex];

            txtIDBestellung.Text = dully.id.ToString();
            txtIDBestellungKunde.Text = dully.kundenId.ToString();
            txtDatum.Text = dully.datum.ToString();
            txtAusgeliefert.Text = dully.ausgeliefert.ToString();

            if(dully.positionen != null)
            {
                bestellpos.ItemsSource = dully.positionen;
            }
        }

        private void bestellpos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Kunde dummy = supply.getKundeFromID(kunden.SelectedIndex);
            Bestellung dully = dummy.bestellungen[bestellungen.SelectedIndex];
            Bestellposition duffy = dully.positionen[bestellpos.SelectedIndex];
            Artikel duppy = duffy.artikel[bestellpos.SelectedIndex];

            txtIDBestellpos.Text = duffy.id.ToString();
            txtIDBestellposBestellung.Text = duffy.idbestellung.ToString();
            txtIDBestellposArtikel.Text = duffy.idartikel.ToString();
            txtAnzahl.Text = duffy.anzahl.ToString();

            txtBeschreibung.Text = duppy.bezeichnung;
            txtGewicht.Text = duppy.gewicht.ToString();
            txtBestand.Text = duppy.bestand.ToString();
            txtPreis.Text = duppy.preis.ToString();
        }
    }
}
