using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Interface
{
    interface IEducationRepository
    {
        IEnumerable<Education> Get();
        Education Get(int Id);
        int Insert(Education education);
        int Update(Education education);
        int Delete(int Id);
    }
}
