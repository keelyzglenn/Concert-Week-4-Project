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
            Band firstBand = new Band("Johnny Flynn");
            Band secondBand = new Band("Johnny Flynn");

            // Assert
            Assert.Equal(firstBand, secondBand);
        }


        public void Dispose()
        {
            Band.DeleteAll();
        }
      }
  }
