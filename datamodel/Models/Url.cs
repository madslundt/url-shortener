using System;
using System.Collections.Generic;

namespace datamodel.Models
{
    public class Url
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ShortId { get; set; }
        public string RedirectUrl { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        public ICollection<UrlHistory> UrlHistories { get; set; }
    }
}