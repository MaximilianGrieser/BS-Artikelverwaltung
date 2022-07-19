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
    /// Interaktionslogik für NewKunde.xaml
    /// </summary>
    public partial class NewKunde : Window
    {
        Supplier supply;

        public NewKunde(Supplier supply, int newID)
        {
            this.supply = supply;
            InitializeComponent();
            lblKunde.Content = "Neuen Kunden Erstellen:";
            txtIDKunde.Text = newID.ToString();
        }

        public NewKunde(Supplier supply, Kunde kunde)
        {
            this.supply = supply;
            InitializeComponent();
            lblKunde.Content = "Exisiterenden Kunden Bearbeiten:";

            txtIDKunde.Text = kunde.id.ToString();
            txtVorname.Text = kunde.vorname;
            txtNachname.Text = kunde.nachname;
            txtgeburt.Text = kunde.geburtsdatum.ToString();
            txtStadt.Text = kunde.stadt;
        }

        private void btnSaveKunde_Click(object sender, RoutedEventArgs e)
        {
            string line = txtIDKunde.Text + ";" + txtNachname.Text + ";" + txtVorname.Text + ";" + txtgeburt.Text + ";" + txtStadt.Text;
            Kunde k = new Kunde(line);
            supply.writeKundeToList(k);
            this.Close();
        }
    }
}
