using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.Tests
{
    [TestClass]
    public class StylistTest : IDisposable
    {
        public void Dispose()
        {
          Client.ClearAll();
          Stylist.ClearAll();
          Specialty.ClearAll();
        }

        public StylistTest()
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
        public void Save_SavesStylistToDatabase_StylistList()
        {
            //Arrange
            Stylist testStylist = new Stylist("Freddy Mercury");

            //Act
            testStylist.Save();
            List<Stylist> databaseList = Stylist.GetAll();

            //Assert
            Assert.AreEqual(testStylist, databaseList[0]);
        }

        [TestMethod]
        public void Find_DatabaseFindsObjectById_StylistObject()
        {
            //Arrange
            Stylist testStylist = new Stylist ("Joan Jett");
            testStylist.Save();
            int testId = testStylist.Id;

            //Act
            Stylist result = Stylist.Find(testId);

            //Assert
            Assert.AreEqual(result, testStylist);
        }

        [TestMethod]
        public void Delete_DatabaseDeletesObject_0()
        {
            //Arrange
            Stylist testStylist = new Stylist ("Lady Gaga");
            testStylist.Save();

            //Act
            testStylist.Delete();
            List<Stylist> result = Stylist.GetAll();

            //Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetClients_ReturnsListOfClients_ClientList()
        {
            //Arrange
            Stylist testStylist = new Stylist ("Lady Gaga");
            testStylist.Save();
            Client clientOne = new Client ("Jones McGuffin", testStylist.Id);
            clientOne.Save();
            Client clientTwo = new Client ("Freda McGuffin", testStylist.Id);
            clientTwo.Save();

            //Act
            List<Client> testClients = testStylist.GetClients();
            List<Client> controlClients = new List<Client> {clientOne, clientTwo};

            //Assert
            Assert.AreEqual(controlClients.Count, testClients.Count);
        }
    }
}
