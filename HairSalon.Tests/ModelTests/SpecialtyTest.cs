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
            Specialty firstSpecialty = new Specialty("Lasers");
            Specialty secondSpecialty = new Specialty("Lasers");

            //Assert
            Assert.AreEqual(firstSpecialty, secondSpecialty);
        }

        [TestMethod]
        public void Save_SavesSpecialtyToDatabase_SpecialtyList()
        {
            //Arrange
            Specialty testSpecialty = new Specialty("Pyros", 1);

            //Act
            testSpecialty.Save();
            List<Specialty> resultList = Specialty.GetAll();

            //Assert
            Assert.AreEqual(testSpecialty, resultList[0]);
        }

        [TestMethod]
        public void Find_DatabaseFindsObjectById_StylistObject()
        {
            //Arrange
            Specialty testSpecialty = new Specialty("Invlunerability");
            testSpecialty.Save();
            int testId = testSpecialty.Id;

            //Act
            Specialty result = Specialty.Find(testId);

            //Assert
            Assert.AreEqual(result, testSpecialty);
        }

        [TestMethod]
        public void GetStylists_ReturnsSpecialtysStylists_StylistList()
        {
            //Arrange
            Specialty testSpecialty = new Specialty("Ninja Moves");
            testSpecialty.Save();
            Stylist testStylist = new Stylist("Adam Levine");
            testStylist.Save();

            //Act
            Stylist.AddSpecialty(testStylist.Id, testSpecialty.Id);
            List<Stylist> result = testSpecialty.GetStylists();

            //Assert
            Assert.AreEqual(testStylist, result[0]);
        }

        [TestMethod]
        public void Update_NameInDatabase_NewStylist()
        {
            //Arrange
            Specialty oldSpecialty = new Specialty("Telepathy");
            oldSpecialty.Save();
            Specialty newSpecialty = new Specialty("Diamond Skin", oldSpecialty.Id);

            //Act
            oldSpecialty.Update("Diamond Skin");
            Specialty result = Specialty.Find(oldSpecialty.Id);

            //Assert
            Assert.AreEqual(newSpecialty, result);
        }

        [TestMethod]
        public void Delete_DeletesObject_0()
        {
            //Arrange
            Specialty testSpecialty = new Specialty("Flight");
            testSpecialty.Save();

            //Act
            testSpecialty.Delete();
            List<Specialty> result = Specialty.GetAll();

            //Assert
            Assert.AreEqual(0, result.Count);
        }
    }
}
