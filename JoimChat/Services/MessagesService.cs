using JoimChat.Models;
using Microsoft.EntityFrameworkCore;

namespace JoimChat.Services
{
    public class MessagesService : IMessagesService
    {
        private readonly ChatDbContext _context;

        public MessagesService (ChatDbContext context)
        {
            _context = context;
        }

        public async Task CreateMessage(MessageCreateModel messageModel)
        {
            var message = new Message()
            {
                DispatchTime = DateTime.UtcNow,
                MessageString = messageModel.MessageString,
                IsRead = false,
                RecipientId = messageModel.RecipientId,
                SenderId = messageModel.SenderId,
                Sender = await _context.Users.FirstOrDefaultAsync(x => x.UserId == messageModel.SenderId),
                Recipient = await _context.Users.FirstOrDefaultAsync(x => x.UserId == messageModel.RecipientId),
            };

            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        public async Task<Message> GetMessageById(int messageId)
        {
            if (messageId < 0)
            {
                throw new ArgumentNullException($"messageId {messageId}");
            }

            var message = await _context.Messages.FirstOrDefaultAsync(x => x.MessageId == messageId);

            return message ?? throw new ArgumentNullException($"messageId {messageId}");
        }

        public async Task DeleteMessageById(int messageId)
        {
            var message = await GetMessageById(messageId) ?? throw new ArgumentNullException($"messageId {messageId}");
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
        }

        public async Task<Message> GetMessageByString(string messageString)
        {
            if (string.IsNullOrEmpty(messageString))
            {
                throw new ArgumentNullException(nameof(messageString));
            }

            var message = await _context.Messages.FirstOrDefaultAsync(x => x.MessageString == messageString);

            return message ?? throw new ArgumentNullException(nameof(messageString));
        }

        public async Task<List<Message>> GetSentMessagesByUserId(int userId)
        {
            if (userId < 0)
            {
                throw new ArgumentNullException($"userID {userId}"); ;
            }

            var messagesList = await _context.Messages.Where(m => m.SenderId == userId).ToListAsync();

            return messagesList ?? throw new ArgumentNullException($"userID {userId}"); ;
        }

        public async Task<List<Message>> GetReceivedMessagesByUserId(int userId)
        {
            if (userId < 0)
            {
                throw new ArgumentNullException($"userID {userId}");
            }

            var messagesList = await _context.Messages.Where(m => m.RecipientId == userId).ToListAsync();

            return messagesList ?? throw new ArgumentNullException($"userID {userId}"); ; ;
        }
    }
}
