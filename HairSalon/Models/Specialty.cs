using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
    public class Specialty
    {
//VARIALBLES & CONSTRUCTOR
        public int Id {get; set; }
        public string Name {get; set; }

        public Specialty(string name, int id = 0)
        {
            this.Name = name;
            this.Id = id;
        }

//OVERRIDES
        public override bool Equals(System.Object otherSpecialty)
        {
            if(!(otherSpecialty is Specialty))
            {
                return false;
            }
            else
            {
                Specialty newSpecialty = (Specialty) otherSpecialty;
                bool areNamesEqual = this.Name.Equals(newSpecialty.Name);
                bool areIdsEqual = this.Id.Equals(newSpecialty.Id);
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
            cmd.CommandText = @"INSERT INTO specialties (specialty_name) VALUES (@specialtyName);";

            cmd.Parameters.Add(new MySqlParameter("@specialtyName", this.Name));

            cmd.ExecuteNonQuery();
            this.Id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

//READ
        public static Specialty Find(int specialtyId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialties WHERE specialty_id = @specialtyId;";

            cmd.Parameters.Add(new MySqlParameter("@specialtyId", specialtyId));

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int id = 0;
            string name = "";
            while (rdr.Read())
            {
                id = rdr.GetInt32(0);
                name = rdr.GetString(1);
            }
            Specialty foundSpecialty = new Specialty(name, id);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundSpecialty;
        }

        public static List<Specialty> GetAll()
        {
            List<Specialty> allSpecialties = new List<Specialty> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialties;";

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                allSpecialties.Add(new Specialty(name, id));
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allSpecialties;
        }

        public List<Stylist> GetStylists()
        {
            List<Stylist> itsStylists = new List<Stylist> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT stylists.* FROM specialties
                JOIN stylists_specialties ON (specialties.specialty_id = stylists_specialties.specialty_id)
                JOIN stylists ON (stylists_specialties.stylist_id = stylists.stylist_id)
                WHERE specialties.specialty_id = @thisId;";

            cmd.Parameters.AddWithValue("@thisId", this.Id);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                itsStylists.Add(new Stylist(name, id));
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return itsStylists;
        }


//UPDATE
//DELETE
        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE from specialties;";

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

    }
}
