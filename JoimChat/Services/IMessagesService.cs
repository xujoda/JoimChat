using JoimChat.Models;

namespace JoimChat.Services
{
    public interface IMessagesService
    {
        Task CreateMessage(MessageCreateModel message);
        Task DeleteMessageById(int messageId);
        Task<Message> GetMessageByString(string text);
        Task<Message> GetMessageById(int messageId);
        Task<List<Message>> GetSentMessagesByUserId(int senderId);
        Task<List<Message>> GetReceivedMessagesByUserId(int recipientId);
    }
}
