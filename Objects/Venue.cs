using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Concert
{
    public class Venue
    {
        private int _id;
        private string _name;

        public Venue(string Name, int Id = 0)
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
        public override bool Equals(System.Object otherVenue)
        {
            if (!(otherVenue is Venue))
            {
                return false;
            }
            else
            {
                Venue newVenue = (Venue) otherVenue;
                bool idEquality = this.GetId() == newVenue.GetId();
                bool nameEquality = this.GetName() == newVenue.GetName();
                return (idEquality && nameEquality);
            }
        }

        public static List<Venue> GetAll()
        {
            List<Venue> AllVenues = new List<Venue>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM venues;", conn);

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int venueId = rdr.GetInt32(0);
                string venueName = rdr.GetString(1);

                Venue newVenue = new Venue(venueName, venueId);
                AllVenues.Add(newVenue);
            }
            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return AllVenues;
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO venues (name) OUTPUT INSERTED.id VALUES (@VenueName);", conn);

            SqlParameter venueName = new SqlParameter();
            venueName.ParameterName = "@VenueName";
            venueName.Value = this.GetName();

            cmd.Parameters.Add(venueName);

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }
            if(rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
        }
        //
        // public static Venue Find(int id)
        // {
        //     SqlConnection conn = DB.Connection();
        //     conn.Open();
        //
        //     SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE id = @VenueId;", conn);
        //
        //     SqlParameter venueId = new SqlParameter();
        //     venueId.ParameterName = "@VenueId";
        //     venueId.Value = id.ToString();
        //     cmd.Parameters.Add(venueId);
        //
        //     SqlDataReader rdr = cmd.ExecuteReader();
        //
        //     int foundVenueId = 0;
        //     string foundVenueName = null;
        //
        //     while(rdr.Read())
        //     {
        //         foundVenueId = rdr.GetInt32(0);
        //         foundVenueName = rdr.GetString(1);
        //     }
        //     Venue foundVenue = new Venue(foundVenueName, foundVenueId);
        //
        //     if (rdr != null)
        //     {
        //         rdr.Close();
        //     }
        //     if (conn != null)
        //     {
        //         conn.Close();
        //     }
        //
        //     return foundVenue;
        // }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM venues;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

    }
}
