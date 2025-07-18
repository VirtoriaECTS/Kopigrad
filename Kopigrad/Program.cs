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

// ���������� MudBlazor
builder.Services.AddMudServices();

// ����������� ����� ��� �������� ������
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = long.MaxValue;
});

// ����
builder.Logging.SetMinimumLevel(LogLevel.Debug);
builder.Logging.AddConsole();

// Blazor Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddControllers();

// ����� ����� ������ � HttpContext ������ �����������
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

// HttpClient, ���� �����
builder.Services.AddHttpClient();

// Cookie-��������������
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "KopigradAuth";
        options.LoginPath = "/Admin";
        options.ExpireTimeSpan = TimeSpan.FromHours(2);
        options.SlidingExpiration = true;
    });

// Authorization ��� [Authorize]
builder.Services.AddAuthorization();

// ������������ ����� �����������
builder.Services.AddScoped<AutorisationClasses>();

// ����� ��� �������� ����� (���� ������������)
builder.Services.AddSingleton<MailSender>();

var app = builder.Build();

// � ������ Production ���������� �������� ������ � HSTS
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// �������� �������������
app.UseRouting();

// ��������������/�����������
app.UseAuthentication();
app.UseAuthorization();

// Middleware ��� Anti-Forgery
app.UseAntiforgery();

app.MapGet("/doLogout", async (HttpContext httpContext) =>
{
    await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    httpContext.Response.Redirect("/");
});

// ����������� API-�������� ��� ������
app.MapGet("/doLogin", async (HttpContext httpContext, AutorisationClasses authService) =>
{
    var request = httpContext.Request;
    var response = httpContext.Response;

    // ������ ��������� �� query-string
    var email = request.Query["email"].ToString();
    var password = request.Query["password"].ToString();
    var returnUrl = request.Query["returnUrl"].ToString();

    // ��������� ������
    string checkResult = authService.CheakPassword(email, password);
    if (!string.IsNullOrEmpty(checkResult))
    {
        response.ContentType = "text/html; charset=utf-8";
        await response.WriteAsync($@"
            <h3 style=""color:red;"">������ �����: {checkResult}</h3>
            <p><a href=""/Admin"">��������� � ����� �����</a></p>");
        return;
    }

    // ������ ����-��������������
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

    // ���������� �� returnUrl ��� �� /Servise
    if (!string.IsNullOrEmpty(returnUrl))
        response.Redirect("/" + returnUrl);
    else
        response.Redirect("/Servise");
});

// ������������ ����������� (���� ���� API/MVC)
app.MapControllers();

// Blazor-���������� (��������� ���������)
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
