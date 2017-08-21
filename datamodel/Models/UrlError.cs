using System;
using System.Collections.Generic;

namespace datamodel.Models
{
    public class UrlError
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ErrorMessage { get; set; }
        public int StatusCode { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}