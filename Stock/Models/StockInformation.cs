using System.ComponentModel.DataAnnotations;

namespace Stock.Models
{
    public class StockInformation
    {
        [Required]
        [Display(Name = "Тикер")]
        [Key]
        public string SecID { get; set; }

        [Required]
        [Display(Name = "Компания")]
        public string ShortName { get; set; }

        [Display(Name = "Цена за шт")]
        public decimal StockPrice { get; set; }

        [Display(Name = "Уровень Листинга")]
        public int ListLevel { get; set; }



        public ICollection<StockActive>? StockActive { get; set; }
    }
}
