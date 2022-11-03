using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;



namespace MusicfyAPI.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetAllAlbums()
        {
            var testProducts = GetTestProducts();
            var controller = new SimpleProductController(testProducts);

            var result = controller.GetAllProducts() as List<Product>;
            Assert.AreEqual(testProducts.Count, result.Count);
        }
    }
}
