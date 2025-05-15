using Kopigrad.Components.Pages.Admin.Requst;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;

namespace Kopigrad.Components.Classes.Controller
{


[ApiController]

    public class TelegramAuthController : ControllerBase
    {
        private readonly string BotToken = "7806191743:AAHhN4JtNubcpRoPMtZsss-EStNYXpYAAMw"; // вставьте токен вашего бота

        [HttpGet("/auth/telegram")]
        public IActionResult TelegramAuth()
        {
            Console.WriteLine("👉 Телеграм-авторизация сработала");
            var query = Request.Query.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());

            if (!query.ContainsKey("hash"))
                return BadRequest("No hash");

            var hash = query["hash"];
            query.Remove("hash");

            var dataCheckString = string.Join("\n", query.OrderBy(kvp => kvp.Key).Select(kvp => $"{kvp.Key}={kvp.Value}"));

            using var sha256 = SHA256.Create();
            var key = sha256.ComputeHash(Encoding.UTF8.GetBytes(BotToken));

            using var hmac = new HMACSHA256(key);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dataCheckString));
            var computedHashHex = BitConverter.ToString(computedHash).Replace("-", "").ToLower();

            if (computedHashHex != hash)
            {
                return BadRequest("Invalid hash");
            }

            var id = query.GetValueOrDefault("id");

            // сохраняем TelegramId в куки
            Response.Cookies.Append("TelegramId", id, new CookieOptions
            {
                Path = "/",
                HttpOnly = false,
                Expires = DateTimeOffset.UtcNow.AddMinutes(10),
                SameSite = SameSiteMode.Lax
            });

            // сохраняем photo_url в куки (если есть)
            var photoUrl = query.GetValueOrDefault("photo_url");
            if (!string.IsNullOrEmpty(photoUrl))
            {
                Response.Cookies.Append("TelegramPhoto", photoUrl, new CookieOptions
                {
                    Path = "/",
                    HttpOnly = false,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(10),
                    SameSite = SameSiteMode.Lax
                });
            }

            // редирект обратно на Blazor-страницу
            return Redirect("/CreateRequest");
        }



    }

}
