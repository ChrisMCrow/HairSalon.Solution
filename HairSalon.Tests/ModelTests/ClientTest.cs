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
            Stylist firstStylist = new Stylist("Jamie Maddrox");
            Stylist secondStylist = new Stylist("Jamie Maddrox");

            //Assert
            Assert.AreEqual(firstStylist, secondStylist);
        }

        [TestMethod]
        public void Save_SavesClientToDatabase_ClientList()
        {
            //Arrange
            Client testClient = new Client("Bobby Drake", 1);

            //Act
            testClient.Save();
            List<Client> resultList = Client.GetAll();

            //Assert
            Assert.AreEqual(testClient, resultList[0]);
        }

        [TestMethod]
        public void Find_DatabaseFindsObjectById_ClientObject()
        {
            //Arrange
            Client testClient = new Client ("Jean Grey", 1);
            testClient.Save();

            //Act
            Client result = Client.Find(testClient.Id);

            //Assert
            Assert.AreEqual(result, testClient);
        }

        [TestMethod]
        public void Update_NameInDatabase_NewClient()
        {
            //Arrange
            Client oldClient = new Client("Scott Summers", 1);
            oldClient.Save();
            Client newClient = new Client("Cyclops", 1, oldClient.Id);

            //Act
            oldClient.Update("Cyclops");
            Client result = Client.Find(oldClient.Id);

            //Assert
            Assert.AreEqual(newClient, result);
        }

        [TestMethod]
        public void Delete_DatabaseDeletesObject_0()
        {
            //Arrange
            Stylist testStylist = new Stylist("Logan");
            testStylist.Save();

            //Act
            testStylist.Delete();
            List<Stylist> result = Stylist.GetAll();

            //Assert
            Assert.AreEqual(0, result.Count);
        }
    }
}
