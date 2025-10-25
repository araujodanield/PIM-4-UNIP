using WebChamado.Services; // 🟢 NOVO: Necessário para usar o ChamadoApiService
using System.Net.Http.Headers; // 🟢 NOVO: Necessário para MediaTypeWithQualityHeaderValue
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<ChamadoApiService>(client =>
{
    // Define a URL base para sua API
    client.BaseAddress = new Uri("https://apipim-anfwgmdah3fre6ca.brazilsouth-01.azurewebsites.net/api/");

    // Configura o cabeçalho Accept esperando JSON na resposta (Correção do Erro 1)
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
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
