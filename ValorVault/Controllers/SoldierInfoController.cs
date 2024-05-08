using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using ValorVault.Models;
using ValorVault.Services;

public class SoldierInfoController : Controller
{
    private readonly ISoldierInfoService _soldierInfoService;

    public SoldierInfoController(ISoldierInfoService soldierInfoService)
    {
        _soldierInfoService = soldierInfoService;
    }

    [HttpGet]
    public IActionResult Adding()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Adding(SoldierInfo model, IFormFile photo)
    {
        if (photo != null && photo.Length > 0)
        {
            var photoBytes = await ImageFormatter.ConvertToByteArray(photo);
            if (photoBytes != null)
            {
                model.photo = photoBytes;
            }
            else
            {
                ModelState.AddModelError("Photo", "There was a problem converting the photo.");
            }
        }
        else
        {
            ModelState.AddModelError("Photo", "Photo is required.");
        }
        
        model.birth_date = model.birth_date.ToUniversalTime();
        model.admin_ref = 2;
        model.user_ref = 23;
        model.source_ref = 1;
        _soldierInfoService.AddSoldierInfo(model);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var soldierInfo = _soldierInfoService.GetSoldierInfoById(id);
        if (soldierInfo == null)
        {
            return NotFound();
        }
        return View(soldierInfo);
    }

    [HttpPost]
    public IActionResult Edit(SoldierInfo model)
    {
        if (ModelState.IsValid)
        {
            _soldierInfoService.UpdateSoldierInfo(model);
            return RedirectToAction("Index");
        }
        return View(model);
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        _soldierInfoService.DeleteSoldierInfo(id);
        return RedirectToAction("Index");
    }
}