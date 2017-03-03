using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Concert
{
    public class BookTest : IDisposable
    {
        public BookTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_EqualOverrideTrueForSame()
        {
            // Arrange, Act
            Band firstBand = new Band("Drake");
            Band secondBand = new Band("Drake");

            // Assert
            Assert.Equal(firstBand, secondBand);
        }

        [Fact]
        public void Test_SaveAndGetAll_ListOfBands()
        {
            // Arrange
            Band newBand = new Band("Drake");
            newBand.Save();

            // Act
            List<Band> result = Band.GetAll();
            List<Band> expectedResult = new List<Band>{newBand};

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Test_FindBandIdInDatabase_band()
        {
            // Arrange
            Band newBand = new Band("Drake");
            newBand.Save();

            // Act
            Band result = Band.Find(newBand.GetId());

            // Assert
            Assert.Equal(newBand, result)
        }



        public void Dispose()
        {
            Band.DeleteAll();
        }
      }
  }
