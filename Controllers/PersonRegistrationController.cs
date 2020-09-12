using System;
using System.Threading.Tasks;
using CoranaRegistration.Models;
using CoranaRegistration.Services;
using Microsoft.AspNetCore.Mvc;
using CoranaRegistration.Constants;

namespace CoranaRegistration.Controllers
{
    
    public class PersonRegistrationController : Controller
    {
        private readonly IPersonRegistrationService _personRegistrationService;
        private const string BindValues = "id,cpr,fullname,adress,zipcode,testresult";
        private const string BindValuesCreate = "cpr,fullname,adress,zipcode,testresult";

        //private string BindValues
        //{
        //    get {
        //        var properties = typeof(PersonRegistration).GetProperties();
        //        var result = string.Join(',', typeof(PersonRegistration).GetProperties().Where(w => w.Name != "Id").Select(s => s.Name));
        //        return result;
        //    }
        //}        

        public PersonRegistrationController(IPersonRegistrationService personRegistrationService)
        {
            _personRegistrationService = personRegistrationService;
         }

        [ActionName("Index")]
        public async Task<IActionResult> Index(int? page)
        {
            int pageNumber = (page == null) ? 0 : page.Value - 1;
            int offSet = pageNumber * Contants.PageSize;
            ViewBag.PageNumber = pageNumber + 1;
            return View(await _personRegistrationService.GetItemsAsync($"select * from c order by c.{nameof(PersonRegistration.fullname)} OFFSET {offSet} LIMIT {Contants.PageSize}"));
        }
 

        [ActionName("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> CreateAsync([Bind(BindValuesCreate)] PersonRegistration personRegistration) 
        { 
            if (ModelState.IsValid)
            {
                personRegistration.id = Guid.NewGuid().ToString();
                await _personRegistrationService.AddItemAsync(personRegistration);
                return RedirectToAction("Index");
            }
            return View(personRegistration);
        }
        [HttpPost] //[HttpPut] ! hvad ! den virker ikke med HttpPut , men det er da en update=put - men vi rammer jo også en upsert, det er måske grunden
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync([Bind(BindValues)] PersonRegistration personRegistration)
        {
            if (ModelState.IsValid)
            {
                await _personRegistrationService.UpdateItemAsync (personRegistration.id , personRegistration);
                return RedirectToAction("Index");
            }
            return View(personRegistration);
        }

        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var person= await _personRegistrationService.GetItemAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync([Bind("id")] string id)
        {
            return View(await _personRegistrationService.GetItemAsync(id));
        }

        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var person = await _personRegistrationService.GetItemAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpDelete]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedAsync([Bind("Id")] string id)
        {
            await this._personRegistrationService.DeleteItemAsync(id);
            return RedirectToAction("Index");
        }
    }
}
