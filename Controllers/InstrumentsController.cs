using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
