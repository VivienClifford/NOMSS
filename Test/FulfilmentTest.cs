using System;
using Xunit;
using Service;
using System.Linq;
using Service.Models;
using System.Collections.Generic;

namespace UnitTest
{
    public class FulfilmentTest
    {
        private readonly IFulfilmentOrder fulfilmentOrder = new FulfilmentOrder();

        [Fact]
        public void Orders_Awaiting_Fulfilment()
        {
            //Find orders waiting to be fulfilled
            Assert.True(fulfilmentOrder.GetOrdersByStatus("pending").Any());
        }

        [Fact]
        public void Orders_Have_Products()
        {
            //Orders have product included
            var hasProduct = fulfilmentOrder.GetOrdersByStatus("pending").Select(o => o.Products).Any();

            Assert.True(hasProduct);
        }

        private IEnumerable<OrderProduct> CalculateStockAvailability()
        {
            var pendingOrders = fulfilmentOrder.GetOrdersByStatus("pending");
            var orders = pendingOrders.Select(o => o.Orders); //Where(o => o.Orders.Select(or => or.Items))
            var products = pendingOrders.Select(o => o.Products);

            return pendingOrders;
        }

        [Fact]
        public void Order_Have_Stock_Available()
        {
            var available = 

            //Orders have stock available
        }

        [Fact]
        public void Submit_Fulfilment()
        {
            //When I submit the fulfilment
            //Then I expect the order to be processed
            //And I expect that the order status is “Fulfilled”
            //And I expect that product quantity is updated
        }


    }
}
