using System.ComponentModel.DataAnnotations;

namespace Stock.Models
{
    public class BrokerageAccount
    {
        public int BrokerageAccountID { get; set; }

        [Display(Name = "Счет")]
        public string BrokerageAccountType { get; set; }



        public ICollection<Portfolio>? Portfolios { get; set; }
    }
}
