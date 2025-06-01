namespace Kopigrad.Components.Classes
{
    // Models/TemplateDefinition.cs
    public class TextSlot
    {
        // Уникальный ключ слота (например, "OrgName", "Phone", "Position" и т.д.)
        public string Key { get; set; }

        // Отображаемая надпись в форме (label), например: "Название организации" или "Телефон"
        public string Label { get; set; }

        // Процент от ширины контейнера (0 – 100): X-координата слота
        public double XPercent { get; set; }

        // Процент от высоты контейнера (0 – 100): Y-координата слота
        public double YPercent { get; set; }

        // Размер шрифта в пикселях (или можно задать относительный rem, но тут для простоты пикселы)
        public int FontSizePx { get; set; }

        // Цвет текста (hex, rgb, либо просто "white", "black" и т. п.)
        public string Color { get; set; }

        // Дополнительные CSS-правила (если нужно, например, тень, font-weight и т.п.)
        public string AdditionalCss { get; set; } = "";

        // Можно указать align: "left", "center", "right" — если текст нужно выравнивать по-разному
        public string TextAlign { get; set; } = "left";
    }

    public class TemplateDefinition
    {
        // URL или путь к файлу-изображению
        public string BackgroundImageUrl { get; set; }

        // Список слотов (TextSlot), каждый из которых: позиция + стиль + ключ + label
        public List<TextSlot> Slots { get; set; } = new List<TextSlot>();
    }

}
