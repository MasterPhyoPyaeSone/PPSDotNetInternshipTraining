using Microsoft.AspNetCore.Mvc;
using RestSharp; // 🌟 RestSharp ကို ခေါ်သုံးထားပါသည်
using PPSDotNetInternshipTraining.MVCToAPI_RestSharp.Models; // 🌟 AuthorViewModel နဲ့ AuthorCreateModel ကို သုံးမယ်ဆိုတော့ ဒီ namespace ကို ခေါ်သုံးပါမယ်
using System.Linq;

namespace YourMvcApp.Controllers
{
    public class AuthorController : Controller
    {
        private readonly RestClient _client;

        // Constructor
        public AuthorController(RestClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> Index(int CurrentPage = 1)
        {
            List<AuthorViewModel> authors = new List<AuthorViewModel>();

            try
            {
                var request = new RestRequest("Author", Method.Get);
                
                // ExecuteAsync ဆိုတာက API ကို လှမ်းခေါ်ပြီး List အဖြစ် Auto ပြောင်းခိုင်းတာပါ
                var response = await _client.ExecuteAsync<List<AuthorViewModel>>(request);

                if (response.IsSuccessful && response.Data != null)
                {
                    var sortedList = response.Data.OrderByDescending(a => a.AuthorId).ToList();

                    const int pageSize = 1;
                    if (CurrentPage < 1) CurrentPage = 1;
                    int totalRecords = sortedList.Count;
                    ViewBag.TotalPages = (int)Math.Ceiling((decimal)totalRecords / pageSize);
                    ViewBag.PageSize = pageSize; // Calulate for No in Pagination
                    ViewBag.CurrentPage = CurrentPage;

                    authors = sortedList.Skip((CurrentPage - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Not able error occurred while fetching authors. Please try again later.");
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
            if (!ModelState.IsValid) return View(model);

            var request = new RestRequest("Author", Method.Post);
            
            // 🌟 AddJsonBody ဆိုတာ RestSharp ရဲ့ အသက်ပါပဲ။ Model ကို JSON အဖြစ် Auto ပြောင်းပြီး ထည့်ပေးသွားပါတယ်။
            request.AddJsonBody(model); 

            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                TempData["SuccessMessage"] = "Author created successfully!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Failed to create author. Please try again later.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var request = new RestRequest($"Author/{id}", Method.Get);
            var response = await _client.ExecuteAsync<AuthorViewModel>(request);

            if (!response.IsSuccessful || response.Data == null)
            {
                return NotFound();
            }

            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AuthorViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            // PUT Request ဆောက်ပါမယ်
            var request = new RestRequest($"Author/{id}", Method.Put);
            request.AddJsonBody(model);

            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                TempData["SuccessMessage"] = "Author Successfully Updated!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Error occurred while updating the author. Please try again later.");
            return View(model);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var request = new RestRequest($"Author/{id}", Method.Delete);
            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                TempData["SuccessMessage"] = "Delete successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Error occurred while deleting the author. Please try again later.";
            }

            return RedirectToAction("Index");
        }
    }
}