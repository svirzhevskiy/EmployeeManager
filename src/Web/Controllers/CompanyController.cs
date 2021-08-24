using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Logic.DTOs;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Models.Company;

namespace Web.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _service;

        public CompanyController(ICompanyService service)
        {
            _service = service;
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
        public IActionResult Create()
        {
            ViewBag.LegalForms = new SelectList(new List<LegalFormDTO>
            {
                new () { Id = 1, Title = "OOO" },
                new () { Id = 2, Title = "OAO" }
            }, "Id", "Title");

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
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(dto);
        }

        [HttpGet("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.LegalForms = new SelectList(new List<LegalFormDTO>
            {
                new () { Id = 1, Title = "OOO" },
                new () { Id = 2, Title = "OAO" }
            }, "Id", "Title");

            ViewBag.Title = "Редактировать";

            var dto = await _service.GetById(id);
            
            return View("Create", dto);
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
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View("Create", dto);
        }
    }
}