using System.Collections.Generic;
using System.Linq;
using getting_started_with_apollo_csharp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Cassandra.Data.Linq;
using Cassandra.Mapping;
using System;

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

        // POST api/spacecrafts/{spaceCraftName}
        [HttpPost("{spaceCraftName}")]
        public ActionResult<Guid> CreateJourneyForSpacecraft(string spaceCraftName, [FromBody]string summary)
        {
            IMapper mapper = new Mapper(Service.Session);
            var journey = new Models.spacecraft_journey_catalog();
            journey.Spacecraft_Name=spaceCraftName;
            journey.Journey_Id=Cassandra.TimeUuid.NewId();
            journey.Active=false;
            journey.Start=DateTimeOffset.Now;
            journey.End = DateTimeOffset.Now.AddSeconds(1000);
            journey.Summary = summary;
            
            mapper.Insert(journey);

            return journey.Journey_Id;
        }
    }
}
