using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
    public class Stylist
    {
//VARIABLES & CONSTRUCTOR
        public int Id {get; set; }
        public string Name {get; set; }

        public Stylist(string name, int id = 0)
        {
            this.Name = name;
            this.Id = id;
        }

//OVERRIDES
        public override bool Equals(System.Object otherStylist)
        {
            if(!(otherStylist is Stylist))
            {
                return false;
            }
            else
            {
                Stylist newStylist = (Stylist) otherStylist;
                bool areNamesEqual = this.Name.Equals(newStylist.Name);
                bool areIdsEqual = this.Id.Equals(newStylist.Id);
                return (areNamesEqual && areIdsEqual);
            }
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("{{id = {0}, name = {1}}}", Id, Name);
        }

//CREATE
        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists (stylist_name) VALUES (@stylistName);";

            cmd.Parameters.Add(new MySqlParameter("@stylistName", this.Name));

            cmd.ExecuteNonQuery();
            this.Id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

//READ
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

        public List<Client> GetClients()
        {
            List<Client> theirClients = new List<Client> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = @stylistId;";

            cmd.Parameters.Add(new MySqlParameter("@stylistId", this.Id));

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                int stylistId = rdr.GetInt32(2);
                theirClients.Add(new Client(name, stylistId, id));
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return theirClients;
        }

        public List<Specialty> GetSpecialties()
        {
            List<Specialty> theirSpecialties = new List<Specialty> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT specialties.* FROM stylists
                JOIN stylists_specialties ON (stylists.stylist_id = stylists_specialties.stylist_id)
                JOIN specialties ON (stylists_specialties.specialty_id = specialties.specialty_id)
                WHERE stylists.stylist_id = @thisId;";

            cmd.Parameters.AddWithValue("@thisId", this.Id);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                theirSpecialties.Add(new Specialty(name, id));
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return theirSpecialties;
        }


//UPDATE
        public void Update(string name)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE stylists SET stylist_name = @newName WHERE stylist_id = @thisId;";

            cmd.Parameters.AddWithValue("@newName", name);
            cmd.Parameters.AddWithValue("@thisId", this.Id);
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void AddSpecialty(int stylistId, int specialtyId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists_specialties (stylist_id, specialty_id) VALUES (@stylistId, @specialtyId);";

            cmd.Parameters.AddWithValue("@stylistId", stylistId);
            cmd.Parameters.AddWithValue("@specialtyId", specialtyId);
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void RemoveSpecialty(int stylistId, int specialtyId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM stylists_specialties WHERE stylist_id = @stylistId AND specialty_id = @specialtyId;";

            cmd.Parameters.AddWithValue("@stylistId", stylistId);
            cmd.Parameters.AddWithValue("@specialtyId", specialtyId);
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

//DELETE
        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"
            DELETE FROM stylists WHERE stylist_id = @stylistId;
            DELETE FROM stylists_specialties WHERE stylist_id = @stylistId;
            DELETE FROM clients WHERE stylist_id = @stylistId;";

            cmd.Parameters.Add(new MySqlParameter("@stylistId", this.Id));

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
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
    }
}
