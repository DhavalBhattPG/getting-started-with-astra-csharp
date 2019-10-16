using System.Collections.Generic;
using System.Linq;
using getting_started_with_apollo_csharp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Cassandra.Data.Linq;

namespace getting_started_with_apollo_csharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpacecraftController : ControllerBase
    {
       private IDataStaxService Service { get; set; }

        public SpacecraftController(IDataStaxService service)
        {
            Service = service;
        }

        // GET api/spacecrafts
        [HttpGet]
        public ActionResult<ICollection<Models.spacecraft_journey_catalog>> GetAllSpaceCraft()
        {
            var spaceCraft = new Table<Models.spacecraft_journey_catalog>(Service.Session);
            var crafts = spaceCraft.Execute().OrderBy(s => s.Spacecraft_Name).ThenBy(s => s.Start);
            return crafts.ToList();
        }

        // GET api/spacecrafts/{spaceCraftName}
        [HttpGet("{spaceCraftName}")]
        public ActionResult<ICollection<Models.spacecraft_journey_catalog>> GetJourneysForSpacecraft(string spaceCraftName)
        {
            var spaceCraft = new Table<Models.spacecraft_journey_catalog>(Service.Session);
            var craft = spaceCraft.Where(s => s.Spacecraft_Name==spaceCraftName).Execute().OrderBy(s => s.Start);
            return craft.ToList();
        }
    }
}
