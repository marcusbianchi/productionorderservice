using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using productionorderservice.Data;
using productionorderservice.Model;
using productionorderservice.Services.Interfaces;
using productionorderservice.Validation;

namespace productionorderservice.Services {
    public class StateManagementService : IStateManagementService {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHistStateService _histStateService;
        private readonly HttpClient client;
        public StateManagementService (ApplicationDbContext context, IConfiguration configuration, IHistStateService histStateService) {
            _context = context;
            _configuration = configuration;
            _histStateService = histStateService;
            client = new HttpClient ();
        }
        public async Task<ProductionOrder> setProductionOrderToStatusById (int productionOrderId, stateEnum newState, string username) {
            var produtionOrder = await _context.ProductionOrders
                .Where (x => x.productionOrderId == productionOrderId)
                .FirstOrDefaultAsync ();

            //alteração feita para a mudança de status gravar tbm o nome do usuário

            Console.WriteLine ("produtionOrder - from Service: ");
            Console.WriteLine (produtionOrder.ToString ());

            if (produtionOrder == null)
                return null;
            var productionOrderType = await _context.ProductionOrderTypes
                .Where (x => x.productionOrderTypeId == produtionOrder.productionOrderTypeId)
                .Include (x => x.stateConfiguration)
                .ThenInclude (x => x.states)
                .FirstOrDefaultAsync ();

            string url = productionOrderType.stateConfiguration.states
                .Where (x => x.state == newState.ToString ()).FirstOrDefault ().url;

            produtionOrder.currentStatus = newState.ToString ();
            produtionOrder.latestUpdate = DateTime.Now.Ticks;
            if (username != null) {
                produtionOrder.username = username;
            } else {
                produtionOrder.username = "nulo";
            }
            _context.Entry (produtionOrder).State = EntityState.Modified;
            await _context.SaveChangesAsync ();

            await _histStateService.addHistStates (productionOrderId, newState.ToString (), username);

            if (!string.IsNullOrEmpty (url))
                postAfterChangedState (url, produtionOrder);
            return produtionOrder;

        }

        public async Task<ProductionOrder> setProductionOrderToStatusByNumber (string productionOrderNumber, stateEnum newState) {
            var produtionOrder = await _context.ProductionOrders
                .Where (x => x.productionOrderNumber == productionOrderNumber)
                .FirstOrDefaultAsync ();
            if (produtionOrder == null)
                return null;
            var productionOrderType = await _context.ProductionOrderTypes
                .Where (x => x.productionOrderTypeId == produtionOrder.productionOrderTypeId)
                .Include (x => x.stateConfiguration)
                .ThenInclude (x => x.states)
                .FirstOrDefaultAsync ();
            var curState = productionOrderType.stateConfiguration.states
                .Where (x => x.state == produtionOrder.currentStatus.ToString ()).FirstOrDefault ();
            if (curState == null)
                return null;
            if (curState.possibleNextStates.Contains (newState.ToString ())) {
                string url = productionOrderType.stateConfiguration.states
                    .Where (x => x.state == newState.ToString ()).FirstOrDefault ().url;

                produtionOrder.currentStatus = newState.ToString ();
                produtionOrder.latestUpdate = DateTime.Now.Ticks;
                _context.Entry (produtionOrder).State = EntityState.Modified;
                await _context.SaveChangesAsync ();

                if (!string.IsNullOrEmpty (url))
                    postAfterChangedState (url, produtionOrder);
                return produtionOrder;
            }
            return null;
        }

        private async void postAfterChangedState (string url, ProductionOrder productionOrder) {
            if (url != null) {
                try {
                    client.DefaultRequestHeaders.Accept.Clear ();
                    client.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));
                    var content = new StringContent (JsonConvert.SerializeObject (productionOrder), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync (url, content);
                    if (response.IsSuccessStatusCode) {
                        Console.WriteLine ("Data posted");

                    }
                } catch (Exception ex) {
                    Console.WriteLine (ex.Message);
                }
            }
        }

    }
}