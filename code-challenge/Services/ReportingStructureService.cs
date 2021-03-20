using challenge.Models;
using challenge.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace challenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {
        private readonly ILogger<IReportingStructureService> _logger;
        private readonly IEmployeeRepository _employeeRepository;

        public ReportingStructureService(ILogger<IReportingStructureService> logger, IEmployeeRepository employeeRepository)
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
        }

        // Fetch all the employee's reporting to the specific ID
        public ReportingStructure GetReportEmployees(string employeeId)
        {
            try
            {
                Employee employee = _employeeRepository.GetById(employeeId, true);
                int numberOfReports = 0;
                var stack = new Stack<Employee>();
                // Depth-first search to find all the child nodes.
                if (employee != null)
                {
                    stack.Push(employee);
                    while (stack.Count != 0)
                    {
                        var current = stack.Pop();

                        foreach (Employee directReport in current.DirectReports ?? new List<Employee>())
                        {
                            Employee childEmp = _employeeRepository.GetById(directReport.EmployeeId, true);
                            numberOfReports += 1;
                            current.DirectReports.Append(directReport);
                            stack.Push(directReport);
                        }
                    }

                    ReportingStructure reportingStructure = new ReportingStructure() { Employee = employee, NumberOfReports = numberOfReports };

                    return reportingStructure;

                }
                else
                {
                    _logger.LogError($"Employee [Id: '{employeeId}'] not found.");
                    return null;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

    }
}
