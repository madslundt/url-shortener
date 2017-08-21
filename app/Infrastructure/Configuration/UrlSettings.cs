namespace src.Infrastructure.Configuration
{
    public class UrlSettings
    {
        public string Domain { get; set; }
        public string AllowedCharacters { get; set; }
        public int ShortIdLength { get; set; }
        public string RedirectUrl { get; set; }
    }
}