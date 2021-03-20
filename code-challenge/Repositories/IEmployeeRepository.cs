using challenge.Models;
using System;
using System.Threading.Tasks;

namespace challenge.Repositories
{
    public interface IEmployeeRepository
    {
        Employee GetById(String id);
        Employee Add(Employee employee);
        Employee Remove(Employee employee);
        Employee GetById(String id, bool directReports);
        Employee GetAllChildNodesbyId(String id);
        Task SaveAsync();
    }
}