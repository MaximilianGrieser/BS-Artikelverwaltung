using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benutzerverwaltung {
    public class Kunde {

        public ObservableCollection<Bestellung> bestellungen = new ObservableCollection<Bestellung>();
        public Kunde(string zeile) {
            string[] item = zeile.Split(';');
            this.id = Convert.ToInt32(item[0]);
            this.nachname = item[1];
            this.vorname = item[2];
            this.geburtsdatum = Convert.ToDateTime(item[3]);
            this.stadt = item[4];
            //DB_Handler db = new DB_Handler();
            //db.addKunde(item[0], item[1], item[2], item[3], item[4]);
        }

        public Kunde (string id, string nachname, string vorname, string geburtsdatum, string stadt) {
            this.id = Convert.ToInt32(id);
            this.nachname = nachname;
            this.vorname = vorname;
            this.geburtsdatum = Convert.ToDateTime(geburtsdatum);
            this.stadt = stadt;

        }

        public int id { get; set; }
        public string nachname { get; set; }
        public string vorname { get; set; }
        public DateTime geburtsdatum { get; set; }
        public string stadt { get; set; }

        public void assignBestellungen(ObservableCollection<Bestellung> best) {
            this.bestellungen.Clear();
            foreach (Bestellung bs in best) {
                if(bs.kundenId == this.id) {
                    this.bestellungen.Add(bs);
                }
            }
        }
    }
}
