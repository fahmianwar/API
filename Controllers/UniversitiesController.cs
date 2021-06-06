﻿using API.Base;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversitiesController : BaseController<University, UniversityRepository, int>
    {
        private readonly UniversityRepository universityRepository;
        public UniversitiesController(UniversityRepository universityRepository) : base(universityRepository)
        {
            this.universityRepository = universityRepository;
        }
    }
}
