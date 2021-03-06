using System;
using System.Threading.Tasks;
using Logic.DTOs;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Web.Models.Company;

namespace Web.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _service;
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ICompanyService service, ILogger<CompanyController> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var companies = await _service.GetAll();
            
            return View(new IndexModel
            {
                Companies = companies
            });
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var legalForms = await _service.GetLegalForms();
            
            ViewBag.LegalForms = new SelectList(legalForms, "Id", "Title");
            ViewBag.Title = "Добавить";
            
            return View(new CompanyDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyDTO dto)
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
            var legalForms = await _service.GetLegalForms();
            
            ViewBag.LegalForms = new SelectList(legalForms, "Id", "Title");
            ViewBag.Title = "Добавить";
            
            return View(dto);
        }

        [Route("Company/Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);

            if (result)
            {
                return RedirectToAction("Index");
            }
            
            ModelState.AddModelError("", "Произошла ошибка");
            
            var companies = await _service.GetAll();
            
            return View("Index", new IndexModel
            {
                Companies = companies
            });
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var legalForms = _service.GetLegalForms();
            
            var dto = _service.GetById(id);

            await Task.WhenAll(legalForms, dto);
            
            ViewBag.LegalForms = new SelectList(legalForms.Result, "Id", "Title");
            ViewBag.Title = "Редактировать";
            
            return View("Create", dto.Result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CompanyDTO dto)
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
            var legalForms = await _service.GetLegalForms();
            
            ViewBag.LegalForms = new SelectList(legalForms, "Id", "Title");
            ViewBag.Title = "Редактировать";
            
            return View("Create", dto);
        }
    }
}