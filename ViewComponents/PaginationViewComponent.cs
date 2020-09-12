using CoranaRegistration.Constants;
using CoranaRegistration.Models;
using CoranaRegistration.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoranaRegistration.ViewComponents
{
    public class PaginationViewComponent : ViewComponent
    {
        private readonly IPersonRegistrationService _personRegistrationService;
        public PaginationViewComponent(IPersonRegistrationService personRegistrationService)
        {
            _personRegistrationService = personRegistrationService;
        }
        public IViewComponentResult Invoke()
        {
            var sum  = _personRegistrationService.GetCovidNumberRegistrationsAsync().Result; // hmm .Result er vist ikke ok ?
            var positivs = _personRegistrationService.GetCovidNumberAsync().Result;
            var  pagination = new Pagination(sum, Contants.PageSize, positivs);
            return View("Default", pagination);
        }
    }
}
