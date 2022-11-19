using ChatChallenge.Data;
namespace ChatChallenge.Repositories
{
    public interface IChatRepository
    {
        List<ChatMessage> GetChatMessages(Guid id);
        List<Chat> GetUserChats(Guid id);
        List<ChatUser> GetChatUsers(Guid id);
        List<ChatUser> GetAllUser(Guid id);
        void CreateChat(Chat chat);
        void CreateMessage(ChatMessage message);
        void CreateUser(ChatUser user);
    }
}
