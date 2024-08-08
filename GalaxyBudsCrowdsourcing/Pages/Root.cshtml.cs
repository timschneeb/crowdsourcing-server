using GalaxyBudsCrowdsourcing.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GalaxyBudsCrowdsourcing.Pages
{
    public class Root(ExperimentContext dbContext) : PageModel
    {
        public readonly ExperimentContext DbContext = dbContext;
    }
}