using ChatChallenge.Data;

namespace ChatChallenge.Services
{
    public interface IChatService
    {
        (ChatMessage? _ChatMessage, bool isSucceful, string? ErrorMessage) SendMessage(string userId, Guid chatId, string message, bool isBotMessage);
        (Chat? _Chat, bool isSucceful, string? ErrorMessage) StartChat(string creatorId, string invitedId);
        (Chat? _Chat, bool isSucceful, string? ErrorMessage) JoinChat(string userId, Guid chatId);
        List<ChatUser> GetAllUsers();
        List<Chat> GetUserChats(string userId);
        (Chat? _Chat, bool isSucceful, string? ErrorMessage) GetChatWithMessages(Guid id);
    }
}
