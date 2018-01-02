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

namespace productionorderservice.Services
{
    public class StateManagementService : IStateManagementService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly HttpClient client;
        public StateManagementService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            if (_configuration["ChangeStatePostEndpoint"] != null)
            {
                client = new HttpClient();

            }
        }
        public async Task<ProductionOrder> setProductionOrderToStatusById(int productionOrderId, stateEnum newState)
        {
            var produtionOrder = await _context.ProductionOrders
                        .Where(x => x.productionOrderId == productionOrderId)
                        .FirstOrDefaultAsync();
            if (produtionOrder == null)
                return null;
            var productionOrderType = await _context.ProductionOrderTypes
                        .Where(x => x.productionOrderTypeId == produtionOrder.productionOrderTypeId)
                        .Include(x => x.stateConfiguration)
                        .ThenInclude(x => x.states)
                        .FirstOrDefaultAsync();
            var curState = productionOrderType.stateConfiguration.states
                        .Where(x => x.state == produtionOrder.currentStatus.ToString()).FirstOrDefault();
            if (curState == null)
                return null;
            if (curState.possibleNextStates.Contains(newState.ToString()))
            {
                produtionOrder.currentStatus = newState.ToString();
                _context.Entry(produtionOrder).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                await postAfterChangedState(produtionOrder);
                return produtionOrder;
            }
            return null;

        }

        public async Task<ProductionOrder> setProductionOrderToStatusByNumber(string productionOrderNumber, stateEnum newState)
        {
            var produtionOrder = await _context.ProductionOrders
                                    .Where(x => x.productionOrderNumber == productionOrderNumber)
                                    .FirstOrDefaultAsync();
            if (produtionOrder == null)
                return null;
            var productionOrderType = await _context.ProductionOrderTypes
                        .Where(x => x.productionOrderTypeId == produtionOrder.productionOrderTypeId)
                        .Include(x => x.stateConfiguration)
                        .ThenInclude(x => x.states)
                        .FirstOrDefaultAsync();
            var curState = productionOrderType.stateConfiguration.states
                        .Where(x => x.state == produtionOrder.currentStatus.ToString()).FirstOrDefault();
            if (curState == null)
                return null;
            if (curState.possibleNextStates.Contains(newState.ToString()))
            {
                produtionOrder.currentStatus = newState.ToString();
                _context.Entry(produtionOrder).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                await postAfterChangedState(produtionOrder);
                return produtionOrder;
            }
            return null;
        }

        private async Task postAfterChangedState(ProductionOrder productionOrder)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var content = new StringContent(JsonConvert.SerializeObject(productionOrder), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(_configuration["ChangeStatePostEndpoint"], content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Data posted");

            }
        }


    }
}