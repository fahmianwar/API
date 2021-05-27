using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Interface
{
    interface IProfilingRepository
    {
        IEnumerable<Profiling> Get();
        Profiling Get(int nik);
        int Insert(Profiling profiling);
        int Update(Profiling profiling);
        int Delete(int nik);
    }
}
