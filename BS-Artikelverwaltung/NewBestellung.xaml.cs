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
        List<int> removedBesPos = new List<int>();

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

        public void refreshBestand() { 
        }

        private void btnSaveBestellung_Click(object sender, RoutedEventArgs e)
        {
            if (kunden.SelectedIndex > -1)
            {
                if (validateInput(bestellung))
                {
                    Kunde dummy = kunden.SelectedItem as Kunde;
                    bestellung.kundenId = dummy.id;
                    bestellung.datum = Convert.ToDateTime(txtDatum.Text);
                    bestellung.ausgeliefert = Convert.ToBoolean(cbxAusgeliefert.IsChecked);
                    supply.writeBestellungToList(bestellung, removedBesPos);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Bitte eingaben überprüfen!");
                }
            }
            else
            {
                MessageBox.Show("Bitte wähle einen Kunden");
            }
        }

        private void btnAddArtikelToBestellung_Click(object sender, RoutedEventArgs e)
        {
            if (artikel.SelectedIndex > -1)
            {
                if (String.IsNullOrEmpty(txtAnzahl.Text))
                {
                    MessageBox.Show("Bitte Anzahl eingeben");
                    txtAnzahl.BorderBrush = System.Windows.Media.Brushes.Red;
                }
                else
                {
                    txtAnzahl.ClearValue(Border.BorderBrushProperty);
                    Artikel a = artikel.SelectedItem as Artikel;
                    Bestellposition newPB = supply.getNewBesPos(a.id, bestellung.id, Convert.ToInt32(txtAnzahl.Text));
                    artikel.ItemsSource = null;
                    artikel.ItemsSource = supply.getArtikel();
                    if (newPB != null)
                    {
                        newPB.artikel = a;
                        bestellung.positionen.Add(newPB);
                        bestellpos.ItemsSource = bestellung.positionen;
                    }
                    else
                    {
                        MessageBox.Show("Bestand Nicht Ausreichend!");
                    }

                }
            }
            else
            {
                MessageBox.Show("Bitte wähle einen Artikel");
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

        private void txtArtikelSuche_TextChanged(object sender, TextChangedEventArgs e)
        {
            artikel.ItemsSource = supply.searchArtikel(txtArtikelSuche.Text);
        }

        private bool validateInput(Bestellung b)
        {
            bool valid = true;

            if (String.IsNullOrEmpty(txtDatum.Text))
            {
                valid = false;
                txtDatum.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            if (b.positionen.Count == 0)
            {
                valid = false;
                bestellpos.BorderBrush = System.Windows.Media.Brushes.Red;
            }

            return valid;
        }

        private void btnDeleteBespos_Click(object sender, RoutedEventArgs e)
        {
            if (bestellpos.SelectedIndex > -1)
            {
                removedBesPos.Add((bestellpos.SelectedItem as Bestellposition).id);
                bestellung.positionen.RemoveAt(bestellpos.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Bitte wähle eine BestellPos");
            }
        }
    }
}
