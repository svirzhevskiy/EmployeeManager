using System;
using System.Threading.Tasks;
using Logic.DTOs;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Web.Models.Company;

namespace Web.Controllers;

public class CompanyController(ICompanyService service, ILogger<CompanyController> logger) : Controller
{
    public async Task<IActionResult> Index()
    {
        var companies = await service.GetAll();
            
        return View(new IndexModel
        {
            Companies = companies
        });
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var legalForms = await service.GetLegalForms();
            
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
                var result = await service.Create(dto);
                if (result)
                {
                    return RedirectToAction("Index");
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError($"{ex.Message}/n{ex.StackTrace}");
            ModelState.AddModelError("", "Произошла ошибка");
        }
        var legalForms = await service.GetLegalForms();
            
        ViewBag.LegalForms = new SelectList(legalForms, "Id", "Title");
        ViewBag.Title = "Добавить";
            
        return View(dto);
    }

    [Route("Company/Delete/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await service.Delete(id);

        if (result)
        {
            return RedirectToAction("Index");
        }
            
        ModelState.AddModelError("", "Произошла ошибка");
            
        var companies = await service.GetAll();
            
        return View("Index", new IndexModel
        {
            Companies = companies
        });
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var legalForms = service.GetLegalForms();
            
        var dto = service.GetById(id);

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
                var result = await service.Update(dto);
                if (result)
                {
                    return RedirectToAction("Index");
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError($"{ex.Message}/n{ex.StackTrace}");
            ModelState.AddModelError("", "Произошла ошибка");
        }
        var legalForms = await service.GetLegalForms();
            
        ViewBag.LegalForms = new SelectList(legalForms, "Id", "Title");
        ViewBag.Title = "Редактировать";
            
        return View("Create", dto);
    }
}