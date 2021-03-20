using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using challenge.Data;

namespace challenge.Repositories
{
    public class EmployeeRespository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public EmployeeRespository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Employee Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            _employeeContext.Employees.Add(employee);
            return employee;
        }

        public Employee GetById(string id)
        {
            return _employeeContext.Employees.SingleOrDefault(e => e.EmployeeId == id);
        }

        public Employee GetById(string id, bool directReports = false)
        {
            var parentEmployee = _employeeContext.Employees;
            if (directReports)
            {
                return parentEmployee.Include(e => e.DirectReports).SingleOrDefault(e => e.EmployeeId == id);
            }
            return parentEmployee.SingleOrDefault(e => e.EmployeeId == id);
        }

        // use incase we want to remove boolean in GetById
        public Employee GetAllChildNodesbyId(string id)
        {
            return _employeeContext.Employees.AsEnumerable().Where(e => e.EmployeeId == id).SingleOrDefault();
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        public Employee Remove(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }
    }
}
