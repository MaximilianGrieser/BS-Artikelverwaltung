using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benutzerverwaltung {
    public class Bestellung {

        public ObservableCollection<Bestellposition> positionen = new ObservableCollection<Bestellposition>();
        public Bestellung(int id, int kundenId, DateTime datum, bool ausgeliefert) {
            this.id = id;
            this.kundenId = kundenId;
            this.datum = datum;
            this.ausgeliefert = ausgeliefert;
        }


        public int id { get; set; }
        public int kundenId { get; set; }
        public DateTime datum { get; set; }
        public bool ausgeliefert { get; set; }
    }
}
