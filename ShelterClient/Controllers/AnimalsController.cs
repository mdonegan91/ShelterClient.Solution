using Microsoft.AspNetCore.Mvc;
using ShelterClient.Models;
using Newtonsoft.Json.Linq;

namespace ShelterClient.Controllers;

public class AnimalsController : Controller
{
  public async Task<IActionResult> Index(int page = 1, int pageSize = 6)
  {
    Animal destination = new Animal();
    List<Animal> destList = new List<Animal> { };
    using (var httpClient = new HttpClient())
    {
      using (var response = await httpClient.GetAsync($"https://localhost:5001/api/Animals?page={page}&pagesize={pageSize}"))
      {
        var destContent = await response.Content.ReadAsStringAsync();
        JArray destArray = JArray.Parse(destContent);
        destList = destArray.ToObject<List<Animal>>();
      }
    }

    ViewBag.TotalPages = destList.Count();
    ViewBag.CurrentPage = page;
    ViewBag.PageSize = pageSize;

    return View(destList);
  }

  public IActionResult Details(int id)
  {
    Animal destination = Animal.GetDetails(id);
    return View(destination);
  }

  public ActionResult Create()
  {
    return View();
  }

  [HttpPost]
  public ActionResult Create(Animal destination)
  {
    Animal.Post(destination);
    return RedirectToAction("Index");
  }

  public ActionResult Edit(int id)
  {
    Animal destination = Animal.GetDetails(id);
    return View(destination);
  }

  [HttpPost]
  public ActionResult Edit(Animal destination)
  {
    Animal.Put(destination);
    return RedirectToAction("Details", new { id = destination.AnimalId });
  }

  public ActionResult Delete(int id)
  {
    Animal destination = Animal.GetDetails(id);
    return View(destination);
  }

  [HttpPost, ActionName("Delete")]
  public ActionResult DeleteConfirmed(int id)
  {
    Animal.Delete(id);
    return RedirectToAction("Index");
  }
}