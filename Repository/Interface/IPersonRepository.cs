using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
