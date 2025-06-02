using Kopigrad.Components;
using Kopigrad.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Features;
using Kopigrad.Components.Classes.Admin.Servise;
using Microsoft.Extensions.Options;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMudServices();

// Увеличиваем лимит для файлов (например, до 100 MB)
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = long.MaxValue; // максимальный размер тела запроса
});
// Включаем подробные ошибки
builder.Logging.SetMinimumLevel(LogLevel.Debug);
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();


builder.Services.AddDbContext<KopigradContext>(options =>
{
    options.UseMySql(
        "server=62.76.233.55;port=3306;database=kopigrad;user=blazoruser;password=newpassword123;",
        new MySqlServerVersion(new Version(10, 4, 32)), // Убедитесь, что версия правильно указана
        mysqlOptions => mysqlOptions.EnableRetryOnFailure()  // Включаем повторные попытки подключения
    );
});



builder.Services.AddHttpClient();



builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "Google";
})
.AddCookie("Cookies");

builder.Services.AddSingleton<MailSender>();


builder.Services.AddAuthorization();

builder.Services.AddMudServices();


var app = builder.Build();

// Настроим приложение на прослушивание на всех интерфейсах на порту 5000


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.MapControllers();
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
