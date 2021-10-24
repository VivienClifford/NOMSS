using Service;
using Service.Models;
using System.Linq;
using Xunit;

namespace Test
{
    public class FulfilmentTest
    {
        private readonly IFulfilmentOrder _fulfilmentOrder;

        public FulfilmentTest()
        {
            this._fulfilmentOrder = new FulfilmentOrder();
        }

        [Fact]
        public void Orders_Awaiting_Fulfilment()
        {
            //Find orders waiting to be fulfilled
            Assert.True(_fulfilmentOrder.GetOrdersByStatus(nameof(OrderStatusEnum.Pending)) != null);
        }

        [Fact]
        public void Orders_Have_Products()
        {
            //Orders have product included
            var hasProduct = _fulfilmentOrder.GetOrdersByStatus(nameof(OrderStatusEnum.Pending))
                             .Products.Any();

            Assert.True(hasProduct);
        }

        [Fact]
        public void Order_Have_Stock_Available()
        {
            //Orders have stock available
            var orders = _fulfilmentOrder.CalculateStockAvailability();

            var fulfilledOrders = orders.Orders.Where(o => o.Status == nameof(OrderStatusEnum.Fulfilled));

            Assert.True(fulfilledOrders.Select(o => o).Count() == 1);
        }

        #region "Main test"
        [Fact]
        public void Submit_Fulfilment()
        {
            //When I submit the fulfilment
            //Then I expect the order to be processed
            var originalOrder = _fulfilmentOrder.GetOrdersByStatus(nameof(OrderStatusEnum.Pending));
            Order_Processed(originalOrder);
            
            //And I expect that the order status is “Fulfilled”
            var updateStock = _fulfilmentOrder.CalculateStockAvailability();
            Order_Status_Updated(updateStock);

            //And I expect that product quantity is updated
            Compare_Product_Quantity(originalOrder, updateStock);
        }

        private static bool Order_Processed(OrderProduct originalOrder)
        {
            
            var originalOrderHasOrder = originalOrder.Orders.Where(o => o.Status == nameof(OrderStatusEnum.Pending));

            bool checkOrder = originalOrder.Orders.Count() == originalOrderHasOrder.Count();
            
            Assert.True(checkOrder);

            return checkOrder;
        }
        
        private static bool Order_Status_Updated(OrderProduct updateStock)
        {
            var pending = updateStock.Orders.Where(o => o.Status == nameof(OrderStatusEnum.Pending));
            var fulfilledOrders = updateStock.Orders.Where(o => o.Status.Contains(nameof(OrderStatusEnum.Fulfilled)));

            bool checkOrder = (pending.Count() == 0 && fulfilledOrders.Count() >= 1);

            Assert.True(checkOrder);

            return checkOrder;
        }

        private static bool Compare_Product_Quantity(OrderProduct originalOrder, OrderProduct updateStock)
        {
            var ogProducts = originalOrder.Products;
            var upProducts = updateStock.Products;

            var compare = ogProducts.Join(upProducts,
                                og => og.ProductId,
                                up => up.ProductId,
                                (og, up) =>
                                new
                                {
                                    OriginalProductId = og.ProductId,
                                    OriginalQuantityOnHand = og.QuantityOnHand,
                                    UpdatedProductId = up.ProductId,
                                    UpdatedQuantityOnHand = up.QuantityOnHand,
                                    IsQuantitySame = og.QuantityOnHand == up.QuantityOnHand,
                                });

            bool hasSameQuantity = compare.Any(p => p.IsQuantitySame && p.OriginalQuantityOnHand != 0);

            Assert.False(hasSameQuantity);

            return hasSameQuantity;
        }
        #endregion
    }
}