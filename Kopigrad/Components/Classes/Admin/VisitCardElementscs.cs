namespace Kopigrad.Components.Classes.Admin
{
    /// <summary>Базовый элемент: просто X/Y.</summary>
    public abstract class ElementBase
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    /// <summary>Текстовый элемент.</summary>
    public class TextElement : ElementBase
    {
        public string Text { get; set; } = string.Empty;
        public string Color { get; set; } = "#000000";
        public int FontSize { get; set; } = 16;
        public string FontFamily { get; set; } = "Arial";
    }

    /// <summary>Графический элемент.</summary>
    public class ImageElement : ElementBase
    {
        /// <remarks>data-url или обычный URL</remarks>
        public string DataUrl { get; set; } = string.Empty;
        public double Width { get; set; } = 100;
        public double Height { get; set; } = 100;
    }

    /// <summary>Ответ от API генерации.</summary>
    public class ApiResponse
    {
        public string status { get; set; } = "";
        public string image_url { get; set; } = "";
    }
}
