using System.ComponentModel.DataAnnotations;

namespace IncomeExpenseManager.ViewModels
{
    public class CreateCategoryViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Der {0} muss zwischen {2} und {1} Zeichen lang sein.", MinimumLength = 1)]
        public string Name { get; set; }
    }
}
