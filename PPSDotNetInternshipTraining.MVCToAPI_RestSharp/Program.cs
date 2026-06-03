using RestSharp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// ၁။ အရင်ဆုံး IHttpClientFactory အတွက် Base URL ကို မှတ်ထားပါမယ်
builder.Services.AddHttpClient("MyApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5107/api/");
});
// ၂။ 🌟 RestSharp (RestClient) ကို DI အဖြစ် ထည့်သွင်းခြင်း
builder.Services.AddTransient<RestClient>(sp =>
{
    // Program ထဲက IHttpClientFactory ကို လှမ်းခေါ်ပါတယ်
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    
    // "MyApi" နာမည်နဲ့ အသင့်လုပ်ထားတဲ့ HttpClient ကို ယူပါတယ်
    var httpClient = factory.CreateClient("MyApi");
    
    // ရလာတဲ့ httpClient ကို RestSharp ထဲကို ထည့်ပြီး ပြန်ပေးလိုက်ပါတယ်
    return new RestClient(httpClient); 
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
