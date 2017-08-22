namespace src.Infrastructure.Configuration
{
    public class UrlSettings
    {
        public string Domains { get; set; }
        public string ShortIdCharacters { get; set; }
        public int ShortIdLength { get; set; }
        public string RedirectUrl { get; set; }
    }
}