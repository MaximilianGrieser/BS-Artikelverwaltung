using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using MySql.Data.MySqlClient;

namespace Benutzerverwaltung
{
    class DB_Handler
    {
        //CREATE TABLE kunden (ID int, LastName varchar(255), FirstName varchar(255), Birthdate DATE, City varchar(255))
        //CREATE TABLE Bestellung (ID int, kundenId int, datum Date, ausgeliefert Boolean)
        //CREATE TABLE Bestellposition (ID int, idbestellung int, idartikel int, anzahl int, artikelBez text)
        //CREATE TABLE Artikel (ID int, gewicht DOUBLE, bestand int, bezeichnung text, preis DOUBLE)

        private MySqlConnection con;

        public DB_Handler()
        {
            string connectionString = @"SERVER=localhost;DATABASE=artikelverwaltung;UID=root;Password=;";
            this.con = new MySqlConnection(connectionString);
        }

        public void addKunde(string id, string nachname, string vorname, string geburtsdatum, string stadt)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO kunden (ID, LastName, FirstName, Birthdate, City) VALUES ('" + id + "','" + nachname + "','" + vorname + "','" + geburtsdatum + "','" + stadt + "',)", this.con);

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
                //kunden.Add(reader.getData());
            }

            con.Close();

            return kunden;
        }
    }
}
