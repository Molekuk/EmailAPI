using System.Collections.Generic;

namespace EmailAPI.Models
{
    public class SendStatus
    {
        public bool Succeed { get; set; } = true;
        public List<string> Errors { get; set; }
    }
}
