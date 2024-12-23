using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Stock.Models
{
    public class Portfolio
    {
        public int PortfolioID { get; set; }

        [Display(Name = "Имя портфеля")]
        public string? PortfolioName { get; set; }


        [Display(Name = "Пользователь")]
        public int UserID { get; set; }

        [Display(Name = "Брокер")]
        public int BrokerID { get; set; }

        [Display(Name = "Тип счета")]
        public int BrokerageAccountID { get; set; }




        public User? User { get; set; }
        public Broker? Broker { get; set; }
        public BrokerageAccount? BrokerageAccount { get; set; }
    }
}
