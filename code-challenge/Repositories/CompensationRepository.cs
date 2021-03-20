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
    public class CompensationRespository : ICompensationRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRespository(ILogger<ICompensationRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Compensation Add(Compensation compensation)
        {
            Employee employee = _employeeContext.Employees.Where(e => e.EmployeeId.Equals(compensation.EmployeeId)).FirstOrDefault();

            if (employee == null)
            {
                throw new Exception("Must be a valid employee.");
            }
            else if (GetById(compensation.EmployeeId) != null)
            {
                throw new Exception("Employee already has a compensation.");
            }

            compensation.CompensationId = Guid.NewGuid().ToString();
            _employeeContext.Compensations.Add(compensation);
            return compensation;
        }

        public Compensation GetById(string id)
        {
            return _employeeContext.Compensations.AsEnumerable().Where(c => c.EmployeeId == id).SingleOrDefault();
        }

        public List<Compensation> GetAll()
        {
            return _employeeContext.Compensations.ToList();
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        public Compensation Remove(Compensation compensation)
        {
            return _employeeContext.Remove(compensation).Entity;
        }
    }
}