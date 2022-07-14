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

namespace BS_Artikelverwaltung
{
    //TODOS
    //- Save To File
    //- Label Font Size anpassung allgemein
    //- Artikel List mit artikel bezeichnung und anzahl ergänzen
    //- NewBestellung edit bespos
    //- NewBestellung delete bespos
    //- NewBestellung leere Bestellung speichern bug
    //- Objekte löschen ?
    //- User Error abfangen



    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Supplier supply = new Supplier();

        public MainWindow()
        {
            //var splashScreen = new SplashScreen("memes-loading.gif");
            //splashScreen.Show(false);

            InitializeComponent();
            kunden.ItemsSource = supply.getKunden();

            //splashScreen.Close(TimeSpan.FromSeconds(1));
        }

        private void kunden_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clearBesFields();
            if (kunden.SelectedIndex > -1)
            {
                Kunde dummy = supply.getKundeFromIndex(kunden.SelectedIndex);

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
        }

        private void bestellungen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clearArtikelFields();
            if (bestellungen.SelectedIndex > -1)
            {
                Kunde dummy = supply.getKundeFromIndex(kunden.SelectedIndex);
                Bestellung dully = dummy.bestellungen[bestellungen.SelectedIndex];

                txtIDBestellung.Text = dully.id.ToString();
                txtIDBestellungKunde.Text = dully.kundenId.ToString();
                txtDatum.Text = dully.datum.ToString();
                cbxAusgeliefert.IsChecked = dully.ausgeliefert;

                if (dully.positionen != null)
                {
                    bestellpos.ItemsSource = dully.positionen;
                }
            }
        }

        private void bestellpos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (bestellpos.SelectedIndex > -1)
            {
                Kunde dummy = supply.getKundeFromIndex(kunden.SelectedIndex);
                Bestellung dully = dummy.bestellungen[bestellungen.SelectedIndex];
                Bestellposition duffy = dully.positionen[bestellpos.SelectedIndex];
                Artikel duppy = duffy.artikel;

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

        private void clearBesFields()
        {
            txtIDBestellung.Text = "";
            txtIDBestellungKunde.Text = "";
            txtDatum.Text = "";
            cbxAusgeliefert.IsChecked = false;
            clearArtikelFields();
        }

        private void clearArtikelFields()
        {
            txtIDBestellpos.Text = "";
            txtIDBestellposBestellung.Text = "";
            txtIDBestellposArtikel.Text = "";
            txtAnzahl.Text = "";

            txtBeschreibung.Text = "";
            txtGewicht.Text = "";
            txtBestand.Text = "";
            txtPreis.Text = "";
            bestellpos.ItemsSource = null;
        }

        private void btnNewKunde_Click(object sender, RoutedEventArgs e)
        {
            NewKunde kWindow = new NewKunde(supply);
            kWindow.ShowDialog();
        }

        private void btnNewBestellung_Click(object sender, RoutedEventArgs e)
        {
            NewBestellung bWindow = new NewBestellung(supply);
            bWindow.ShowDialog();
        }

        private void btnNewArtikel_Click(object sender, RoutedEventArgs e)
        {
            NewArtikel aWindow = new NewArtikel(supply);
            aWindow.ShowDialog();
        }

        private void btnEditKunde_Click(object sender, RoutedEventArgs e)
        {
            if (kunden.SelectedIndex > -1)
            {
                NewKunde kWindow = new NewKunde(supply, supply.getKundeFromIndex(kunden.SelectedIndex));
                kWindow.Show();
            }
            else
            {
                MessageBox.Show("Bitte Kunden Auswählen um zu Bearbeiten");
            }
        }

        private void btnEditBestellung_Click(object sender, RoutedEventArgs e)
        {
            if (bestellungen.SelectedIndex > -1)
            {
                Kunde dummy = supply.getKundeFromIndex(kunden.SelectedIndex);
                NewBestellung bWindow = new NewBestellung(supply, dummy.bestellungen[bestellungen.SelectedIndex]);
                bWindow.Show();
            }
            else
            {
                MessageBox.Show("Bitte Bestellung Auswählen um zu Bearbeiten");
            }
        }

        private void btnEditArtikel_Click(object sender, RoutedEventArgs e)
        {
            if (bestellpos.SelectedIndex > -1)
            {
                Kunde dummy = supply.getKundeFromIndex(kunden.SelectedIndex);
                Bestellung dully = dummy.bestellungen[bestellungen.SelectedIndex];
                Bestellposition duffy = dully.positionen[bestellpos.SelectedIndex];
                NewArtikel aWindow = new NewArtikel(supply, duffy.artikel);
                aWindow.Show();
            }
            else
            {
                MessageBox.Show("Bitte Bestellung Auswählen um zu Bearbeiten");
            }
        }
    }
}
