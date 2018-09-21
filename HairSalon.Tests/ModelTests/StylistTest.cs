using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
    }
}
