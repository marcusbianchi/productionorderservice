using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using productionorderservice.Model;

namespace productionorderservice.Services.Interfaces
{
    public interface IThingService
    {
        Task<(Thing, HttpStatusCode)> getThing(int thingId);
    }
}