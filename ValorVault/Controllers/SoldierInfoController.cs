using Microsoft.AspNetCore.Mvc;
using ValorVault.Models;

public class SoldierInfoController : Controller
{
    private readonly ISoldierInfoService _soldierInfoService;

    public SoldierInfoController(ISoldierInfoService soldierInfoService)
    {
        _soldierInfoService = soldierInfoService;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(SoldierInfo model)
    {
        if (ModelState.IsValid)
        {
            _soldierInfoService.AddSoldierInfo(model);
            return RedirectToAction("Index");
        }
        return View(model);
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
