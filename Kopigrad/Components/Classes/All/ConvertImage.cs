namespace Kopigrad.Components.Classes.All
{
    static class ConvertImage
    {
        public static string? GetImageSrc(byte[] Image)
        {
            if (Image == null)
                return null;

            try
            {
                // Преобразование в строку Base64
                var base64 = Convert.ToBase64String(Image);
                return $"data:image/png;base64,{base64}";
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
