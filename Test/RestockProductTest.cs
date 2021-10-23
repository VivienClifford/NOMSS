using Service;
using Service.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Test
{
    public class RestockProductTest
    {
        private readonly IRestockProduct restockOrder = new RestockProduct();

        public static void Calculate_Product_Restock_Threshold_Reached()
        {

        }

        public void Product_Can_Be_Restocked()
        {
            var restock = restockOrder.RestockProducts();
            Assert.True(restock.All(r => r.NeedRestock == true));
        }

    }
}
