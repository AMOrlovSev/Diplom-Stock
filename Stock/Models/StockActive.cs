using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.Models
{
    [Index(nameof(PortfolioID), nameof(SecID), IsUnique = true)]
    public class StockActive
    {
        public int StockActiveID { get; set; }

        public int PortfolioID { get; set; }

        [ForeignKey("StockInformation")]
        public string SecID { get; set; }

        [Required]
        [Display(Name = "Количество")]
        [Range(1, int.MaxValue)]
        public int Number { get; set; }

        [Display(Name = "Текущая стоимость")]
        public decimal CostCurrent
        {
            get
            {
                if (StockInformation != null)
                    return Number * StockInformation.StockPrice;
                else return 0;
            }
        }




        public Portfolio? Portfolio { get; set; }
        public StockInformation? StockInformation { get; set; }
    }
}
