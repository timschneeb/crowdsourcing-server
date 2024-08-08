using System.Threading.Tasks;
using GalaxyBudsCrowdsourcing.Models;
using Microsoft.AspNetCore.Mvc;

namespace GalaxyBudsCrowdsourcing.Controllers.v2
{
    [Route("/v2/[controller]")]
    [ApiController]
    public class ResultController(ResultContext context) : ControllerBase
    {
        // POST: /Result
        [HttpPost("/v2/[controller]")]
        public async Task<ActionResult> PostResult(ResultItem resultItem)
        {
            await context.ResultItems.AddAsync(resultItem);
            await context.SaveChangesAsync();
            return new OkResult();
        }
    }
}