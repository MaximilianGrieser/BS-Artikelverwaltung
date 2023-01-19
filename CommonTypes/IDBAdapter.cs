using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Benutzerverwaltung;

namespace CommonTypes {
    public interface IDBAdapter {

        void connect();

        ObservableCollection<Kunde> getKunden();

        ObservableCollection<Bestellung> getBestellungen();

        ObservableCollection<Artikel> getArtikel();

        ObservableCollection<Bestellposition> getBestellposition();

        void addArtikel(int id, double gewicht, int bestand, string bezeichnung, double preis);

        void addKunde(string id, string nachname, string vorname, string geburtsdatum, string stadt);

        void addBspos(int id, int idbestellung, int idartikel, int anzahl);

        void addBestellung(int id, int kundenId, string datum, bool ausgeliefert);

    }
}
