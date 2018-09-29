using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
    public class Client
    {
        public int Id {get; set; }
        public string Name {get; set; }
        public int StylistId {get; set; }

        public Client(string clientName, int stylistId, int clientId = 0)
        {
            this.Id = clientId;
            this.Name = clientName;
            this.StylistId = stylistId;
        }

        public override bool Equals(System.Object otherClient)
        {
            if(!(otherClient is Client))
            {
                return false;
            }
            else
            {
                Client newClient = (Client) otherClient;
                bool areNamesEqual = this.Name.Equals(newClient.Name);
                bool areIdsEqual = this.Id.Equals(newClient.Id);
                return (areNamesEqual && areIdsEqual);
            }
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("{{id = {0}, name = {1}, stylistId = {2}}}", Id, Name, StylistId);
        }


//CREATE
        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO clients (client_name, stylist_id) VALUES (@clientName, @stylistId);";

            cmd.Parameters.Add(new MySqlParameter("@clientName", this.Name));
            cmd.Parameters.Add(new MySqlParameter("@stylistId", this.StylistId));

            cmd.ExecuteNonQuery();
            this.Id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

//READ
        public static Client Find(int clientId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients WHERE client_id = @clientId;";

            cmd.Parameters.Add(new MySqlParameter("@clientId", clientId));

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int id = 0;
            string name = "";
            int stylistId = 0;
            while (rdr.Read())
            {
                id = rdr.GetInt32(0);
                name = rdr.GetString(1);
                stylistId = rdr.GetInt32(2);
            }
            Client foundClient = new Client(name, stylistId, id);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return foundClient;
        }


        public static List<Client> GetAll()
        {
            List<Client> allClients = new List<Client> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients ORDER BY client_name ASC;";

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                int stylistId = rdr.GetInt32(2);
                allClients.Add(new Client(name, stylistId, id));
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allClients;
        }

//UPDATE
        public void Update(string name, int stylistId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE clients SET client_name = @newName, stylist_id = @newId WHERE client_id = @thisId;";

            cmd.Parameters.AddWithValue("@newName", name);
            cmd.Parameters.AddWithValue("@newId", stylistId);
            cmd.Parameters.AddWithValue("@thisId", this.Id);
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
            DELETE FROM clients WHERE client_id = @clientId;";

            cmd.Parameters.Add(new MySqlParameter("@clientId", this.Id));

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
          cmd.CommandText = @"DELETE from clients;";

          cmd.ExecuteNonQuery();
          conn.Close();
          if (conn != null)
          {
              conn.Dispose();
          }
        }
    }
}
