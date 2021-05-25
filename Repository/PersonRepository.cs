using API.Context;
using API.Models;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly MyContext conn;
        public PersonRepository(MyContext conn)
        {
            this.conn = conn;
        }
        public int Delete(int nik)
        {
            //Person person = conn.Persons.FirstOrDefault(m => m.NIK == nik);
            Person person = conn.Persons.Find(nik);
            conn.Remove(person);
            int result = conn.SaveChanges();
            return result;
        }

        public IEnumerable<Person> Get()
        {
            return conn.Persons.ToList();
        }

        public Person Get(int nik)
        {
            //return conn.Persons.FirstOrDefault(m => m.NIK == nik);
            return conn.Persons.Find(nik);
        }

        public int Insert(Person person)
        {
            conn.Persons.Add(person);
            int result = conn.SaveChanges();
            return result;
        }

        public int Update(Person person)
        {
            /*
            int nik = person.NIK;
            Person oldPerson = conn.Persons.Single(m => m.NIK == nik);
            if (person.FirstName != null) {
                oldPerson.FirstName = person.FirstName;
            }
            if (person.LastName != null) {
                oldPerson.LastName = person.LastName;
            }
            if (person.BirthDate != null) {
                oldPerson.BirthDate = person.BirthDate;
            }
            if (person.Phone != null) {
                oldPerson.Phone = person.Phone;
            }
            if (person.Salary < 0)
            {
                oldPerson.Salary = person.Salary;
            }
            if (person.Email != null)
            {
                oldPerson.Email = person.Email;
            }
                int result = conn.SaveChanges();
                return result;
            */
            try
            {
                //conn.Entry(person).State = EntityState.Modified;
                //conn.Attach(person);
                conn.Entry(person).Property("FirstName").IsModified = true;
                conn.Entry(person).Property("LastName").IsModified = true;
                conn.Entry(person).Property("Phone").IsModified = true;
                conn.Entry(person).Property("Salary").IsModified = true;
                conn.Entry(person).Property("Email").IsModified = true;
                int result = conn.SaveChanges();
                return result;
            }
            catch (Exception)
            {

                return 0;
            }

            
        }
    }
}
