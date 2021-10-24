using API.Helper;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    [ApiController]
    public class WarehouseController : Controller
    {
        private readonly IFulfilmentOrder _fulfilmentOrder;
        private readonly IRestockProduct _restockProduct;

        public WarehouseController()
        {
            this._fulfilmentOrder = new FulfilmentOrder();
            this._restockProduct = new RestockProduct();
        }

        /// <summary>
        /// Order fulfilment process to determine which orders aren't able to be fulfilled
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/v1/warehouse/fulfilment")]
        public Dictionary<string, List<int>> PostWarehouseFulfilment([FromBody] RequestWarehouseFulfilment request)
        {
            if (request.OrderIds == null)
                throw new Exception($"No {nameof(request.OrderIds)} passed in the body of the request"); //Test for other data types

            var orderProducts = _fulfilmentOrder.CalculateStockAvailability(request.OrderIds);

            return UnfulfillableOrder.GetUnfulfillableOrders(orderProducts);
        }

        /// <summary>
        /// Retrieves the products which need to be restocked
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/v1/warehouse/products/restock")]
        public IEnumerable<ResponsePurchaseOrder> GetWarehouseRestockProduct()
        {
            var products = _restockProduct.RestockProducts()
                           .Select(p => new ResponsePurchaseOrder(
                                          p.ProductId, 
                                          p.Description, 
                                          p.QuantityOnHand, 
                                          p.ReorderAmount));

            return products;
        }
    }
}