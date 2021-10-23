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
            Assert.True(fulfilmentOrder.GetOrdersByStatus(nameof(OrderStatusEnum.Pending)).Any());
        }

        [Fact]
        public void Orders_Have_Products()
        {
            //Orders have product included
            var hasProduct = fulfilmentOrder.GetOrdersByStatus(nameof(OrderStatusEnum.Pending)).Select(o => o.Products).Any();

            Assert.True(hasProduct);
        }

        [Fact]
        public void Order_Have_Stock_Available()
        {
            //Orders have stock available
            fulfilmentOrder.CalculateStockAvailability();
        }
       
        private void Test()
        {
            throw new NotImplementedException();
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
