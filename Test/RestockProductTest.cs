using Service;
using System.Linq;
using Xunit;

namespace Test
{
    public class RestockProductTest
    {
        private readonly IFulfilmentOrder _fulfilmentOrder;
        private readonly IRestockProduct _restockOrder;

        public RestockProductTest()
        {
            this._restockOrder = new RestockProduct();
            this._fulfilmentOrder = new FulfilmentOrder();
        }

        [Fact]
        public void Product_Can_Be_Restocked()
        {
            var restock = _restockOrder.RestockProducts();
            Assert.True(restock.All(r => r.NeedRestock == true));
        }
    }
}