using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmailAPI
{
    public class Message
    {
        [Required]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public ICollection<string> Recipients { get; set; } = new List<string>();
    }
}
