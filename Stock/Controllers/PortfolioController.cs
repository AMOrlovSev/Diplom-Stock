using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stock.Data;
using Stock.Models;

namespace Stock.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly StockDbContext _context;
        public PortfolioController(StockDbContext context)
        {
            _context = context;
        }

        //GET: Portfolios
        public async Task<IActionResult> Index()
        {
            var userLogin = HttpContext.User.Identity.Name;
            var portfolios = _context.Portfolios.Include(p => p.BrokerageAccount).Include(p => p.Broker).Include(p => p.User);
            var portfoliosUser = portfolios.Where(p => p.User.Email.Equals(userLogin));
            return View(await portfoliosUser.ToListAsync());
        }


        // GET: Portfolios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolio = await _context.Portfolios.Include(p => p.BrokerageAccount).Include(p => p.Broker).Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PortfolioID == id);
            if (portfolio == null)
            {
                return NotFound();
            }

            return View(portfolio);
        }


        // GET: Portfolios/Create
        public IActionResult Create()
        {
            var userLogin = HttpContext.User.Identity.Name;
            var userUser = _context.Users.Where(p => p.Email.Equals(userLogin));
            ViewData["UserID"] = new SelectList(userUser, "UserID", "Login");
            ViewData["BrokerID"] = new SelectList(_context.Brokers, "BrokerID", "BrokerName");
            ViewData["BrokerageAccountID"] = new SelectList(_context.BrokerageAccounts, "BrokerageAccountID", "BrokerageAccountType");
            return View();
        }

        // POST: Portfolios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PortfolioID,PortfolioName,UserID,BrokerID,BrokerageAccountID")] Portfolio portfolio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(portfolio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var userLogin = HttpContext.User.Identity.Name;
            var userUser = _context.Users.Where(p => p.Email.Equals(userLogin));
            ViewData["UserID"] = new SelectList(userUser, "UserID", "Login", userLogin);
            ViewData["BrokerID"] = new SelectList(_context.Brokers, "BrokerID", "BrokerName", portfolio.BrokerID);
            ViewData["BrokerageAccountID"] = new SelectList(_context.BrokerageAccounts, "BrokerageAccountID", "BrokerageAccountType", portfolio.BrokerageAccountID);
            return View(portfolio);
        }


        // GET: Portfolios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolio = await _context.Portfolios.Include(p => p.BrokerageAccount).Include(p => p.Broker).Include(p => p.User)
                .FirstOrDefaultAsync(i => i.PortfolioID == id);
            if (portfolio == null)
            {
                return NotFound();
            }
            var userLogin = HttpContext.User.Identity.Name;
            var userUser = _context.Users.Where(p => p.Email.Equals(userLogin));
            ViewData["UserID"] = new SelectList(userUser, "UserID", "Login", userLogin);
            ViewData["BrokerID"] = new SelectList(_context.Brokers, "BrokerID", "BrokerName");
            ViewData["BrokerageAccountID"] = new SelectList(_context.BrokerageAccounts, "BrokerageAccountID", "BrokerageAccountType");
            return View(portfolio);
        }

        // POST: Portfolios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PortfolioID,UserID,PortfolioName,BrokerID,BrokerageAccountID")] Portfolio portfolio)
        {
            if (id != portfolio.PortfolioID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(portfolio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioExists(portfolio.PortfolioID))
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
            var userLogin = HttpContext.User.Identity.Name;
            var userUser = _context.Users.Where(p => p.Email.Equals(userLogin));
            ViewData["UserID"] = new SelectList(userUser, "UserID", "Login", userLogin);
            ViewData["BrokerID"] = new SelectList(_context.Brokers, "BrokerID", "BrokerName");
            ViewData["BrokerageAccountID"] = new SelectList(_context.BrokerageAccounts, "BrokerageAccountID", "BrokerageAccountType");
            return View(portfolio);
        }

        private bool PortfolioExists(int id)
        {
            return _context.Portfolios.Any(e => e.PortfolioID == id);
        }



        // GET: Portfolios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolio = await _context.Portfolios.Include(p => p.BrokerageAccount).Include(p => p.Broker).Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PortfolioID == id);
            if (portfolio == null)
            {
                return NotFound();
            }

            return View(portfolio);
        }

        // POST: Portfolios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var portfolio = await _context.Portfolios.FindAsync(id);
            if (portfolio != null)
            {
                _context.Portfolios.Remove(portfolio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
