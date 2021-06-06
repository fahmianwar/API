using API.Context;
using API.Models;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using BCrypt.Net;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using Microsoft.Extensions.Configuration;

namespace API.Repository.Data
{
    public class PersonRepository : GeneralRepository<MyContext, Person, int>
    {
        private readonly MyContext conn;
        private readonly DbSet<RegisterVM> entities;
        public IConfiguration Configuration;

        public PersonRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            this.conn = myContext;
            entities = conn.Set<RegisterVM>();
            Configuration = configuration;
        }

        private static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }

        private static bool ValidatePassword(string password, string hashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashPassword);
        }

        public string GenerateTokenLogin(LoginVM loginVM)
        {
            var person = conn.Persons.Single(p => p.Email == loginVM.Email);
            var ar = conn.AccountRoles.Single(ar => ar.NIK == person.NIK);
            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, Configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("NIK", person.NIK.ToString()),
                    new Claim("Email", person.Email),
                    new Claim("role",ar.Role.RoleName)
                    //new Claim(ClaimTypes.Role,ar.Role.RoleName)
                   };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                Configuration["Jwt:Issuer"], 
                Configuration["Jwt:Audience"], 
                claims, 
                expires: DateTime.UtcNow.AddMinutes(1), 
                signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public int Login(LoginVM loginVM)
        {
            //Check EMail
            var person = conn.Persons.Single(p => p.Email == loginVM.Email);
            if (person == null)
            {
                return 404;
            }

            if (ValidatePassword(loginVM.Password, person.Account.Password))
            {
                return 1;
            }
            else
            {
                return 401;
            }
        }

        public IEnumerable<RegisterVM> GetAllProfile()
        {
            List<RegisterVM> registerVMs = new List<RegisterVM>();

            var data = (
               from p in conn.Persons
               join a in conn.Accounts on p.NIK equals a.NIK
               join pr in conn.Profilings on a.NIK equals pr.NIK
               join e in conn.Educations on pr.EducationId equals e.EducationId
               join u in conn.Universities on e.UniversityId equals u.UniversityId
               join ar in conn.AccountRoles on a.NIK equals ar.NIK
               select new RegisterVM
               {
                   NIK = p.NIK,
                   FirstName = p.FirstName,
                   LastName = p.LastName,
                   Email = p.Email,
                   Phone = p.Phone,
                   Password = a.Password,
                   BirthDate = p.BirthDate,
                   Salary = p.Salary,
                   UniversityId = u.UniversityId,
                   GPA = e.GPA,
                   Degree = e.Degree,
                   RoleId = ar.RoleId
               }
               ).ToList();
           return data;
        }
        public RegisterVM GetProfileById(int nik)
        {
            /*
            var person = conn.Persons.Find(nik);
            var account = conn.Accounts.Find(nik);
            var profiling = conn.Profilings.Find(nik);
            var education = conn.Educations.Find(profiling.EducationId);
            var university = conn.Universities.Find(education.UniversityId);
            */
            var data = (
            from p in conn.Persons
            join a in conn.Accounts on p.NIK equals a.NIK
            join pr in conn.Profilings on a.NIK equals pr.NIK
            join e in conn.Educations on pr.EducationId equals e.EducationId
            join u in conn.Universities on e.UniversityId equals u.UniversityId
            join ar in conn.AccountRoles on a.NIK equals ar.NIK
            select new RegisterVM
            {
            NIK = p.NIK,
            FirstName = p.FirstName,
            LastName = p.LastName,
            Email = p.Email,
            Phone = p.Phone,
            Password = a.Password,
            BirthDate = p.BirthDate,
            Salary = p.Salary,
            UniversityId = u.UniversityId,
            GPA = e.GPA,
            Degree = e.Degree,
            RoleId = ar.NIK
            }
            ).ToList();
            /*
            RegisterVM registerVM = new RegisterVM()
            {
                NIK = person.NIK,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Email = person.Email,
                Phone = person.Phone,
                Password = account.Password,
                BirthDate = person.BirthDate,
                Salary = person.Salary,
                UniversityName = university.UniversityName,
                GPA = education.GPA,
                Degree = education.Degree

            };
            */
            return data.FirstOrDefault(a => a.NIK == nik);
            //return this.GetAllProfile.FirstOrDefault(a => a.NIK == nik);
        }

        public int ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var person = conn.Persons.Single(p => p.Email == changePasswordVM.Email);
            if (person == null)
            {
                return 0;
            }
            var account = conn.Accounts.Find(person.NIK);
            if(ValidatePassword(changePasswordVM.Password, account.Password))
            {
                account.Password = HashPassword(changePasswordVM.NewPassword);
                conn.Accounts.Update(account);
                return conn.SaveChanges();
            }
            else
            {
                return 0;
            }

        }
        public int Register(RegisterVM registerVM)
        {
            var checkEmail = conn.Persons.FirstOrDefault(p => p.Email == registerVM.Email);
            if (checkEmail != null)
            {
                return 0;
            }

            Person person = new Person()
            {
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                BirthDate = registerVM.BirthDate,
                Email = registerVM.Email,
                Phone = registerVM.Phone,
                Salary = registerVM.Salary
            };


            conn.Add(person);
            conn.SaveChanges();
            Account account = new Account()
            {
                NIK = person.NIK,
                Password = HashPassword(registerVM.Password.ToString()).ToString()
            };
            conn.Add(account);
            conn.SaveChanges();
            /*
            University university = new University()
            {
                UniversityName = registerVM.UniversityName
            };

            conn.Add(university);
            conn.SaveChanges();
            */
            Education education = new Education()
            {
                Degree = registerVM.Degree,
                GPA = registerVM.GPA,
                UniversityId = registerVM.UniversityId

            };
            conn.Add(education);
            conn.SaveChanges();

            Profiling profiling = new Profiling()
            {
                NIK = person.NIK,
                EducationId = education.EducationId

            };
            conn.Add(profiling);
            conn.SaveChanges();

            // RoleId 2 = Employee
            AccountRole accountRole = new AccountRole()
            {
                NIK = person.NIK,
                RoleId = 2
            };
            conn.Add(accountRole);
            return conn.SaveChanges();
        }
    }
}
