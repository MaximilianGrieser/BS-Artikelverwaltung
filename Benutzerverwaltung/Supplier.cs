using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.IO;

namespace Benutzerverwaltung {
    public class Supplier {
        public ObservableCollection<Kunde> kunden = new ObservableCollection<Kunde>();
        
        public Supplier() {
            this.kunden = this.getKunden();
        }

        public ObservableCollection<Kunde> getKunden() {
            this.kunden.Clear();
            string[] s = File.ReadAllLines("kunden.csv");
            for(int i = 1; i < s.Length; i++) {
                kunden.Add(new Kunde(s[i]));
            }

            return kunden;
        }

    }
}
