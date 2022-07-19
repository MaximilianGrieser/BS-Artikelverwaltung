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

        public NewBestellung(Supplier supply, int newID)
        {
            this.supply = supply;
            this.bestellung = new Bestellung(newID + ";-1;-1;-1");
            InitializeComponent();
            kunden.ItemsSource = supply.getKunden();
            artikel.ItemsSource = supply.getArtikel();
            txtIDBestellung.Text = newID.ToString();
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
            Kunde dummy = kunden.SelectedItem as Kunde;
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
                Artikel a = artikel.SelectedItem as Artikel;
                int newID = supply.getNewBesPosID();
                Bestellposition bp = new Bestellposition(newID + ";" + bestellung.id.ToString() + ";" + a.id.ToString() + ";" + txtAnzahl.Text);
                bp.artikel = a;
                bestellung.positionen.Add(bp);
                bestellpos.ItemsSource = bestellung.positionen;
            }
            else
            {
                MessageBox.Show("Bitte wähle zuerst einen Artikel");
            }
            txtAnzahl.Text = "";
        }

        private void kunden_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (kunden.SelectedIndex > -1)
            {
                Kunde dummy = kunden.SelectedItem as Kunde;
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

        private void txtKundeSuche_TextChanged(object sender, TextChangedEventArgs e)
        {
            kunden.ItemsSource = supply.searchKunden(txtKundeSuche.Text);
        }
    }
}
