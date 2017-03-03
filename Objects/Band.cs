using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Concert
{
    public class Band
    {
        private int _id;
        private string _name;

        public Band(string Name, int Id = 0);
        {
            _id = Id;
            _name = Name;
        }

        public override int GetHashCode()
        {
            return this.GetName().GetHashCode();
        }

        // id
        public bool GetId()
        {
            return _id;
        }

        // name getter and setter
        public bool GetName()
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
                Bnad newBand = (Band) otherBand;
                bool idEquality = this.GetId() == newBand.GetId();
                bool nameEquality = this.GetName() == newBand.GetName();
                return (idEquality && nameEquality);
            }
        }


    }
}
