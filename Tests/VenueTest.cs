using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Concert
{
    public class VenueTest : IDisposable
    {
        public VenueTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_EqualOverrideTrueForSame()
        {
            // Arrange, Act
            Venue firstVenue = new Venue("Madison Square Garden");
            Venue secondVenue = new Venue("Madison Square Garden");

            // Assert
            Assert.Equal(firstVenue, secondVenue);
        }

        [Fact]
        public void Test_SaveAndGetAll_ListOfVenues()
        {
            // Arrange
            Venue newVenue = new Venue("Madison Square Garden");
            newVenue.Save();

            // Act
            List<Venue> result = Venue.GetAll();
            List<Venue> expectedResult = new List<Venue>{newVenue};

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Test_FindVenueIdInDatabase_band()
        {
            // Arrange
            Venue newVenue = new Venue("Madison Square Garden");
            newVenue.Save();

            // Act
            Venue result = Venue.Find(newVenue.GetId());

            // Assert
            Assert.Equal(newVenue, result);
        }


        [Fact]
        public void AddBand_AddsAndGetsBandToBand_ListBands()
        {
            // Arrange
            Band newBand = new Band("Drake");
            newBand.Save();

            Venue newVenue = new Venue("Madison Square Garden");
            newVenue.Save();

            // Act
            newVenue.AddBand(newBand);

            List<Band> result = newVenue.GetBands();
            List<Band> expectedResult = new List<Band>{newBand};

            // Arrange
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Test_Update_UpdateVenueInData()
        {
            // Arrange
            Venue oldVenue = new Venue("Madison Square Garden");
            oldVenue.Save();

            string newVenue = "The Knitting Factory";

            // Act
            oldVenue.Update(newVenue);

            string result = oldVenue.GetName();

            // Assert
            Assert.Equal(newVenue, result);
        }

        public void Dispose()
        {
            Venue.DeleteAll();
        }
      }
  }
