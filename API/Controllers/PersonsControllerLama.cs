using API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PersonsControllera : ControllerBase
    {
        private readonly PersonRepository personRepository;
        public PersonsControllera(PersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }


    }
}
