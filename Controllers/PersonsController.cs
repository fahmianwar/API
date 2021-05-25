using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class PersonsController : ControllerBase
    {
        private readonly PersonRepository personRepository;
        public PersonsController(PersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        [HttpPost]
        public ActionResult Post(Person person)
        {
            var post = personRepository.Insert(person);
            if (post > 0)
            {
                return Ok("Data berhasil ditambah");
            }
            else
            {
                return BadRequest("Data gagal ditambah");
            }
            
        }

        [HttpGet]
        public ActionResult Get()
        {
            var get = personRepository.Get();
            if (get.Count() > 0)
            {
                return Ok(get);
            }
            else
            {
                return NotFound("Data tidak ditemukan");
            }
        }

        [HttpGet("{nik}")]
        public ActionResult Get(int nik)
        {
            var get = personRepository.Get(nik);
            if (get != null)
            {
                return Ok(get);
            }
            else
            {
                return NotFound("Data tidak ditemukan");
            }
        }

        [HttpPut]
        public ActionResult Update(Person person)
        {
            var get = personRepository.Update(person);
            if (get > 0)
            {
                return Ok("Data berhasil diubah");
            }
            else
            {
                return BadRequest("Data gagal diubah");
            }
        }

        [HttpDelete("{nik}")]
        public ActionResult Delete(int nik)
        {
            var get = personRepository.Delete(nik);
            if (get > 0)
            {
                return Ok("Data berhasil dihapus");
            }
            else
            {
                return BadRequest("Data gagal dihapus");
            }
        }
    }
}
