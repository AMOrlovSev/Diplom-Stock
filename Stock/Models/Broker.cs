using System.ComponentModel.DataAnnotations;

namespace Stock.Models
{
    public class Broker
    {
        public int BrokerID { get; set; }

        [Display(Name = "Брокер")]
        public string BrokerName { get; set; }



        public ICollection<Portfolio>? Portfolios { get; set; }
    }
}
