using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
    public class Stylist
    {
        private int _stylistId;
        private string _stylistName;

        public Stylist(string name, int id = 0)
        {
            _stylistName = name;
            _stylistId = id;
        }

        public int GetStylistId()
        {
            return _stylistId;
        }
        public string GetStylistName()
        {
            return _stylistName;
        }

        public override bool Equals(System.Object otherStylist)
        {
            if(!(otherStylist is Stylist))
            {
                return false;
            }
            else
            {
                Stylist newStylist = (Stylist) otherStylist;
                bool areNamesEqual = this.GetStylistName().Equals(newStylist.GetStylistName());
                bool areIdsEqual = this.GetStylistId().Equals(newStylist.GetStylistId());
                return (areNamesEqual && areIdsEqual);
            }
        }

        public override int GetHashCode()
        {
            return this.GetStylistName().GetHashCode();
        }

        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE from stylists;";

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists (stylist_name) VALUES (@stylistName);";

            cmd.Parameters.Add(new MySqlParameter("@stylistName", _stylistName));

            cmd.ExecuteNonQuery();
            _stylistId = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Stylist> GetAll()
        {
            List<Stylist> allStylists = new List<Stylist> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists;";

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                Stylist newStylist = new Stylist(name, id);
                allStylists.Add(newStylist);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allStylists;
        }

        public static Stylist Find(int stylistId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists WHERE stylist_id = @stylistId;";

            cmd.Parameters.Add(new MySqlParameter("@stylistId", stylistId));

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int id = 0;
            string name = "";
            while (rdr.Read())
            {
                id = rdr.GetInt32(0);
                name = rdr.GetString(1);
            }
            Stylist foundStylist = new Stylist(name, id);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return foundStylist;
        }

        public List<Client> GetClients()
        {
            List<Client> theirClients = new List<Client> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = @stylistId;";

            cmd.Parameters.Add(new MySqlParameter("@stylistId", _stylistId));

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                int stylistID = rdr.GetInt32(2);
                theirClients.Add(new Client(name, stylistId, id))
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return theirClients;
        }
    }
}
