using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoranaRegistration.Constants;
using CoranaRegistration.Models;
using CoranaRegistration.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoranaRegistration.Controllers
{
    public class PageController : Controller
    {
        private readonly IPersonRegistrationService _personRegistrationService;
        

        public PageController(IPersonRegistrationService personRegistrationService)
        {
            _personRegistrationService = personRegistrationService;
            
        }
        public IActionResult Index()
        {
            var sum = _personRegistrationService.GetCovidNumberAsync().Result;
            var positivs = _personRegistrationService.GetCovidNumberRegistrationsAsync ().Result;
            var pagination = new Pagination(sum, Contants.PageSize,positivs );
            return View(pagination );
        }
    }
}
