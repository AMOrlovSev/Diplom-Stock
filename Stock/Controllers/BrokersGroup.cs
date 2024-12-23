using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stock.Data;
using Stock.Models.VM;

namespace Stock.Controllers
{
    public class BrokersGroup : Controller
    {
        private readonly StockDbContext _context;
        public BrokersGroup(StockDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            IQueryable<BrokerGroup> data = from portfolios in _context.Portfolios
                                           join brokers in _context.Brokers on portfolios.BrokerID equals brokers.BrokerID
                                           select new BrokerGroup()
                                           {
                                               BrokerName = brokers.BrokerName,
                                           };

            IQueryable<BrokerGroup> dataGroup = from d in data
                                                group d by d.BrokerName into dateGroup
                                                select new BrokerGroup()
                                                {
                                                    BrokerName = dateGroup.Key,
                                                    BrokerCount = dateGroup.Count()
                                                };
            return View(await dataGroup.AsNoTracking().ToListAsync());
        }
    }
}
