using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Stock.Data;
using Stock.Models;
using System.Xml;

namespace Stock.Controllers
{
    public class StockInformationController : Controller
    {
        private readonly StockDbContext _context;

        public StockInformationController(StockDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            InitializeStocks();

            ViewData["SecIDSortParm"] = String.IsNullOrEmpty(sortOrder) ? "SecID_desc" : "";
            ViewData["ShortNameSortParm"] = sortOrder == "ShortName" ? "ShortName_desc" : "ShortName";
            ViewData["StockPriceSortParm"] = sortOrder == "StockPrice" ? "StockPrice_desc" : "StockPrice";
            ViewData["ListLevelSortParm"] = sortOrder == "ListLevel" ? "ListLevel_desc" : "ListLevel";

            ViewData["CurrentFilter"] = searchString;

            var stocks = from s in _context.StockInformations
                         select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                stocks = stocks.Where(s => s.SecID.Contains(searchString)
                                       || s.ShortName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "SecID_desc":
                    stocks = stocks.OrderByDescending(s => s.SecID);
                    break;
                case "ShortName":
                    stocks = stocks.OrderBy(s => s.ShortName);
                    break;
                case "ShortName_desc":
                    stocks = stocks.OrderByDescending(s => s.ShortName);
                    break;
                case "StockPrice":
                    stocks = stocks.OrderBy(s => s.StockPrice);
                    break;
                case "StockPrice_desc":
                    stocks = stocks.OrderByDescending(s => s.StockPrice);
                    break;
                case "ListLevel":
                    stocks = stocks.OrderBy(s => s.ListLevel);
                    break;
                case "ListLevel_desc":
                    stocks = stocks.OrderByDescending(s => s.ListLevel);
                    break;
                default:
                    stocks = stocks.OrderBy(s => s.SecID);
                    break;
            }


            return View(await stocks.AsNoTracking().ToListAsync());
        }

        public void InitializeStocks()
        {
            //foreach (StockInformation item in _context.StockInformations)
            //{
            //    _context.StockInformations.Remove(item);
            //}
            //_context.SaveChanges();

            String URLString = "https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities.xml?securities.columns=SECID,SHORTNAME,PREVPRICE,LOTSIZE,LISTLEVEL&iss.meta=off&iss.only=securities";

            List<StockInformation> stocks = new List<StockInformation>();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(URLString);

            XmlElement? xRoot = xDoc.DocumentElement;
            XmlNodeList? nodes = xRoot?.SelectNodes("//row");
            foreach (XmlNode node in nodes)
            {
                StockInformation stock = new StockInformation();

                XmlNode? attr1 = node.Attributes.GetNamedItem("SECID");

                XmlNode? attr2 = node.Attributes.GetNamedItem("SHORTNAME");

                XmlNode? attr3 = node.Attributes.GetNamedItem("PREVPRICE");

                XmlNode? attr4 = node.Attributes.GetNamedItem("LISTLEVEL");

                if (attr3.Value != "")
                {
                    stock.SecID = attr1.Value;
                    stock.ShortName = attr2.Value;
                    stock.StockPrice = Convert.ToDecimal(attr3.Value);
                    stock.ListLevel = Convert.ToInt32(attr4.Value);
                    stocks.Add(stock);
                }
            }

            foreach (StockInformation s in stocks)
            {
                //_context.StockInformations.AddOrUpdate(i=>i.SecID, s.SecID);
                //_context.StockInformations.AddOrUpdate(s);

                var stock = _context.StockInformations.FirstOrDefault(i => i.SecID == s.SecID);
                if (stock == null)
                {
                    _context.StockInformations.Add(s);
                }
                else
                {
                    stock.ShortName = s.ShortName;
                    stock.StockPrice = s.StockPrice;
                    stock.ListLevel = s.ListLevel;
                }
            }
            _context.SaveChanges();
        }


    }
}
