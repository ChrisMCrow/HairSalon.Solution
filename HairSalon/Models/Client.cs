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

        public static List<Client> GetAll()
        {
            List<Client> allClients = new List<Client> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients;";

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
    }
}
