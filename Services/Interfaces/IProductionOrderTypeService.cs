using System.Collections.Generic;
using System.Threading.Tasks;
using productionorderservice.Model;

namespace productionorderservice.Services.Interfaces
{
    public interface IProductionOrderTypeService
    {
        Task<List<ProductionOrderType>> getProductionOrderTypes(int startat, int quantity);
        Task<ProductionOrderType> getProductionOrderType(int productionOrderTypeId);
        Task<ProductionOrderType> addProductionOrderType(ProductionOrderType productionOrderType);
        Task<ProductionOrderType> updateProductionOrderType(int productionOrderTypeId, ProductionOrderType productionOrderType);
        Task<ProductionOrderType> deleteProductionOrderType(int productionOrderTypeId);
    }
}