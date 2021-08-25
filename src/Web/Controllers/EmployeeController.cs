using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Logic.DTOs;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Web.Models;
using Web.Models.Employee;

namespace Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeService _service;

        private const int ItemsPerPage = 5;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService service)
        {
            _logger = logger;
            _service = service;
        }
        
        public async Task<IActionResult> Index(int page = 1, string filter = "")
        {
            var employees = _service.GetAll(page, ItemsPerPage, filter);
            var total = _service.CountEmployees(filter);

            await Task.WhenAll(employees, total);
            
            return View(new IndexModel
            {
                Employees = employees.Result,
                ItemsPerPage = ItemsPerPage,
                PageNumber = page,
                TotalItems = total.Result,
                Filter = filter
            });
        }
        
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var positions = _service.GetPositions();
            var companies = _service.GetCompanies();

            await Task.WhenAll(positions, companies);

            ViewBag.Companies = new SelectList(companies.Result, "Id", "Title");
            ViewBag.Positions = new SelectList(positions.Result, "Id", "Title");
            ViewBag.Title = "Добавить";
            
            return View(new EmployeeDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _service.Create(dto);
                    if (result)
                        return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}/n{ex.StackTrace}");
                ModelState.AddModelError("", "Произошла ошибка");
            }
            var positions = _service.GetPositions();
            var companies = _service.GetCompanies();

            await Task.WhenAll(positions, companies);

            ViewBag.Companies = new SelectList(companies.Result, "Id", "Title");
            ViewBag.Positions = new SelectList(positions.Result, "Id", "Title");
            ViewBag.Title = "Добавить";
            
            return View(dto);
        }

        [Route("Employee/Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);

            if (result)
            {
                return RedirectToAction("Index");
            }
            
            ModelState.AddModelError("", "Произошла ошибка");
            
            var employees = await _service.GetAll(1, ItemsPerPage, "");
            
            return View("Index", new IndexModel
            {
                Employees = employees
            });
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var positions = _service.GetPositions();
            var companies = _service.GetCompanies();
            var dto = _service.GetById(id);

            await Task.WhenAll(positions, dto, companies);

            ViewBag.Companies = new SelectList(companies.Result, "Id", "Title");
            ViewBag.Positions = new SelectList(positions.Result, "Id", "Title");
            ViewBag.Title = "Редактировать";
            
            return View("Create", dto.Result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(EmployeeDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _service.Update(dto);
                    if (result)
                        return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}/n{ex.StackTrace}");
                ModelState.AddModelError("", "Произошла ошибка");
            }
            var positions = _service.GetPositions();
            var companies = _service.GetCompanies();

            await Task.WhenAll(positions, companies);

            ViewBag.Companies = new SelectList(companies.Result, "Id", "Title");
            ViewBag.Positions = new SelectList(positions.Result, "Id", "Title");
            ViewBag.Title = "Редактировать";
            
            return View("Create", dto);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}