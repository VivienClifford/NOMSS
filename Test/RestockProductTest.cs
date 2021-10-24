using Service;
using System.Linq;
using Xunit;

namespace Test
{
    public class RestockProductTest
    {
        private readonly IRestockProduct restockOrder;

        public RestockProductTest()
        {
            this.restockOrder = new RestockProduct();
        }

        [Fact]
        public static void Calculate_Product_Restock_Threshold_Reached()
        {
        }

        [Fact]
        public void Product_Can_Be_Restocked()
        {
            var restock = restockOrder.RestockProducts();
            Assert.True(restock.All(r => r.NeedRestock == true));
        }
    }
}