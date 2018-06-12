using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using productionorderservice.Data;
using productionorderservice.Model;
using productionorderservice.Services.Interfaces;

namespace productionorderservice.Services {
    public class HistStatesService : IHistStateService {

        private readonly ApplicationDbContext _context;

        public HistStatesService (ApplicationDbContext context) {
            _context = context;
        }

        public async Task<IEnumerable<HistState>> getHistStates (int productionOrderId) {
            var histStates = await _context.HistStates
                .Where (x => x.productionOrderId == productionOrderId)
                .OrderBy (x => x.date)
                .ToListAsync ();

            return histStates;

        }

        public async Task<IEnumerable<int>> getHistStatesPerStatusAndDate (string statusSearch, long startDate, long endDate) {
            var productionOrderIds = await _context.HistStates.Where (x => x.state.ToLower () == statusSearch.ToLower () &&
                    x.date >= startDate && x.date <= endDate)
                .Select (x => x.productionOrderId)
                .ToListAsync ();

            return productionOrderIds;
        }

        public async Task<HistState> addHistStates (int productionOrderId, string state, string username) {
            try {
                HistState histState = new HistState ();
                histState.date = DateTime.Now.Ticks;
                histState.state = state;
                if (username != null) {
                    histState.username = username;
                }
                histState.productionOrderId = productionOrderId;

                Console.WriteLine ("addHistStates - histState: ");
                Console.WriteLine (histState.ToString ());

                _context.HistStates.Add (histState);
                await _context.SaveChangesAsync ();

                return histState;

            } catch (Exception ex) {
                Console.WriteLine (ex.ToString ());
                return null;
            }
        }

    }
}