using Kopigrad.Components;
using Kopigrad.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Features;
using Kopigrad.Components.Classes.Admin.Servise;
using Kopigrad.Components.Classes.Autorisation;
using Microsoft.Extensions.Options;
using MudBlazor.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Подключаем MudBlazor
builder.Services.AddMudServices();

// Увеличиваем лимит для загрузки файлов
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = long.MaxValue;
});

// Логи
builder.Logging.SetMinimumLevel(LogLevel.Debug);
builder.Logging.AddConsole();

// Blazor Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddControllers();

// Чтобы иметь доступ к HttpContext внутри компонентов
builder.Services.AddHttpContextAccessor();
//builder.WebHost.UseUrls("http://0.0.0.0:5000");
// DbContext
builder.Services.AddDbContext<KopigradContext>(options =>
{
    options.UseMySql(
        //"server=62.76.233.55;port=3306;database=kopigrad;user=blazoruser;password=newpassword123;",
        "server=localhost;port=3306;database=copygrad;user=root;password=;",
        new MySqlServerVersion(new Version(10, 4, 32)),
        mysqlOptions => mysqlOptions.EnableRetryOnFailure()
    );
});

// HttpClient, если нужно
builder.Services.AddHttpClient();

// Cookie-аутентификация
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "KopigradAuth";
        options.LoginPath = "/Admin";
        options.ExpireTimeSpan = TimeSpan.FromHours(2);
        options.SlidingExpiration = true;
    });

// Authorization для [Authorize]
builder.Services.AddAuthorization();

// Регистрируем класс авторизации
builder.Services.AddScoped<AutorisationClasses>();

// Класс для отправки почты (если используется)
builder.Services.AddSingleton<MailSender>();

var app = builder.Build();

// В режиме Production отображаем страницу ошибок и HSTS
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Включаем маршрутизацию
app.UseRouting();

// Аутентификация/авторизация
app.UseAuthentication();
app.UseAuthorization();

// Middleware для Anti-Forgery
app.UseAntiforgery();

app.MapGet("/doLogout", async (HttpContext httpContext) =>
{
    await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    httpContext.Response.Redirect("/");
});

// Минимальный API-эндпоинт для логина
app.MapGet("/doLogin", async (HttpContext httpContext, AutorisationClasses authService) =>
{
    var request = httpContext.Request;
    var response = httpContext.Response;

    // Читаем параметры из query-string
    var email = request.Query["email"].ToString();
    var password = request.Query["password"].ToString();
    var returnUrl = request.Query["returnUrl"].ToString();

    // Проверяем пароль
    string checkResult = authService.CheakPassword(email, password);
    if (!string.IsNullOrEmpty(checkResult))
    {
        response.ContentType = "text/html; charset=utf-8";
        await response.WriteAsync($@"
            <h3 style=""color:red;"">Ошибка входа: {checkResult}</h3>
            <p><a href=""/Admin"">Вернуться к форме входа</a></p>");
        return;
    }

    // Создаём куки-аутентификацию
    var claims = new List<Claim> { new Claim(ClaimTypes.Name, email) };
    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    var principal = new ClaimsPrincipal(identity);

    await httpContext.SignInAsync(
        CookieAuthenticationDefaults.AuthenticationScheme,
        principal,
        new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2)
        });

    // Редиректим на returnUrl или на /Servise
    if (!string.IsNullOrEmpty(returnUrl))
        response.Redirect("/" + returnUrl);
    else
        response.Redirect("/Servise");
});

// Пробрасываем контроллеры (если есть API/MVC)
app.MapControllers();

// Blazor-компоненты (серверный рендеринг)
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
