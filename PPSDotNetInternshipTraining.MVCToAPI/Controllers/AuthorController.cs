
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PPSDotNetInternshipTraining.MVCToAPI.Models;


public class AuthorController : Controller
{
    private readonly HttpClient _httpClient;
    public AuthorController(IHttpClientFactory httpClientFactory)
    {
        // Program.cs မှာ မှတ်ထားတဲ့ "MyApi" ကို လှမ်းယူလိုက်ပါတယ်
        _httpClient = httpClientFactory.CreateClient("MyApi");
    }

  
    public async Task<IActionResult> Index(int CurrentPage = 1)
    {
        List<AuthorViewModel> authors = new List<AuthorViewModel>();

        try
        {
            var result = await _httpClient.GetFromJsonAsync<List<AuthorViewModel>>("Author");
            if (result != null)
            {
                authors = result.OrderByDescending(a => a.AuthorId).ToList();
                const int pageSize = 1;
                if (CurrentPage < 1) CurrentPage = 1;
                int recsCount = authors.Count();
                int totalPages = (int)Math.Ceiling((decimal)recsCount / pageSize);
                ViewBag.CurrentPage = CurrentPage;
                ViewBag.PageSize = pageSize;
                ViewBag.TotalPages = totalPages;
                authors = authors.Skip((CurrentPage - 1) * pageSize).Take(pageSize).ToList();
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Not able error occurred while fetching authors. Please try again later.");

            Console.WriteLine(ex.Message);
        }
        return View(authors);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(AuthorCreateModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        try
        {
            var response = await _httpClient.PostAsJsonAsync("Author", model);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Author created successfully!";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Failed to create author. Please try again later.");

        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while creating the author. Please try again later.");
            Console.WriteLine(ex.Message);
            return View(model);
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<AuthorEditModel>($"Author/{id}");
            if (response is null)
            {
                return NotFound();
            }
            return View(response);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while fetching the author details. Please try again later.");
            Console.WriteLine(ex.Message);
            return RedirectToAction("Index");
        }
    }

    public async Task<IActionResult> Edit(int id, AuthorEditModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"Author/{id}", model);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Author updated successfully!";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Failed to update author. Please try again later.");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while updating the author. Please try again later.");
            Console.WriteLine(ex.Message);
            return View(model);
        }
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"Author/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Author deleted successfully!";
                return RedirectToAction("Index");
            }
        }
        catch
        {
            ModelState.AddModelError(string.Empty, "An error occurred while deleting the author. Please try again later.");
        }
        return View();
    }
}