using System;

namespace datamodel.Models
{
    public class UrlHistory
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UrlId { get; set; }
        public Url Url { get; set; }
        public DateTime Redirected { get; set; } = DateTime.Now;
    }
}