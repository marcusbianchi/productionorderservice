using System.Collections.Generic;
using System.Threading.Tasks;
using productionorderservice.Model;

namespace productionorderservice.Services.Interfaces
{
    public interface IStateConfigurationService
    {
        Task<StateConfiguration> getStateConfiguration(int productionOrderTypeId);
        Task<StateConfiguration> updateProductionOrderType(int productionOrderTypeId, StateConfiguration stateConfiguration);
        Task<bool> deleteProductionOrderType(int productionOrderTypeId);
    }
}