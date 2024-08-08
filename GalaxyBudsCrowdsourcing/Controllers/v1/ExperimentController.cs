using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalaxyBudsCrowdsourcing.Models;
using Microsoft.AspNetCore.Mvc;

namespace GalaxyBudsCrowdsourcing.Controllers.v1
{
    [Route("/[controller]")]
    [ApiController]
    public class ExperimentController(ExperimentContext context) : ControllerBase
    {
        // GET: /Experiments
        [HttpGet]
        [Route("/[controller]s")]
        public ActionResult<IEnumerable<ExperimentItem>> GetExperiments()
        {
            return context.ExperimentItems
                .Where(x => x.Enabled)
                .AsEnumerable()
                .SignScripts(apiVersion: 1)
                .ToList();
        }
        
        // GET: /Experiments/Buds
        [HttpGet("/Experiments/{dev}")]
        public ActionResult<IEnumerable<ExperimentItem>> GetExperiments(Devices dev)
        {
            var list = context.ExperimentItems
                .AsEnumerable()
                .Where(x => x.Enabled)
                .Where(x => x.TargetDevices.Contains(dev))
                .SignScripts(apiVersion: 1)
                .ToList();

            list.ForEach(x => x.TargetDevices = [dev]);
            return list;
        }
        
        // GET: /Experiment/1
        [HttpGet("{id:long}")]
        public async Task<ActionResult<ExperimentItem>> GetExperiment(long id)
        {
            var item = (await context.ExperimentItems.FindAsync(id))?.SignScript(apiVersion: 1);
            if (item == null || item.Enabled == false)
            {
                return NotFound();
            }
            return item!;
        }
    }
}