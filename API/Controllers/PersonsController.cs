using API.Base;
using API.Models;
using API.Repository.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : BaseController<Person, PersonRepository, int>
    {
        private readonly PersonRepository personRepository;
        public IConfiguration Configuration { get; }
        public PersonsController(PersonRepository personRepository) : base(personRepository)
        {
            this.personRepository = personRepository;
        }
        //[Authorize(Roles = "Admin, Employee")]
        [HttpGet("GetAllProfile")]
        // Nama Policy yang diijinkan "AllowOrigin"
        [EnableCors("AllowOrigin")]
        public ActionResult GetAllProfile()
        {
            var get = personRepository.GetAllProfile();
            if (get != null)
            {
                return Ok(get);
            }
            else
            {
                return NotFound("Data tidak ditemukan");
            }
        }
        [Authorize(Roles = "Admin, Employee")]
        [HttpGet("GetProfileById/{nik}")]
        public ActionResult GetProfileById(int nik)
        {
            var get = personRepository.GetProfileById(nik);
            if (get != null)
            {
                return Ok(get);
            }
            else
            {
                return NotFound("Data tidak ditemukan");
            }
        }
        //[Route("login")]
        [HttpPost("login")]
        public ActionResult Login(LoginVM loginVM)
        {
            var login = personRepository.Login(loginVM);
            if (login == 404)
            {
                return BadRequest("Email tidak ditemukan, Silakan gunakan email lain");
            }
            else if (login == 401)
            {
                return BadRequest("Password salah");
            }
            else if(login == 1)
            {
                return Ok("Berhasil login, Token : " + personRepository.GenerateTokenLogin(loginVM));
                
            }
            else
            {
                return BadRequest("Gagal login");
            }
        }
        //[Route("register")]
        [HttpPost("register")]
        public ActionResult Register(RegisterVM registerVM)
        {
            var register = personRepository.Register(registerVM);
            if (register > 0)
            {
                return Ok("Data berhasil ditambah");
            }
            else
            {
                return BadRequest("Data gagal ditambah");
            }
        }
        [Authorize(Roles = "Admin, Employee")]
        //[Route("changePassword")]
        [HttpPost("changePassword")]
        public ActionResult ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var changePassword = personRepository.ChangePassword(changePasswordVM);
            if (changePassword == 1)
            {
                return Ok("Berhasil mengganti password");
            }
            else if (changePassword == 2)
            {
                return BadRequest("Email tidak ditemukan");
            }
            else if (changePassword == 3)
            {
                return BadRequest("Password salah");
            }
            else
            {
                return BadRequest("Gagal mengganti password");
            }
        }
    }
}
