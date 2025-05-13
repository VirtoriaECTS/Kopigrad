using Kopigrad.Components;
using Kopigrad.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Features;
using Kopigrad.Components.Classes.Admin.Servise;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// ����������� ����� ��� ������ (��������, �� 100 MB)
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = long.MaxValue; // ������������ ������ ���� �������
});
// �������� ��������� ������
builder.Logging.SetMinimumLevel(LogLevel.Debug);
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<KopigradContext>(options =>
{
    options.UseMySql(
        "server=62.76.233.55;port=3306;database=kopigrad;user=blazoruser;password=newpassword123;",
        new MySqlServerVersion(new Version(10, 4, 32)), // ���������, ��� ������ ��������� �������
        mysqlOptions => mysqlOptions.EnableRetryOnFailure()  // �������� ��������� ������� �����������
    );
});



builder.Services.AddHttpClient();


var app = builder.Build();

// �������� ���������� �� ������������� �� ���� ����������� �� ����� 5000


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
