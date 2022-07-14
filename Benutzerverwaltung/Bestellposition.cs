using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benutzerverwaltung {

    

    public class Bestellposition {

        public Artikel artikel;
        public Bestellposition(int id, int idbestellung, int idartikel, int anzahl) {
            this.id = id;
            this.idbestellung = idbestellung;
            this.idartikel = idartikel;
            this.anzahl = anzahl;
        }

        public Bestellposition(string csvvv) {
            string[] einzeln = csvvv.Split(';');

            this.id = Convert.ToInt32(einzeln[0]);
            this.idbestellung = Convert.ToInt32(einzeln[1]);
            this.idartikel = Convert.ToInt32(einzeln[2]);
            this.anzahl = Convert.ToInt32(einzeln[3]);

        }

        public int id { get; set; }
        public int idbestellung { get; set; }
        public int idartikel { get; set; }
        public int anzahl { get; set; }

        public void assignArtikel(ObservableCollection<Artikel> best) {
            this.artikel = null;
            foreach (Artikel art in best) {
                if (art.id == this.idartikel) {
                    this.artikel = art;
                }
            }
        }
    }
}
