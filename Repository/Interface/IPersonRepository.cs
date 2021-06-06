using API.Models;
using System.Collections.Generic;

namespace API.Repository.Interface
{
    interface IPersonRepository
    {
        IEnumerable<Person> Get();
        Person Get(int nik);
        int Insert(Person person);
        int Update(Person person);
        int Delete(int nik);
    }
}
