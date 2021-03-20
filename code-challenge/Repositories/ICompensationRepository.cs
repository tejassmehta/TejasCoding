using challenge.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace challenge.Repositories
{
    public interface ICompensationRepository
    {
        Compensation GetById(String id);
        List<Compensation> GetAll();
        Compensation Add(Compensation compensation);
        Compensation Remove(Compensation compensation);
        Task SaveAsync();
    }
}