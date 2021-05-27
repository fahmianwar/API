using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Interface
{
    interface IUniversityRepository
    {
        IEnumerable<University> Get();
        University Get(int Id);
        int Insert(University university);
        int Update(University university);
        int Delete(int Id);
    }
}