using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
    public class Client
    {
        private int _clientId;
        private string _clientName;
        private int _stylistId;

        public Client(string clientName, int stylistId, int clientId = 0)
        {
            _clientId = clientId;
            _clientName = clientName;
            _stylistId = stylistId;
        }

        public int GetClientId()
        {
            return _clientId;
        }

        public string GetClientName()
        {
            return _clientName;
        }

        public int GetStylistId()
        {
            return _stylistId;
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
                bool areNamesEqual = this.GetClientName().Equals(newClient.GetClientName());
                bool areIdsEqual = this.GetClientId().Equals(newClient.GetClientId());
                return (areNamesEqual && areIdsEqual);
            }
        }

        public override int GetHashCode()
        {
            return this.GetClientName().GetHashCode();
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

            cmd.Parameters.Add(new MySqlParameter("@clientName", _clientName));
            cmd.Parameters.Add(new MySqlParameter("@stylistId", _stylistId));

            cmd.ExecuteNonQuery();
            _clientId = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
