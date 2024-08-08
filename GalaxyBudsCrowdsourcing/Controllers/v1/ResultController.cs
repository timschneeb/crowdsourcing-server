using System.Threading.Tasks;
using GalaxyBudsCrowdsourcing.Models;
using Microsoft.AspNetCore.Mvc;

namespace GalaxyBudsCrowdsourcing.Controllers.v1
{
    [Route("/[controller]")]
    [ApiController]
    public class ResultController(ResultContext context) : ControllerBase
    {
        // POST: /Result
        [HttpPost]
        public async Task<ActionResult> PostResult(ResultItem resultItem)
        {
            await context.ResultItems.AddAsync(resultItem);
            await context.SaveChangesAsync();
            return new OkResult();
        }
    }
}