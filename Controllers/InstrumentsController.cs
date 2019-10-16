using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using getting_started_with_apollo_csharp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using getting_started_with_apollo_csharp.Models;
using Cassandra.Data.Linq;

namespace getting_started_with_apollo_csharp.Controllers
{
    [Route("api/spacecraft/{spaceCraftName}/{journeyId}/instruments")]
    [ApiController]
    public class InstrumentsController : ControllerBase
    {
        private IDataStaxService Service { get; set; }

        public InstrumentsController(IDataStaxService service)
        {
            Service = service;
        }

        // GET api/spacecraft/{spaceCraftName}/{journeyId}/instruments/temperature
        [HttpGet("temperature")]
        public ActionResult<ICollection<spacecraft_temperature_over_time>> GetTemperatureReading(string spaceCraftName, Guid journeyId)
        {
            var spaceCraft = new Table<spacecraft_temperature_over_time>(Service.Session);
            IEnumerable<Models.spacecraft_temperature_over_time> temperature = spaceCraft.
                Where(s => s.Spacecraft_Name==spaceCraftName && s.Journey_Id==journeyId).Execute().OrderBy(s => s.Reading_Time);
            return temperature.ToList();
        }

        // GET api/spacecraft/{spaceCraftName}/{journeyId}/instruments/pressure
        [HttpGet("pressure")]
        public ActionResult<ICollection<spacecraft_pressure_over_time>> GetPressureReading(string spaceCraftName, Guid journeyId)
        {
            var spaceCraft = new Table<spacecraft_pressure_over_time>(Service.Session);
            IEnumerable<spacecraft_pressure_over_time> pressure = spaceCraft.
                Where(s => s.Spacecraft_Name==spaceCraftName && s.Journey_Id==journeyId).Execute().OrderBy(s => s.Reading_Time);
            return pressure.ToList();
        }

        // GET api/spacecraft/{spaceCraftName}/{journeyId}/instruments/location
        [HttpGet("location")]
        public ActionResult<ICollection<spacecraft_location_over_time>> GetLocationReading(string spaceCraftName, Guid journeyId)
        {
            var spaceCraft = new Table<spacecraft_location_over_time>(Service.Session);
            IEnumerable<spacecraft_location_over_time> location = spaceCraft.
                Where(s => s.Spacecraft_Name==spaceCraftName && s.Journey_Id==journeyId).Execute().OrderBy(s => s.Reading_Time);
            return location.ToList();
        }

        // GET api/spacecraft/{spaceCraftName}/{journeyId}/instruments/speed
        [HttpGet("speed")]
        public ActionResult<ICollection<spacecraft_speed_over_time>> GetSpeedReading(string spaceCraftName, Guid journeyId)
        {
            var spaceCraft = new Table<spacecraft_speed_over_time>(Service.Session);
            IEnumerable<spacecraft_speed_over_time> speed = spaceCraft.
                Where(s => s.Spacecraft_Name==spaceCraftName && s.Journey_Id==journeyId).Execute().OrderBy(s => s.Reading_Time);
            return speed.ToList();
        }
    }
}
