using API.Context;
using API.Models;

namespace API.Repository.Data
{
    public class ProfilingRepository : GeneralRepository<MyContext, Profiling, int>
    {
        public ProfilingRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
