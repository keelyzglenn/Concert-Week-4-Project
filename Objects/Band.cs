using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Concert
{
    public class Band
    {
        private int _id;
        private string _name;

        public Band(string Name, int Id = 0)
        {
            _id = Id;
            _name = Name;
        }

        public override int GetHashCode()
        {
            return this.GetName().GetHashCode();
        }

        // id
        public int GetId()
        {
            return _id;
        }

        // name getter and setter
        public string GetName()
        {
            return _name;
        }

        public void SetName(string newName)
        {
            _name = newName;
        }

        // stops doubles between database and object
        public override bool Equals(System.Object otherBand)
        {
            if (!(otherBand is Band))
            {
                return false;
            }
            else
            {
                Band newBand = (Band) otherBand;
                bool idEquality = this.GetId() == newBand.GetId();
                bool nameEquality = this.GetName() == newBand.GetName();
                return (idEquality && nameEquality);
            }
        }

        public static List<Band> GetAll()
        {
            List<Book> AllBands = new List<Book>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM bands;", conn);

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int bandId = rdr.GetInt32(0);
                string bandName = rdr.GetString(1);

                Band newBand = new Band(bandName, bandId);
                AllBands.Add(newBand);
            }
            if (rdr != null)
            {
                rdr = Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return AllBands;
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM bands;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

    }
}
