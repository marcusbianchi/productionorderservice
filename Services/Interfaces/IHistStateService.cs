using System.Collections.Generic;
using System.Threading.Tasks;
using productionorderservice.Model;
namespace productionorderservice.Services.Interfaces
{
    public interface IHistStateService
    {
        Task<IEnumerable<HistState>> getHistStates(int productionOrderId);         
        Task<HistState> addHistStates(int productionOrderId,string state);         
    }
}