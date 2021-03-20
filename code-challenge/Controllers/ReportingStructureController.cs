using challenge.Models;
using challenge.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace challenge.Controllers
{
    [Route("api/reportingstructure")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public class ReportingStructureController : Controller
    {
        private readonly ILogger _logger;
        private readonly IReportingStructureService _reportingStructureService;

        public ReportingStructureController(ILogger<ReportingStructureController> logger, IReportingStructureService reportingStructureService)
        {
            _logger = logger;
            _reportingStructureService = reportingStructureService;
        }

        //GET API to fetch reporting structure and no of reporting employees.
        [HttpGet("{id}", Name = "getReportingStructure")]
        public IActionResult Get(string id)
        {
            ReportingStructure reportingStructure;
            _logger.LogDebug($"Received employee report structure request for id: '{id}'");
            try
            {
                reportingStructure = _reportingStructureService.GetReportEmployees(id);
                if (reportingStructure == null)
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

            return Ok(reportingStructure);
        }
    }
}