using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoimChat.Models
{
    public class Message
    {
        [Key] // Указываем, что это ключ
        public int MessageId { get; set; }

        [Required]
        public string MessageString { get; set; } = string.Empty;

        [Required]
        public DateTime DispatchTime { get; set; }

        [Required]
        public bool IsRead { get; set; }

        [ForeignKey("SenderId")]
        public int SenderId { get; set; }

        [ForeignKey("RecipientId")]
        public int RecipientId { get; set; }

        public User Sender { get; set; }
        public User Recipient { get; set; }
    }
}
