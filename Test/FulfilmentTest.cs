using Service;
using System.Linq;
using Xunit;

namespace UnitTest
{
    public class FulfilmentTest
    {
        private readonly IFulfilmentOrder fulfilmentOrder;

        public FulfilmentTest()
        {
            this.fulfilmentOrder = new FulfilmentOrder();
        }

        [Fact]
        public void Orders_Awaiting_Fulfilment()
        {
            //Find orders waiting to be fulfilled
            Assert.True(fulfilmentOrder.GetOrdersByStatus(nameof(OrderStatusEnum.Pending)) != null);
        }

        [Fact]
        public void Orders_Have_Products()
        {
            //Orders have product included
            var hasProduct = fulfilmentOrder.GetOrdersByStatus(nameof(OrderStatusEnum.Pending))
                             .Products.Any();

            Assert.True(hasProduct);
        }

        [Fact]
        public void Order_Have_Stock_Available()
        {
            //Orders have stock available
            var orders = fulfilmentOrder.CalculateStockAvailability();

            var fulfilledOrders = orders.Orders.Where(o => o.Status == nameof(OrderStatusEnum.Fulfilled));

            Assert.True(fulfilledOrders.Select(o => o).Count() == 1);
        }

        [Fact]
        public void Submit_Fulfilment() //TO DO
        {
            //When I submit the fulfilment
            //Then I expect the order to be processed
            //And I expect that the order status is “Fulfilled”
            //And I expect that product quantity is updated
        }
    }
}