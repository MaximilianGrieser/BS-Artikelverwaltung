﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klassen_und_OOP {
    class Kunde {
        public Kunde(string zeile) {
            string[] item = zeile.Split(';');
            this.id = Convert.ToInt32(item[0]);
            this.nachname = item[1];
            this.vorname = item[2];
            this.geburtsdatum = Convert.ToDateTime(item[3]);
            this.stadt = item[4];
        }

        public int id { get; set; }
        public string nachname { get; set; }
        public string vorname { get; set; }
        public DateTime geburtsdatum { get; set; }
        public string stadt { get; set; }


    }
}