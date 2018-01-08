using System.Threading.Tasks;
using productionorderservice.Model;

namespace productionorderservice.Services.Interfaces
{
    public interface IAssociateProductionOrderService
    {
        Task<(ProductionOrder, string)> AssociateProductionOrder(int thingId, int productioOrderId);

    }
}