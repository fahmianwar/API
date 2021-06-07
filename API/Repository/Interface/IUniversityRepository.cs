using API.Models;
using System.Collections.Generic;

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