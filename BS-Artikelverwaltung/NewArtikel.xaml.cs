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

        public NewArtikel(Supplier supply)
        {
            this.supply = supply;
            InitializeComponent();
            lblArtikel.Content = "Neuen Artikel Erstellen:";
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
            string line;
            if (txtIDArtikel.Text != "")
            {
                line = txtIDArtikel.Text + ";" + txtBeschreibung.Text + ";" + txtGewicht.Text + ";" + txtBestand.Text + ";" + txtPreis.Text;
            }
            else
            {
                line = "-1;" + txtBeschreibung.Text + ";" + txtGewicht.Text + ";" + txtBestand.Text + ";" + txtPreis.Text;
            }
            Artikel a = new Artikel(line);
            supply.writeArtikelToList(a);
            this.Close();
        }
    }
}
