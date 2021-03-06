using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;

namespace HairSalon.Tests
{
    [TestClass]
    public class StylistControllerTest
    {
        [TestMethod]
        public void Details_ReturnsCorrectView_True()
        {
            //Arrange
            StylistController controller = new StylistController();
            Stylist testStylist = new Stylist("Bruno Mars");
            testStylist.Save();

            //Act
            ActionResult detailsView = controller.Details(testStylist.Id);

            //Assert
            Assert.IsInstanceOfType(detailsView, typeof(ViewResult));
        }
    }
}
