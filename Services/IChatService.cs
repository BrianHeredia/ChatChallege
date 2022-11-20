using ChatChallenge.Data;

namespace ChatChallenge.Services
{
    public interface IChatService
    {
        (ChatMessage?, bool, string?) SendMessage(string userId, Guid chatId, string message);
        (Chat?, bool, string?) StartChat(string creatorId, string invitedId);
        (Chat?, bool, string?) JoinChat(string userId, Guid chatId);
        List<ChatUser> GetAllUsers();
        List<Chat> GetUserChats(string userId);
        Chat GetChatWithMessages(Guid id);
    }
}
