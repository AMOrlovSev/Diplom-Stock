using Microsoft.EntityFrameworkCore;
using Stock.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Stock.Data
{
    public static class SeedData
    {
        //сначала надо, чтобы заполнилась таблица StockInformation
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new StockDbContext(serviceProvider.GetRequiredService<DbContextOptions<StockDbContext>>()))
            {
                context.Database.EnsureCreated();



                if (!context.Users.Any())
                {
                    var users = new User[]
                    {
                        new User{LastName="LastName1",FirstName="FirstName1",Email="Email1@gmail.com",Login="Login1",Password="Password1"},
                        new User{LastName="LastName2",FirstName="FirstName2",Email="Email2@gmail.com",Login="Login2",Password="Password2"},
                        new User{LastName="LastName3",FirstName="FirstName3",Email="Email3@gmail.com",Login="Login3",Password="Password3"},
                        new User{LastName="LastName4",FirstName="FirstName4",Email="Email4@gmail.com",Login="Login4",Password="Password4"}
                    };
                    foreach (User u in users)
                    {
                        context.Users.Add(u);
                    }
                    context.SaveChanges();
                }


                if (!context.Brokers.Any())
                {
                    var brokers = new Broker[]
                        {
                        new Broker{BrokerName="Сбербанк"},
                        new Broker{BrokerName="Альфа"},
                        new Broker{BrokerName="Т"},
                        new Broker{BrokerName="ВТБ"}
                        };
                    foreach (Broker b in brokers)
                    {
                        context.Brokers.Add(b);
                    }
                    context.SaveChanges();
                }



                if (!context.BrokerageAccounts.Any())
                {
                    var brokerageAccounts = new BrokerageAccount[]
                        {
                        new BrokerageAccount{BrokerageAccountType="Брокерский"},
                        new BrokerageAccount{BrokerageAccountType="ИИС"}
                        };
                    foreach (BrokerageAccount ba in brokerageAccounts)
                    {
                        context.BrokerageAccounts.Add(ba);
                    }
                    context.SaveChanges();
                }



                if (!context.Portfolios.Any())
                {
                    var portfolios = new Portfolio[]
                        {

                        new Portfolio{PortfolioName="Портфель1",UserID=1,BrokerID=1,BrokerageAccountID=1},
                        new Portfolio{PortfolioName="Портфель2",UserID=2,BrokerID=2,BrokerageAccountID=1},
                        new Portfolio{PortfolioName="Портфель3",UserID=2,BrokerID=2,BrokerageAccountID=2},
                        new Portfolio{PortfolioName="Портфель4",UserID=3,BrokerID=1,BrokerageAccountID=1},
                        new Portfolio{PortfolioName="Портфель5",UserID=3,BrokerID=2,BrokerageAccountID=1},
                        new Portfolio{PortfolioName="Портфель6",UserID=3,BrokerID=3,BrokerageAccountID=2},
                        new Portfolio{PortfolioName="Портфель7",UserID=4,BrokerID=1,BrokerageAccountID=1},
                        new Portfolio{PortfolioName="Портфель8",UserID=4,BrokerID=2,BrokerageAccountID=1},
                        new Portfolio{PortfolioName="Портфель9",UserID=4,BrokerID=3,BrokerageAccountID=1},
                        new Portfolio{PortfolioName="Портфель10",UserID=4,BrokerID=4,BrokerageAccountID=1},
                        new Portfolio{PortfolioName="Портфель11",UserID=4,BrokerID=4,BrokerageAccountID=2}
                        };
                    foreach (Portfolio p in portfolios)
                    {
                        context.Portfolios.Add(p);
                    }
                    context.SaveChanges();
                }



                if (!context.StockActives.Any())
                {
                    var stockActives = new StockActive[]
                        {
                        new StockActive{PortfolioID=1, SecID="AFKS", Number=1},
                        new StockActive{PortfolioID=1, SecID="AFLT", Number=2},
                        new StockActive{PortfolioID=1, SecID="AGRO", Number=3},

                        new StockActive{PortfolioID=2, SecID="ALRS", Number=4},
                        new StockActive{PortfolioID=2, SecID="ASTR", Number=5},
                        new StockActive{PortfolioID=2, SecID="AQUA", Number=6},

                        new StockActive{PortfolioID=3, SecID="BELU", Number=7},
                        new StockActive{PortfolioID=3, SecID="BSPB", Number=8},
                        new StockActive{PortfolioID=3, SecID="CBOM", Number=9},

                        new StockActive{PortfolioID=4, SecID="CHMF", Number=10},
                        new StockActive{PortfolioID=4, SecID="CIAN", Number=11},
                        new StockActive{PortfolioID=4, SecID="ENPG", Number=12},

                        new StockActive{PortfolioID=5, SecID="ETLN", Number=13},
                        new StockActive{PortfolioID=5, SecID="EUTR", Number=14},
                        new StockActive{PortfolioID=5, SecID="FEES", Number=15},

                        new StockActive{PortfolioID=6, SecID="FIVE", Number=16},
                        new StockActive{PortfolioID=6, SecID="FIXP", Number=17},
                        new StockActive{PortfolioID=6, SecID="FLOT", Number=18},

                        new StockActive{PortfolioID=7, SecID="ALRS", Number=19},
                        new StockActive{PortfolioID=7, SecID="ASTR", Number=20},
                        new StockActive{PortfolioID=7, SecID="AQUA", Number=21},

                        new StockActive{PortfolioID=8, SecID="BELU", Number=22},
                        new StockActive{PortfolioID=8, SecID="BSPB", Number=23},
                        new StockActive{PortfolioID=8, SecID="CBOM", Number=24},

                        new StockActive{PortfolioID=9, SecID="CHMF", Number=25},
                        new StockActive{PortfolioID=9, SecID="CIAN", Number=26},
                        new StockActive{PortfolioID=9, SecID="ENPG", Number=27},

                        new StockActive{PortfolioID=10, SecID="ETLN", Number=28},
                        new StockActive{PortfolioID=10, SecID="EUTR", Number=29},
                        new StockActive{PortfolioID=10, SecID="FEES", Number=30},

                        new StockActive{PortfolioID=11, SecID="FIVE", Number=31},
                        new StockActive{PortfolioID=11, SecID="FIXP", Number=32},
                        new StockActive{PortfolioID=11, SecID="FLOT", Number=33},
                        };
                    foreach (StockActive sa in stockActives)
                    {
                        context.StockActives.Add(sa);
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
