using System.Collections.Generic;
using System.Threading.Tasks;
using productionorderservice.Model;

namespace productionorderservice.Services.Interfaces
{
    public interface IProductionOrderService
    {
        Task<(List<ProductionOrder>, int)> getProductionOrders(int startat, int quantity, ProductionOrderFields fieldFilter, string fieldValue, ProductionOrderFields orderField, OrderEnum order);
        Task<ProductionOrder> getProductionOrder(int productionOrderId);
        Task<ProductionOrder> addProductionOrder(ProductionOrder productionOrder);
        Task<bool> checkProductionOrderNumber(string productionOrderNumber);
        Task<bool> checkProductionOrderType(int productionOrderTypeId);
        Task<ProductionOrder> setProductionOrderToThing(ProductionOrder productioOrder, int thingId);

    }

    public enum ProductionOrderFields
    {
        Default,
        productionOrderNumber,
        currentStatus
    }
}