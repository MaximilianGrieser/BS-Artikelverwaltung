using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benutzerverwaltung {
    public class Bestellung {

        public ObservableCollection<Bestellposition> positionen = new ObservableCollection<Bestellposition>();
        public Bestellung(int id, int kundenId, string datum, bool ausgeliefert) {
            this.id = id;
            this.kundenId = kundenId;
            this.datum = datum;
            this.ausgeliefert = ausgeliefert;
        }

        public Bestellung(string ccsv) {
            List<Bestellung> Bestellungsliste = new List<Bestellung>();

            string[] einzeln = ccsv.Split(';');

            this.id = Convert.ToInt32(einzeln[0]);
            this.kundenId = Convert.ToInt32(einzeln[1]);
            this.datum = einzeln[2];
            this.ausgeliefert = Convert.ToBoolean(Convert.ToInt32((einzeln[3])));
        }

        public int id { get; set; }
        public int kundenId { get; set; }
        public string datum { get; set; }
        public bool ausgeliefert { get; set; }
        public void assignBestellpos(ObservableCollection<Bestellposition> best) { 
            foreach (Bestellposition bspos in best) {
                if (bspos.idbestellung == this.id) {
                    this.positionen.Add(bspos);
                }
            }
        }
    }
}
