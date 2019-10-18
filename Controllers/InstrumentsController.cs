using System;
using System.Collections.Generic;
using System.Linq;
using getting_started_with_apollo_csharp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using getting_started_with_apollo_csharp.Models;
using Cassandra.Data.Linq;
using Cassandra.Mapping;

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
        public ActionResult<PagedResultWrapper<ICollection<spacecraft_temperature_over_time>>> GetTemperatureReading(string spaceCraftName, Guid journeyId,
            [FromQuery]byte[] pageState, [FromQuery]int? pageSize)
        {
            var spaceCraft = new Table<spacecraft_temperature_over_time>(Service.Session);
            var query =spaceCraft.
                Where(s => s.Spacecraft_Name==spaceCraftName && s.Journey_Id==journeyId);
            if (pageSize.HasValue)
            {
                query.SetPageSize(pageSize.Value);
            }
            if (pageState!=null && pageState.Length>0)
            {
                query.SetPagingState(pageState);
            }
            var temperature = query.ExecutePaged();
            return new PagedResultWrapper<ICollection<spacecraft_temperature_over_time>>(pageSize.HasValue? pageSize.Value: 0,
                temperature.PagingState, temperature.OrderBy(s => s.Reading_Time).ToList()
            );
        }

        // GET api/spacecraft/{spaceCraftName}/{journeyId}/instruments/pressure
        [HttpGet("pressure")]
        public ActionResult<PagedResultWrapper<ICollection<spacecraft_pressure_over_time>>> GetPressureReading(string spaceCraftName, Guid journeyId,
                    [FromQuery]byte[] pageState, [FromQuery]int? pageSize)
        {

            var spaceCraft = new Table<spacecraft_pressure_over_time>(Service.Session);
            var query =spaceCraft.
                Where(s => s.Spacecraft_Name==spaceCraftName && s.Journey_Id==journeyId);
            if (pageSize.HasValue)
            {
                query.SetPageSize(pageSize.Value);
            }
            if (pageState!=null && pageState.Length>0)
            {
                query.SetPagingState(pageState);
            }
            var pressure = query.ExecutePaged();
            return new PagedResultWrapper<ICollection<spacecraft_pressure_over_time>>(pageSize.HasValue? pageSize.Value: 0,
                pressure.PagingState, pressure.OrderBy(s => s.Reading_Time).ToList()
            );
        }

        // GET api/spacecraft/{spaceCraftName}/{journeyId}/instruments/location
        [HttpGet("location")]
        public ActionResult<PagedResultWrapper<ICollection<spacecraft_location_over_time>>> GetLocationReading(string spaceCraftName, Guid journeyId,
                    [FromQuery]byte[] pageState, [FromQuery]int? pageSize)
        {
            var spaceCraft = new Table<spacecraft_location_over_time>(Service.Session);
            var query =spaceCraft.
                Where(s => s.Spacecraft_Name==spaceCraftName && s.Journey_Id==journeyId);
            if (pageSize.HasValue)
            {
                query.SetPageSize(pageSize.Value);
            }
            if (pageState!=null && pageState.Length>0)
            {
                query.SetPagingState(pageState);
            }
            var location = query.ExecutePaged();
            return new PagedResultWrapper<ICollection<spacecraft_location_over_time>>(pageSize.HasValue? pageSize.Value: 0,
                location.PagingState, location.OrderBy(s => s.Reading_Time).ToList()
            );
        }

        // GET api/spacecraft/{spaceCraftName}/{journeyId}/instruments/speed
        [HttpGet("speed")]
        public ActionResult<PagedResultWrapper<ICollection<spacecraft_speed_over_time>>> GetSpeedReading(string spaceCraftName, Guid journeyId,
                    [FromQuery]byte[] pageState, [FromQuery]int? pageSize)
        {
            var spaceCraft = new Table<spacecraft_speed_over_time>(Service.Session);
            var query =spaceCraft.
                Where(s => s.Spacecraft_Name==spaceCraftName && s.Journey_Id==journeyId);
            if (pageSize.HasValue)
            {
                query.SetPageSize(pageSize.Value);
            }
            if (pageState!=null && pageState.Length>0)
            {
                query.SetPagingState(pageState);
            }
            var speed = query.ExecutePaged();
            return new PagedResultWrapper<ICollection<spacecraft_speed_over_time>>(pageSize.HasValue? pageSize.Value: 0,
                speed.PagingState, speed.OrderBy(s => s.Reading_Time).ToList()
            );
        }
    
        [HttpPost("temperature")] 
        public ActionResult SaveTemperatures([FromBody]spacecraft_temperature_over_time[] temperatures)
        {
            IMapper mapper = new Mapper(Service.Session);
            var batch = mapper.CreateBatch();
            for(int i=0; i<temperatures.Count(); i++)
            {
                batch.Insert(temperatures[i]);
            }      
            mapper.Execute(batch);          
            return Ok();
        }

        [HttpPost("pressure")] 
        public ActionResult SavePressures([FromBody]spacecraft_pressure_over_time[] pressures)
        {            
            IMapper mapper = new Mapper(Service.Session);
            var batch = mapper.CreateBatch();
            for(int i=0; i<pressures.Count(); i++)
            {
                batch.Insert(pressures[i]);
            }      
            mapper.Execute(batch);  
            return Ok();
        }

        [HttpPost("speed")] 
        public ActionResult SaveSpeed([FromBody]spacecraft_speed_over_time[] speed)
        {            
            IMapper mapper = new Mapper(Service.Session);    
            var batch = mapper.CreateBatch();
            for(int i=0; i<speed.Count(); i++)
            {
                batch.Insert(speed[i]);
            }      
            mapper.Execute(batch);  
            return Ok();
        }

        [HttpPost("location")] 
        public ActionResult SaveLocations([FromBody]spacecraft_location_over_time[] locations)
        {
            IMapper mapper = new Mapper(Service.Session);    
            var batch = mapper.CreateBatch();
            for(int i=0; i<locations.Count(); i++)
            {
                batch.Insert(locations[i]);
            }      
            mapper.Execute(batch);  
            return Ok();
        }
    
    }
}
