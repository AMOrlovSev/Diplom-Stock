using System.ComponentModel;

namespace Stock.Models.VM
{
    public class BrokerGroup
    {
        [DisplayName("Брокер")]
        public string BrokerName { get; set; }

        [DisplayName("Количество")]
        public int BrokerCount { get; set; }
    }
}
