using WebChamado.Services; 
using System.Net.Http.Headers; 
var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<ChamadoApiService>(client =>
{
    // Define a URL base para a API do Banco de Dados
    client.BaseAddress = new Uri("https://apipim-anfwgmdah3fre6ca.brazilsouth-01.azurewebsites.net/api/");

   
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
});

// Configuração para a API do Google Gemini
builder.Services.AddHttpClient<GeminiApiService>(client =>
{
    // A URL base da API do Gemini para o endpoint generateContent
    client.BaseAddress = new Uri("https://generativelanguage.googleapis.com/v1beta/models/");

    
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddHttpClient<RespostaIaApiService>(client =>
{
    client.BaseAddress = new Uri("https://apipim-anfwgmdah3fre6ca.brazilsouth-01.azurewebsites.net/api/");
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
});

var app = builder.Build();


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
