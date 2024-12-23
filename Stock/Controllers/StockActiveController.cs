using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stock.Data;
using Stock.Models;

namespace Stock.Controllers
{
    public class StockActiveController : Controller
    {
        private readonly StockDbContext _context;
        public StockActiveController(StockDbContext context)
        {
            _context = context;
        }

        //GET: StockActives
        public async Task<IActionResult> Index(string searchString)
        {

            ViewData["CurrentFilter"] = searchString;

            var userLogin = HttpContext.User.Identity.Name;
            var stockActives = _context.StockActives
                                .Include(sa => sa.Portfolio)
                                    .ThenInclude(i => i.Broker)
                                .Include(sa => sa.Portfolio)
                                    .ThenInclude(i => i.BrokerageAccount)
                                .Include(sa => sa.StockInformation);

            var stockActivesUsers = stockActives.Include(sa => sa.Portfolio)
                                                    .ThenInclude(i => i.User);

            var stockActivesUser = stockActivesUsers.Where(p => p.Portfolio.User.Email.Equals(userLogin));

            if (!String.IsNullOrEmpty(searchString))
            {
                var stockActivesUserPortfolio = stockActivesUser.Where(s => s.Portfolio.PortfolioName.Contains(searchString));
                return View(await stockActivesUserPortfolio.ToListAsync());
            }

            return View(await stockActivesUser.ToListAsync());
        }


        // GET: StockActive/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockActive = await _context.StockActives
                                    .Include(sa => sa.Portfolio)
                                        .ThenInclude(i => i.Broker)
                                    .Include(sa => sa.Portfolio)
                                        .ThenInclude(i => i.BrokerageAccount)
                                    .Include(sa => sa.StockInformation)
                                    .FirstOrDefaultAsync(sa => sa.StockActiveID == id);
            if (stockActive == null)
            {
                return NotFound();
            }

            return View(stockActive);
        }


        // GET: StockActive/Create
        public IActionResult Create()
        {
            var userLogin = HttpContext.User.Identity.Name;
            var userPortfolios = _context.Portfolios.Where(p => p.User.Email.Equals(userLogin));
            ViewData["PortfolioID"] = new SelectList(userPortfolios, "PortfolioID", "PortfolioName");
            ViewData["StockInformation"] = new SelectList(_context.StockInformations, "SecID", "ShortName");
            return View();
        }

        // POST: StockActive/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StockActiveID,PortfolioID,SecID,Number")] StockActive stockActive)
        {
            //var dublicate = await _context.StockActives
            //    .FirstOrDefaultAsync(si => si.SecID == stockActive.SecID && si.PortfolioID == stockActive.PortfolioID);

            bool dublicate = PairPortfolioIDSecIDExists(stockActive);

            var userLogin = HttpContext.User.Identity.Name;
            var userPortfolios = _context.Portfolios.Where(p => p.User.Email.Equals(userLogin));

            if (ModelState.IsValid)
            {
                if (!dublicate)
                {
                _context.Add(stockActive);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                }
                TempData["Error"] = "Компания уже есть в портфеле";
                ViewData["PortfolioID"] = new SelectList(userPortfolios, "PortfolioID", "PortfolioName");
                ViewData["StockInformation"] = new SelectList(_context.StockInformations, "SecID", "ShortName");
                return View(stockActive);
            }
            ViewData["PortfolioID"] = new SelectList(userPortfolios, "PortfolioID", "PortfolioName");
            ViewData["StockInformation"] = new SelectList(_context.StockInformations, "SecID", "ShortName");
            return View(stockActive);
        }

        private bool PairPortfolioIDSecIDExists(StockActive stockActive)
        {
            var dublicate = _context.StockActives
                .Any(si => si.SecID == stockActive.SecID && si.PortfolioID == stockActive.PortfolioID);

            return dublicate;
        }



        // GET: StockActives/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockActive = await _context.StockActives
                                    .Include(sa => sa.Portfolio)
                                        .ThenInclude(i => i.Broker)
                                    .Include(sa => sa.Portfolio)
                                        .ThenInclude(i => i.BrokerageAccount)
                                    .Include(sa => sa.StockInformation)
                                    .FirstOrDefaultAsync(sa => sa.StockActiveID == id);
            if (stockActive == null)
            {
                return NotFound();
            }
            ViewData["PortfolioID"] = new SelectList(_context.Portfolios, "PortfolioID", "PortfolioName");
            ViewData["StockInformation"] = new SelectList(_context.StockInformations, "SecID", "ShortName");
            return View(stockActive);
        }

        // POST: StockActive/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StockActiveID,PortfolioID,SecID,Number")] StockActive stockActive)
        {
            if (id != stockActive.StockActiveID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockActive);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockActiveExists(stockActive.StockActiveID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PortfolioID"] = new SelectList(_context.Portfolios, "PortfolioID", "PortfolioName");
            ViewData["StockInformation"] = new SelectList(_context.StockInformations, "SecID", "ShortName");
            return View(stockActive);
        }

        private bool StockActiveExists(int id)
        {
            return _context.StockActives.Any(sa => sa.StockActiveID == id);
        }



        // GET: StockActive/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockActive = await _context.StockActives
                                    .Include(sa => sa.Portfolio)
                                        .ThenInclude(i => i.Broker)
                                    .Include(sa => sa.Portfolio)
                                        .ThenInclude(i => i.BrokerageAccount)
                                    .Include(sa => sa.StockInformation)
                                    .FirstOrDefaultAsync(sa => sa.StockActiveID == id);
            if (stockActive == null)
            {
                return NotFound();
            }

            return View(stockActive);
        }

        // POST: StockActive/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stockActive = await _context.StockActives.FindAsync(id);
            if (stockActive != null)
            {
                _context.StockActives.Remove(stockActive);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
