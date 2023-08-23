using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JoimChat.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public bool IsOnline { get; set; }

        public DateTime LastOnline { get; set; }

        public List<Message> SentMessages { get; set; }
        public List<Message> ReceivedMessages { get; set; }
    }
}
