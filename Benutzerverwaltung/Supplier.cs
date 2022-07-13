using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.IO;
using System.Linq;

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

        public void writeKundeToList(Kunde kunde, int id)
        {
            kunden[kunden.IndexOf(kunden.Where(i => i.id == id).FirstOrDefault())] = kunde;
        }

        public void writeKundeToList(Kunde kunde)
        {
            //Assuming list is orderd ascending by Kunden IDs
            int lastID = kunden.Last().id;
            kunde.id = lastID + 1;
            kunden.Add(kunde);
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
