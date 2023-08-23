using JoimChat.Models;

namespace JoimChat.Services
{
    public class ChatRepository
    {
        private readonly ChatDbContext _context;

        public ChatRepository(ChatDbContext context) // perenesti to ChatService || mesagesservice
        {
            _context = context;
        }

        public async Task SaveMessageAsync(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
        }
    }
}
