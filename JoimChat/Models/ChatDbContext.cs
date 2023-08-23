using Microsoft.EntityFrameworkCore;

namespace JoimChat.Models
{
    public class ChatDbContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }

        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Recipient)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
