﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benutzerverwaltung {
    public class Artikel {
        public Artikel(int id, int gewicht, int bestand, string bezeichnung, double preis) {
            this.id = id;
            this.gewicht = gewicht;
            this.bestand = bestand;
            this.bezeichnung = bezeichnung;
            this.preis = preis;
        }

        public int id { get; set; }
        public int gewicht { get; set; }
        public int bestand { get; set; }
        public string bezeichnung { get; set; }
        public double preis { get; set; }

        public double getLagerwert() {
            return this.preis * this.bestand;
        }
    }
}
