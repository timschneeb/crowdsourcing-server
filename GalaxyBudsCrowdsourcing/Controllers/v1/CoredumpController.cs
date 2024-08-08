using System.Collections.Generic;
using System.Threading.Tasks;
using GalaxyBudsCrowdsourcing.Models;
using Microsoft.AspNetCore.Mvc;

namespace GalaxyBudsCrowdsourcing.Controllers.v1
{
    [Route("/[controller]")]
    [ApiController]
    public class CoredumpController(CoredumpContext context) : ControllerBase
    {
        // GET: /Coredumps
        [HttpGet("/Coredumps")]
        public ActionResult<IEnumerable<CoredumpItem>> GetDumps()
        {
            return context.CoredumpItems;
        }

        // GET: /Coredump/1
        [HttpGet("{id:long}")]
        public async Task<ActionResult<CoredumpItem>> GetDump(long id)
        {
            var item = await context.CoredumpItems.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // POST: /Coredump
        [HttpPost]
        public async Task<ActionResult> PostDump(CoredumpItem resultItem)
        {
            await context.CoredumpItems.AddAsync(resultItem);
            await context.SaveChangesAsync();
            return new OkResult();
        }
    }
}