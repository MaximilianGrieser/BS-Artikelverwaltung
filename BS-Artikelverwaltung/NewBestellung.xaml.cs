using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Benutzerverwaltung;


namespace BS_Artikelverwaltung
{
    /// <summary>
    /// Interaktionslogik für NewBestellung.xaml
    /// </summary>
    public partial class NewBestellung : Window
    {
        Supplier supply;
        Bestellung bestellung;

        public NewBestellung(Supplier supply)
        {
            this.supply = supply;
            this.bestellung = new Bestellung("-1;-1;-1;-1");
            InitializeComponent();
            kunden.ItemsSource = supply.getKunden();
            artikel.ItemsSource = supply.getArtikel();

            txtDatum.Text = DateTime.Now.ToString();
        }
        public NewBestellung(Supplier supply, Bestellung bestellung)
        {
            this.supply = supply;
            this.bestellung = bestellung;
            InitializeComponent();
            kunden.ItemsSource = supply.getKunden();
            kunden.SelectedIndex = supply.getIndexFromKundenID(bestellung.kundenId);
            artikel.ItemsSource = supply.getArtikel();
            bestellpos.ItemsSource = bestellung.positionen;

            txtIDBestellung.Text = bestellung.id.ToString();
            txtIDBestellungKunde.Text = bestellung.kundenId.ToString();
            txtDatum.Text = bestellung.datum.ToString();
            cbxAusgeliefert.IsChecked = bestellung.ausgeliefert;
        }

        private void btnSaveBestellung_Click(object sender, RoutedEventArgs e)
        {
            Kunde dummy = supply.getKundeFromIndex(kunden.SelectedIndex);

            bestellung.kundenId = dummy.id;
            bestellung.datum = Convert.ToDateTime(txtDatum.Text);
            bestellung.ausgeliefert = Convert.ToBoolean(cbxAusgeliefert.IsChecked);

            supply.writeBestellungToList(bestellung);
            this.Close();
        }

        private void btnAddArtikelToBestellung_Click(object sender, RoutedEventArgs e)
        {
            if (artikel.SelectedIndex > -1)
            {
                Artikel a = supply.getArtikelFromIndex(artikel.SelectedIndex);
                Bestellposition bp = new Bestellposition("-1;" + bestellung.id.ToString() + ";" + a.id.ToString() + ";" + txtAnzahl.Text);
                bp.artikel = a;
                bestellung.positionen.Add(bp);
                bestellpos.ItemsSource = bestellung.positionen;
            }
            else
            {
                MessageBox.Show("Bitte wähle zuerst einen Artikel");
            }
        }

        private void kunden_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (kunden.SelectedIndex > -1)
            {
                Kunde dummy = supply.getKundeFromIndex(kunden.SelectedIndex);
                txtIDBestellungKunde.Text = dummy.id.ToString();
            }
        }

        private void bestellpos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (bestellpos.SelectedIndex > -1)
            {
                txtAnzahl.Text = bestellung.positionen[bestellpos.SelectedIndex].anzahl.ToString();
                artikel.SelectedIndex = supply.getIndexFromArtikelID(bestellung.positionen[bestellpos.SelectedIndex].artikel.id);
            }
        }
    }
}
