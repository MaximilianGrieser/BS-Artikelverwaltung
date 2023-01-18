using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.IO;
using System.Linq;

namespace Benutzerverwaltung
{
    public class Supplier
    {
        public ObservableCollection<Kunde> kunden = new ObservableCollection<Kunde>();
        public ObservableCollection<Artikel> artikel = new ObservableCollection<Artikel>();
        public ObservableCollection<Bestellung> bestellungen = new ObservableCollection<Bestellung>();
        public ObservableCollection<Bestellposition> bestellpos = new ObservableCollection<Bestellposition>();

        public Supplier(bool isSQL)
        {
            if (isSQL)
            {
                DB_Handler db = new DB_Handler();
                this.kunden = db.getKunden();
                //this.kunden = db.getArtikel();
                this.bestellungen = db.getBestellungen();
                //this.kunden = db.getBestellpositionen();
            }
            else
            {
                //readCSV(this.kunden, "kunden.csv");
                readCSV(this.bestellungen, "bestellungen.csv");
                //readCSV(this.artikel, "artikel.csv");
                //readCSV(this.bestellpos, "bestellpositionen.csv");
            }

            buildConstruct();
        }

        public ObservableCollection<Kunde> getKunden()
        {
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
            foreach (Bestellposition bp in b.positionen)
            {
                if (bp.id.ToString().Contains(searchWord))
                {
                    results.Add(bp);
                }
            }
            return results;
        }

        public ObservableCollection<Artikel> searchArtikel(string searchWord)
        {
            ObservableCollection<Artikel> results = new ObservableCollection<Artikel>();
            string swUpper = searchWord.ToUpper();
            foreach (Artikel a in artikel)
            {
                if (a.id.ToString().Contains(searchWord) || a.bezeichnung.ToUpper().Contains(swUpper) || a.bestand.ToString().Contains(searchWord))
                {
                    results.Add(a);
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

        public int getNewKundeID()
        {
            //Assuming list is orderd ascending by Kunden IDs
            int lastID = kunden.Last().id;
            return lastID + 1;
        }

        public void writeKundeToList(Kunde kunde)
        {
            if (kunden.Where(i => i.id == kunde.id).FirstOrDefault() != null)
            {
                kunden[kunden.IndexOf(kunden.Where(i => i.id == kunde.id).FirstOrDefault())] = kunde;
            }
            else
            {
                kunden.Add(kunde);
            }
        }

        public int getNewArtikelID()
        {
            //Assuming list is orderd ascending by Artikel IDs
            int lastID = artikel.Last().id;
            return lastID + 1;
        }

        public void writeArtikelToList(Artikel a)
        {
            if (artikel.Where(i => i.id == a.id).FirstOrDefault() != null)
            {
                artikel[artikel.IndexOf(artikel.Where(i => i.id == a.id).FirstOrDefault())] = a;
                buildBespos();
            }
            else
            {
                artikel.Add(a);
            }
        }

        public int getNewBesID()
        {
            //Assuming list is orderd ascending by Bestellungen IDs
            int lastID = bestellungen.Last().id;
            return lastID + 1;
        }

        public Bestellposition getNewBesPos(int artikelID, int bestellID, int anzahl)
        {
            //Assuming list is orderd ascending by Bestellpositionen IDs
            int lastID = bestellpos.Last().id;
            Artikel a = artikel.Where(i => i.id == artikelID).FirstOrDefault();
            if (a.bestand >= anzahl)
            {
                a.bestand = a.bestand - anzahl;
                Bestellposition bp = new Bestellposition(lastID + 1 + ";" + bestellID + ";" + artikelID + ";" + anzahl);
                bestellpos.Add(bp);
                return bp;
            }
            else
            {
                return null;
            }
        }
        public void writeBestellungToList(Bestellung b, List<int> removed)
        {
            foreach (int bp in removed)
            {
                bestellpos.Remove(bestellpos.Where(i => i.id == bp).FirstOrDefault());
            }

            foreach (Bestellposition bp in b.positionen)
            {
                if (bestellpos.Where(i => i.id == bp.id).FirstOrDefault() != null)
                {
                    bestellpos[bestellpos.IndexOf(bestellpos.Where(i => i.id == bp.id).FirstOrDefault())] = bp;
                }
                else
                {
                    bestellpos.Add(bp);
                }
            }

            if (bestellungen.Where(i => i.id == b.id).FirstOrDefault() != null)
            {
                bestellungen[bestellungen.IndexOf(bestellungen.Where(i => i.id == b.id).FirstOrDefault())] = b;
            }
            else
            {
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
            buildBespos();
        }

        void buildBespos()
        {
            foreach (Bestellposition bpos in bestellpos)
            {
                bpos.assignArtikel(artikel);
            }
        }

        private void readCSV<T>(ObservableCollection<T> list, string dateiname)
        {
            list.Clear();
            string[] csv = File.ReadAllLines(dateiname);
            int count = 0;
            foreach (string s in csv)
            {
                if (count == 0)
                {
                    count = 1;
                    continue;
                }
                list.Add((T)Activator.CreateInstance(typeof(T), new object[] { s }));
            }
        }
    }
}
