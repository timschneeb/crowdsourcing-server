using System;
using System.Collections.Generic;
using System.Linq;
using GalaxyBudsCrowdsourcing.Models;
using Microsoft.AspNetCore.Mvc;

namespace GalaxyBudsCrowdsourcing.Controllers.v2
{
    [Route("/v2/[controller]")]
    [ApiController]
    public class ExperimentController(ExperimentContext context) : ControllerBase
    {
        // GET: /Experiments
        [HttpGet]
        [Route("/v2/[controller]s")]
        public ActionResult<IEnumerable<ExperimentItem>> GetExperiments()
        {
            return context.ExperimentItems
                .Where(x => x.Enabled)
                .AsEnumerable()
                .SignScripts(apiVersion: 2)
                .ToList();
        }
        
        // GET: /Experiments/Buds
        [HttpGet("/v2/[controller]s/{dev}")]
        public ActionResult<IEnumerable<ExperimentItem>> GetExperiments(Devices dev)
        {
            var list = context.ExperimentItems
                .AsEnumerable()
                .Where(x => x.Enabled)
                .Where(x => x.TargetDevices.Contains(dev))
                .SignScripts(apiVersion: 2)
                .ToList();

            list.ForEach(x => x.TargetDevices = new Devices[1] {dev});
            return list;
        }
        
        // GET: /Experiment/1
        [HttpGet("/v2/[controller]/{id:long}")]
        public ActionResult<ExperimentItem> GetExperiment(long id)
        {
            var item = context.ExperimentItems.FirstOrDefault(x => x.Id == id)?.SignScript(apiVersion: 2);
            if (item == null || item.Enabled == false)
            {
                return NotFound();
            }
            return item!;
        }
                
        // GET: /Experiment/1/script
        [HttpGet("/v2/[controller]/{id:long}/script")]
        public ActionResult GetExperimentScript(long id)
        {
            var path = context.ExperimentItems
                .Where(x => !x.IsLegacyVariant && x.Enabled)
                .FirstOrDefault(x => x.Id == id)?.Script;
            if (path == null)
                return NotFound();

            try
            {
                return new ContentResult
                {
                    Content = System.IO.File.ReadAllText(path),
                    ContentType = "text/plain"
                };
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}