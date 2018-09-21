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
            Stylist.ClearAll();
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
            int testId = testStylist.GetStylistId();

            //Act
            Stylist result = Stylist.Find(testId);

            //Assert
            Assert.AreEqual(result, testStylist);
        }
    }
}
