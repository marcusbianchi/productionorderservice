using System.Threading.Tasks;
using productionorderservice.Model;
using productionorderservice.Validation;

namespace productionorderservice.Services.Interfaces
{
    public interface IStateManagementService
    {
        Task<ProductionOrder> setProductionOrderToStatusById(int productionOrderId, stateEnum newState, ProductionOrder productionOrder);
        Task<ProductionOrder> setProductionOrderToStatusByNumber(string productionOrderNumber, stateEnum newState);

    }

}