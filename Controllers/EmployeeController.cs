using CosmosDBTutorial.Models;
using CosmosDBTutorial.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CosmosDBTutorial.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ICosmosDBService _cosmosDbService;
        public EmployeeController(ICosmosDBService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [ActionName("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _cosmosDbService.GetItemsAsync("SELECT * FROM c"));
        }

        [ActionName("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind("Id,Name,Age,DOB,Manager")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.Id = Guid.NewGuid().ToString();
                await _cosmosDbService.AddItemAsync(employee);
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind("Id,Name,Age,DOB,Manager")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _cosmosDbService.UpdateItemAsync(employee.Id, employee);
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Employee employee = await _cosmosDbService.GetItemAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Employee employee = await _cosmosDbService.GetItemAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedAsync([Bind("Id")] string id)
        {
            await _cosmosDbService.DeleteItemAsync(id);
            return RedirectToAction("Index");
        }

        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(string id)
        {
            return View(await _cosmosDbService.GetItemAsync(id));
        }
    }
}