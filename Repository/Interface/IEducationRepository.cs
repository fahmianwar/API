using API.Models;
using System.Collections.Generic;

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
