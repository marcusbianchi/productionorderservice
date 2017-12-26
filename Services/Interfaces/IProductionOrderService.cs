using System.Collections.Generic;
using System.Threading.Tasks;
using productionorderservice.Model;

namespace productionorderservice.Services.Interfaces
{
    public interface IProductionOrderService
    {
        Task<List<ProductionOrder>> getProductionOrders(int startat, int quantity);
        Task<ProductionOrder> getProductionOrder(int productionOrderId);
        Task<ProductionOrder> addProductionOrder(ProductionOrder productionOrder);
        Task<ProductionOrder> updateProductionOrder(int productionOrderId, ProductionOrder productionOrder);
        Task<ProductionOrder> deleteProductionOrder(int productionOrderId);

    }
}