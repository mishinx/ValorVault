using Microsoft.AspNetCore.Mvc;
using ValorVault.Models;
using ValorVault.Services.UserService;
using ValorVault.Services.SourceService;
using ValorVault.Controllers;

public class SoldierInfoController : Controller
{
    private readonly ISoldierInfoService _soldierInfoService;
    private readonly IUserService _userService;
    private readonly ISourceService _sourceService;

    public SoldierInfoController(ISoldierInfoService soldierInfoService, IUserService userService, ISourceService sourceService)
    {
        _soldierInfoService = soldierInfoService;
        _userService = userService;
        _sourceService = sourceService;
    }

    [HttpGet]
    public IActionResult Adding()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddSource(Source model)
    {
        try
        {
            await _sourceService.AddSource(model);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Помилка при додаванні джерела: {ex.Message}");
        }
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

        model.birth_date = DateTime.SpecifyKind(model.birth_date, DateTimeKind.Utc);
        model.death_date = model.death_date.HasValue ? DateTime.SpecifyKind(model.death_date.Value, DateTimeKind.Utc) : (DateTime?)null;
        model.admin_ref = 2;
        model.user_ref = _userService.GetIdByEmail(AuthenticationController.user_email).Result;
        model.source_ref = 1;
        _soldierInfoService.AddSoldierInfo(model);
        return RedirectToAction("Main_registered", "Home");
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