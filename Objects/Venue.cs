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

        public static Venue Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE id = @VenueId;", conn);

            SqlParameter venueId = new SqlParameter();
            venueId.ParameterName = "@VenueId";
            venueId.Value = id.ToString();
            cmd.Parameters.Add(venueId);

            SqlDataReader rdr = cmd.ExecuteReader();

            int foundVenueId = 0;
            string foundVenueName = null;

            while(rdr.Read())
            {
                foundVenueId = rdr.GetInt32(0);
                foundVenueName = rdr.GetString(1);
            }
            Venue foundVenue = new Venue(foundVenueName, foundVenueId);

            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }

            return foundVenue;
        }

        public void AddBand(Band newBand)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO bands_venues (band_id, venue_id) VALUES (@BandId, @VenueId);", conn);

            SqlParameter bandId = new SqlParameter();
            bandId.ParameterName = "@BandId";
            bandId.Value = newBand.GetId();
            cmd.Parameters.Add(bandId);

            SqlParameter venueId = new SqlParameter();
            venueId.ParameterName = "@VenueId";
            venueId.Value = this.GetId();
            cmd.Parameters.Add(venueId);

            cmd.ExecuteNonQuery();

            if (conn != null)
            {
                conn.Close();
            }
        }

        public List<Band> GetBands()
        {
            List<Band> bands = new List<Band>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT bands.* FROM venues JOIN bands_venues ON (venues.id = bands_venues.venue_id) JOIN bands ON (bands.id = bands_venues.band_id) WHERE venues.id = @VenueId;", conn);

            SqlParameter venueId = new SqlParameter();
            venueId.ParameterName = "@VenueId";
            venueId.Value = this.GetId().ToString();
            cmd.Parameters.Add(venueId);

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int bandId = rdr.GetInt32(0);
                string bandName = rdr.GetString(1);

                Band newBand = new Band(bandName, bandId);
                bands.Add(newBand);
            }

            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return bands;
        }

        public void Update(string newVenueName)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE venues SET name = @NewVenueName OUTPUT INSERTED.name WHERE id = @VenueId;", conn);

            SqlParameter newName = new SqlParameter();
            newName.ParameterName = "@NewVenueName";
            newName.Value = newVenueName;
            cmd.Parameters.Add(newName);

            SqlParameter venueId = new SqlParameter();
            venueId.ParameterName = "@VenueId";
            venueId.Value = this.GetId();
            cmd.Parameters.Add(venueId);

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._name = rdr.GetString(0);
            }

            if (rdr !=null)
            {
                rdr.Close();
            }

            if (conn != null)
            {
                conn.Close();
            }
        }


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
