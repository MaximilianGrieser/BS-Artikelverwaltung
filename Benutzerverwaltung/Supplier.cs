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
            readCSV(this.kunden, "kunden.csv");
            readCSV(this.bestellungen, "bestellungen.csv");
            readCSV(this.artikel, "artikel.csv");
            readCSV(this.bestellpos, "bestellpositionen.csv");
            buildConstruct();
        }

        public ObservableCollection<Kunde> getKunden() {
            return kunden;
        }

        public ObservableCollection<Kunde> searchKunden(string searchWord)
        {
            ObservableCollection<Kunde> results = new ObservableCollection<Kunde>();
            string swUpper = searchWord.ToUpper();
            foreach (Kunde k in kunden)
            {
                if (k.id.ToString().Contains(searchWord) || k.vorname.ToUpper().Contains(swUpper) || k.nachname.ToUpper().Contains(swUpper))
                {
                    results.Add(k);
                }
            }
            return results;
        }

        public ObservableCollection<Bestellung> searchBestellungen(Kunde k, string searchWord)
        {
            ObservableCollection<Bestellung> results = new ObservableCollection<Bestellung>();
            string swUpper = searchWord.ToUpper();
            foreach (Bestellung b in k.bestellungen)
            {
                if (b.id.ToString().Contains(searchWord) || b.datum.ToString("M.d.yyyy").ToUpper().Contains(swUpper))
                {
                    results.Add(b);
                }
            }
            return results;
        }

        public ObservableCollection<Bestellposition> searchBespos(Bestellung b, string searchWord)
        {
            ObservableCollection<Bestellposition> results = new ObservableCollection<Bestellposition>();
            string swUpper = searchWord.ToUpper();
            foreach (Bestellposition bp in b.positionen)
            {
                if (bp.id.ToString().Contains(searchWord))
                {
                    results.Add(bp);
                }
            }
            return results;
        }

        public ObservableCollection<Artikel> getArtikel()
        {
            return artikel;
        }

        public int getIndexFromKundenID(int id)
        {
            return kunden.IndexOf(kunden.Where(i => i.id == id).FirstOrDefault());
        }

        public int getIndexFromArtikelID(int id)
        {
            return artikel.IndexOf(artikel.Where(i => i.id == id).FirstOrDefault());
        }

        public void writeKundeToList(Kunde kunde)
        {
            if (kunde.id == -1)
            {
                kunden[kunden.IndexOf(kunden.Where(i => i.id == kunde.id).FirstOrDefault())] = kunde;
            }
            else
            {
                //Assuming list is orderd ascending by Kunden IDs
                int lastID = kunden.Last().id;
                kunde.id = lastID + 1;
                kunden.Add(kunde);
            }
        }

        public void writeArtikelToList(Artikel a)
        {
            if (a.id == -1)
            {
                artikel[artikel.IndexOf(artikel.Where(i => i.id == a.id).FirstOrDefault())] = a;
            }
            else
            {
                //Assuming list is orderd ascending by Artikel IDs
                int lastID = artikel.Last().id;
                a.id = lastID + 1;
                artikel.Add(a);
            }
        }

        public void writeBestellungToList(Bestellung b)
        {
            foreach (Bestellposition bp in b.positionen)
            {
                if (bp.id == -1)
                {
                    //Assuming list is orderd ascending by Bestellpositionen IDs
                    int lastID = bestellpos.Last().id;
                    bp.id = lastID + 1;
                    bestellpos.Add(bp);
                }
            }

            if (b.id != -1)
            {
                bestellungen[bestellungen.IndexOf(bestellungen.Where(i => i.id == b.id).FirstOrDefault())] = b;
            }
            else
            {
                //Assuming list is orderd ascending by Bestellungen IDs
                int lasID = bestellungen.Last().id;
                b.id = lasID + 1;
                bestellungen.Add(b);
            }            
            buildConstruct();
        }

        private void buildConstruct()
        {
            foreach (Kunde k in kunden)
            {
                k.assignBestellungen(bestellungen);
            }
            foreach (Bestellung b in bestellungen)
            {
                b.assignBestellpos(bestellpos);
            }
            foreach (Bestellposition bpos in bestellpos)
            {
                bpos.assignArtikel(artikel);
            }
        }

        private void readCSV<T>(ObservableCollection<T> list, string dateiname) {
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
