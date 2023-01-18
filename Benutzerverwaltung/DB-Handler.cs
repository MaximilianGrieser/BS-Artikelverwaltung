using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using MySql.Data.MySqlClient;
using System.Text.Encodings;
namespace Benutzerverwaltung
{
    class DB_Handler
    {
        //CREATE DATABASE artikelverwaltung
        //CREATE TABLE kunden (ID int, LastName varchar(255), FirstName varchar(255), Birthdate DATE, City varchar(255))
        //CREATE TABLE Bestellung (ID int, kundenId int, datum Date, ausgeliefert Boolean)
        //CREATE TABLE Bestellposition (ID int, idbestellung int, idartikel int, anzahl int)
        //CREATE TABLE Artikel (ID int, gewicht DOUBLE, bestand int, bezeichnung text, preis DOUBLE)

        private MySqlConnection con;

        public DB_Handler()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            string connectionString = @"SERVER=localhost;DATABASE=artikelverwaltung;UID=root;Password=;convert zero datetime=True";
            this.con = new MySqlConnection(connectionString);
        }

        public void addKunde(string id, string nachname, string vorname, string geburtsdatum, string stadt)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO kunden (ID, LastName, FirstName, Birthdate, City) VALUES ('" + id + "','" + nachname + "','" + vorname + "','" + geburtsdatum + "','" + stadt + "')", this.con);

            cmd.Connection.Open();

            cmd.ExecuteNonQuery();

            this.con.Close();
        }

        public void addArtikel(int id, double gewicht, int bestand, string bezeichnung, double preis) {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO artikel (ID, gewicht, bestand, bezeichnung, preis) VALUES ('" + id.ToString() + "','" + gewicht.ToString() + "','" + bestand.ToString() + "','" + bezeichnung + "','" + preis.ToString() + "')", this.con);

            cmd.Connection.Open();

            cmd.ExecuteNonQuery();

            this.con.Close();
        }

        public void addbspos(int id, int idbestellung, int idartikel, int anzahl) {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO bestellposition (ID, idbestellung, idartikel, anzahl) VALUES ('" + id.ToString() + "','" + idbestellung.ToString() + "','" + idartikel.ToString() + "','" + anzahl.ToString() + "')", this.con);

            cmd.Connection.Open();

            cmd.ExecuteNonQuery();

            this.con.Close();
        }

        public void addbestellung(int id, int kundenId, string datum, bool ausgeliefert) {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO bestellung (ID, kundenId, datum, ausgeliefert) VALUES ('" + id.ToString() + "','" + kundenId.ToString() + "','" + datum.ToString() + "','" + ausgeliefert.ToString() + "')", this.con);

            cmd.Connection.Open();

            cmd.ExecuteNonQuery();

            this.con.Close();
        }

        public ObservableCollection<Kunde> getKunden()
        {
            ObservableCollection<Kunde> kunden = new ObservableCollection<Kunde>();
            con.Open();

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM kunden", con);
            MySqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                Kunde k = new Kunde(reader["id"].ToString(), reader["lastname"].ToString(), reader["firstname"].ToString(), reader["birthdate"].ToString(), reader["city"].ToString());
                kunden.Add(k);
            }

            con.Close();

            return kunden;
        }
        public ObservableCollection<Bestellung> getBestellungen() {
            ObservableCollection<Bestellung> bestellungen = new ObservableCollection<Bestellung>();
            con.Open();

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM bestellung", con);
            MySqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read()) {
                DateTime test;
                DateTime.TryParse(reader["datum"].ToString(), out test);
                //Trace.WriteLine(reader["datum"]);
                Bestellung k = new Bestellung(Convert.ToInt32(reader["id"].ToString()),
                                              Convert.ToInt32(reader["kundenId"].ToString()),
                                              test, 
                                              Convert.ToBoolean(reader["ausgeliefert"].ToString()));
                bestellungen.Add(k);
            }

            con.Close();

            return bestellungen;
        }
    }
}
