using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using productionorderservice.Model;

namespace productionorderservice.Services.Interfaces
{
    public interface IThingGroupService
    {
        Task<(ThingGroup, HttpStatusCode)> getGroup(int groupId);
        Task<(List<ThingGroup>, HttpStatusCode)> getGroupsList(int[] groupIds);
        Task<(List<ThingGroup>, HttpStatusCode)> getGroups(int startat, int quantity,
        string fieldFilter, string fieldValue, string orderField, string order);
        Task<(List<Thing>, HttpStatusCode)> GetAttachedThings(int groupId);
    }
}