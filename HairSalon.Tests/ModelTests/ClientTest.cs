using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.Tests
{
    [TestClass]
    public class ClientTest : IDisposable
    {
        public void Dispose()
        {
          Client.ClearAll();
          Stylist.ClearAll();
          Specialty.ClearAll();
        }

        public ClientTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=chris_crow_test;";
        }

        [TestMethod]
        public void Equals_ReturnsTrueForSameNameAndId_True()
        {
            //Arrange, Act
            Stylist firstStylist = new Stylist("Jane Doe");
            Stylist secondStylist = new Stylist("Jane Doe");

            //Assert
            Assert.AreEqual(firstStylist, secondStylist);
        }

        [TestMethod]
        public void Save_SavesClientToDatabase_ClientList()
        {
            //Arrange
            Client testClient = new Client("Paul McGuire", 1);

            //Act
            testClient.Save();
            List<Client> resultList = Client.GetAll();

            //Assert
            Assert.AreEqual(testClient, resultList[0]);
        }
    }
}
