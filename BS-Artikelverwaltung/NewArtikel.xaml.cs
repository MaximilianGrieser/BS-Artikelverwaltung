using System;
using System.Collections.Generic;
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
    /// Interaktionslogik für NewArtikel.xaml
    /// </summary>
    public partial class NewArtikel : Window
    {
        Supplier supply;

        public NewArtikel(Supplier supply, int newID)
        {
            this.supply = supply;
            InitializeComponent();
            lblArtikel.Content = "Neuen Artikel Erstellen:";
            txtIDArtikel.Text = newID.ToString();
        }

        public NewArtikel(Supplier supply, Artikel artikel)
        {
            this.supply = supply;
            InitializeComponent();
            lblArtikel.Content = "Exisiterenden Artikel Bearbeiten:";

            txtIDArtikel.Text = artikel.id.ToString();
            txtBeschreibung.Text = artikel.bezeichnung;
            txtGewicht.Text = artikel.gewicht.ToString();
            txtBestand.Text = artikel.bestand.ToString();
            txtPreis.Text = artikel.preis.ToString();
        }

        private void btnSaveArtikel_Click(object sender, RoutedEventArgs e)
        {
            if (validateInput())
            {
                string line = txtIDArtikel.Text + ";" + txtBeschreibung.Text + ";" + txtGewicht.Text + ";" + txtBestand.Text + ";" + txtPreis.Text;
                Artikel a = new Artikel(line);
                supply.writeArtikelToList(a);
                this.Close();
            }
            else
            {
                MessageBox.Show("Bitte eingaben überprüfen!");
            }
        }

        private bool validateInput()
        {
            bool valid = true;

            if (String.IsNullOrEmpty(txtBeschreibung.Text))
            {
                valid = false;
                txtBeschreibung.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            else
            {
                txtBeschreibung.ClearValue(Border.BorderBrushProperty);
            }
            if (String.IsNullOrEmpty(txtGewicht.Text))
            {
                valid = false;
                txtGewicht.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            else
            {
                txtGewicht.ClearValue(Border.BorderBrushProperty);
            }
            if (String.IsNullOrEmpty(txtBestand.Text))
            {
                valid = false;
                txtBestand.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            else
            {
                txtBestand.ClearValue(Border.BorderBrushProperty);
            }
            if (String.IsNullOrEmpty(txtPreis.Text))
            {
                valid = false;
                txtPreis.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            else
            {
                txtPreis.ClearValue(Border.BorderBrushProperty);
            }

            return valid;
        }
    }
}
