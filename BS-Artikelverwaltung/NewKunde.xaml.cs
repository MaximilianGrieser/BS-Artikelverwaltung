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
        public NewKunde()
        {
            InitializeComponent();
        }

        public NewKunde(Kunde kunde)
        {
            InitializeComponent();
        }
    }
}
