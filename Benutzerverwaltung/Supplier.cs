using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.IO;

namespace Benutzerverwaltung {
    public class Supplier {
        public ObservableCollection<Kunde> kunden = new ObservableCollection<Kunde>();
        public ObservableCollection<Artikel> artikel = new ObservableCollection<Artikel>();
        public ObservableCollection<Bestellung> bestellungen = new ObservableCollection<Bestellung>();
        public ObservableCollection<Bestellposition> bestellpos = new ObservableCollection<Bestellposition>();

        public Supplier() {
            this.kunden = this.getKunden();
        }

        public ObservableCollection<Kunde> getKunden() {
            readCSV(this.kunden, "kunden.csv");
            readCSV(this.bestellungen, "bestellungen.csv");
            readCSV(this.artikel, "artikel.csv");
            readCSV(this.bestellpos, "bestellpositionen.csv");


            foreach(Kunde k in kunden) {
                k.assignBestellungen(bestellungen);
            }
            foreach (Bestellung b in bestellungen) {
                b.assignBestellpos(bestellpos);
            }
            foreach(Bestellposition bpos in bestellpos) {
                bpos.assignArtikel(artikel);
            }

            Console.WriteLine("TEST");
            return kunden;
        }

        public Kunde getKundeFromID(int id)
        {
            return kunden[id];
        }

        public void readCSV<T>(ObservableCollection<T> list, string dateiname) {
            list.Clear();
            string[] csv = File.ReadAllLines(dateiname);
            int count = 0;
            foreach (string s in csv) {
                if (count == 0) {
                    count = 1;
                    continue;
                }
                list.Add((T)Activator.CreateInstance(typeof(T), new object[] { s }));
            }
        }
    }
}
