using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using challenge.Services;
using challenge.Models;

namespace code_challenge.Controllers
{
    [Route("api/compensation")]
    public class CompensationController : Controller
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService)
        {
            _logger = logger;
            _compensationService = compensationService;
        }

        [HttpGet]
        public IActionResult GetAllCompensations()
        {
            _logger.LogDebug($"Received compensation get request for all employees");

            var compensations = _compensationService.GetAllCompensations();

            if (compensations == null)
                return NotFound();

            return Ok(compensations);
        }

        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Received compensation create request for '{compensation.EmployeeId}'");
            try
            {
                _compensationService.Create(compensation);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }

            

            return CreatedAtRoute("getCompensationById", new { id = compensation.EmployeeId }, compensation);
        }

        [HttpGet("{id}", Name = "getCompensationById")]
        public IActionResult GetCompensationById(String id)
        {
            _logger.LogDebug($"Received compensation get request for '{id}'");

            var compensation = _compensationService.GetById(id);

            if (compensation == null)
                return NotFound();

            return Ok(compensation);
        }

        [HttpPut("{id}")]
        public IActionResult ReplaceCompensation(String id, [FromBody] Compensation newCompensation)
        {
            _logger.LogDebug($"Recieved compensation update request for '{id}'");

            var existingCompensation = _compensationService.GetById(id);
            if (existingCompensation == null)
                return NotFound();

            _compensationService.Replace(existingCompensation, newCompensation);

            return Ok(newCompensation);
        }
    }
}
