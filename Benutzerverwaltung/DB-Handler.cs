using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data.SqlClient;

namespace Benutzerverwaltung
{
    class DB_Handler
    {
        //CREATE TABLE kunden (ID int, LastName varchar(255), FirstName varchar(255), Birthdate DATE, City varchar(255))


        private SqlConnection con;

        public DB_Handler()
        {
            string connectionString = @"Data Source=db1516.mydbserver.com,1433;Initial Catalog=usr_p597408_4;User ID=p597408d1;Password=9#isC#uuc2pntd;";
            this.con = new SqlConnection(connectionString);
        }

        public void addKunde(string id, string nachname, string vorname, string geburtsdatum, string stadt)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO kunden (ID, LastName, FirstName, Birthdate, City) VALUES ('" + id + "','" + nachname + "','" + vorname + "','" + geburtsdatum + "','" + stadt + "',)", this.con);

            cmd.Connection.Open();

            cmd.ExecuteNonQuery();

            this.con.Close();
        }

        public ObservableCollection<Kunde> getKunden()
        {
            ObservableCollection<Kunde> kunden = new ObservableCollection<Kunde>();

            SqlCommand cmd = new SqlCommand("SELECT * FROM kunden", con);
            SqlDataReader reader = cmd.ExecuteReader();

            con.Open();

            while (reader.Read())
            {
                //kunden.Add(reader.getData());
            }

            con.Close();

            return kunden;
        }
    }
}
