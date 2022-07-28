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
            if (validateInput())
            {
                string line = txtIDKunde.Text + ";" + txtNachname.Text + ";" + txtVorname.Text + ";" + txtgeburt.Text + ";" + txtStadt.Text;
                Kunde k = new Kunde(line);
                supply.writeKundeToList(k);
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

            if (String.IsNullOrEmpty(txtNachname.Text))
            {
                valid = false;
                txtNachname.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            else
            {
                txtNachname.ClearValue(Border.BorderBrushProperty);
            }
            if (String.IsNullOrEmpty(txtVorname.Text))
            {
                valid = false;
                txtVorname.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            else
            {
                txtVorname.ClearValue(Border.BorderBrushProperty);
            }
            if (String.IsNullOrEmpty(txtgeburt.Text))
            {
                valid = false;
                txtgeburt.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            else
            {
                txtgeburt.ClearValue(Border.BorderBrushProperty);
            }
            if (String.IsNullOrEmpty(txtStadt.Text))
            {
                valid = false;
                txtStadt.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            else
            {
                txtStadt.ClearValue(Border.BorderBrushProperty);
            }

            return valid;
        }
    }
}
