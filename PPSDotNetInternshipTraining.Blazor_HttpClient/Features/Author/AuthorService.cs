using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using PPSDotNetInternshipTraining.Blazor_HttpClient.Features.Shared.Models;

namespace PPSDotNetInternshipTraining.Blazor_HttpClient.Features.Author
{
    public class AuthorService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AuthorService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<PaginatedResponse<AuthorResponseModel>> GetAuthorsAsync(int currentPage = 1)
        {
            // Page Size သတ်မှတ်ခြင်း (၁ မျက်နှာလျှင် ဘယ်နှစ်ကြောင်းပြမည်လဲ)
            const int pageSize = 2; // သင့် Code တွင် 1 ဟုထားသော်လည်း လက်တွေ့တွင် 10 ခန့် ထားလေ့ရှိသည်
            if (currentPage < 1) currentPage = 1;

            var client = _httpClientFactory.CreateClient("LibraryApi");
            var result = await client.GetFromJsonAsync<List<AuthorResponseModel>>("Author");

            // ပြန်ပို့ပေးမည့် Object အသစ်တည်ဆောက်ခြင်း
            var response = new PaginatedResponse<AuthorResponseModel>();

            if (result != null)
            {
                // 1. အသစ်ဆုံး ID ကို အပေါ်ဆုံးတင်ရန် Sort လုပ်ပါ
                var sortedAuthors = result.OrderByDescending(x => x.Id).ToList();

                // 2. Pagination တွက်ချက်ပါ (ViewBag အစား response object ထဲသို့ ထည့်ပါ)
                int recsCount = sortedAuthors.Count;
                response.TotalPages = (int)Math.Ceiling((decimal)recsCount / pageSize);
                response.CurrentPage = currentPage;

                // 3. လက်ရှိ Page အတွက် လိုအပ်သော Data ကိုသာ ဖြတ်ယူပါ
                response.Data = sortedAuthors.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            }

            // List အစား ထုပ်ပိုးထားသော response object ကြီးကို ပြန်ပို့ပါ
            return response;
        }
        public async Task CreateAuthorAsync(AuthorRequestModel model)
        {
            var client = _httpClientFactory.CreateClient("LibraryApi");
            await client.PostAsJsonAsync("Author", model);
        }
        public async Task UpdateAuthorAsync(int id, AuthorRequestModel model)
        {
            var client = _httpClientFactory.CreateClient("LibraryApi");
            await client.PutAsJsonAsync($"Author/{id}", model);
        }
        // Author တစ်ယောက်တည်း၏ အချက်အလက်ကို ID ဖြင့် ယူရန်
        public async Task<AuthorResponseModel?> GetAuthorByIdAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("LibraryApi");
            return await client.GetFromJsonAsync<AuthorResponseModel>($"Author/{id}");
        }
        public async Task DeleteAuthorAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("LibraryApi");
            await client.DeleteAsync($"Author/{id}");
        }
    }
}