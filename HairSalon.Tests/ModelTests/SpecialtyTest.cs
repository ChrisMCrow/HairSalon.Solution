using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.Tests
{
    [TestClass]
    public class SpecialtyTest : IDisposable
    {
        public void Dispose()
        {
            Client.ClearAll();
            Stylist.ClearAll();
            Specialty.ClearAll();
        }

        public SpecialtyTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=chris_crow_test;";
        }

        [TestMethod]
        public void Equals_ReturnsTrueForSameNameAndId_True()
        {
            //Arrange, Act
            Specialty firstSpecialty = new Specialty("Hair");
            Specialty secondSpecialty = new Specialty("Hair");

            //Assert
            Assert.AreEqual(firstSpecialty, secondSpecialty);
        }

        [TestMethod]
        public void Save_SavesSpecialtyToDatabase_SpecialtyList()
        {
            //Arrange
            Specialty testSpecialty = new Specialty("Paul McGuire", 1);

            //Act
            testSpecialty.Save();
            List<Specialty> resultList = Specialty.GetAll();

            //Assert
            Assert.AreEqual(testSpecialty, resultList[0]);
        }
    }
}
