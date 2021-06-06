using API.Base;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilingsController : BaseController<Profiling, ProfilingRepository, int>
    {
        private readonly ProfilingRepository profilingRepository;
        public ProfilingsController(ProfilingRepository profilingRepository) : base(profilingRepository)
        {
            this.profilingRepository = profilingRepository;
        }
    }
}
