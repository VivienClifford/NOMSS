using Service.Models;
using System.Collections.Generic;

namespace Service
{
    public interface IRestockProduct
    {
        /// <summary>
        /// Restock products that have gone under the reorder threshold, using the normal dataset
        /// </summary>
        /// <returns></returns>
        IEnumerable<Product> RestockProducts();

        /// <summary>
        /// Restock products that have gone under the reorder threshold, using parameter OrderProduct
        /// </summary>
        /// <param name="orderProducts"></param>
        /// <returns></returns>
        IEnumerable<Product> RestockProducts(OrderProduct orderProducts);
    }
}
