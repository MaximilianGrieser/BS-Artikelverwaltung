using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benutzerverwaltung {

    

    public class Bestellposition {

        public ObservableCollection<Artikel> artikel = new ObservableCollection<Artikel>();
        public Bestellposition(int id, int idbestellung, int idartikel, int anzahl) {
            this.id = id;
            this.idbestellung = idbestellung;
            this.idartikel = idartikel;
            this.anzahl = anzahl;
        }

        public int id { get; set; }
        public int idbestellung { get; set; }
        public int idartikel { get; set; }
        public int anzahl { get; set; }
    }
}
