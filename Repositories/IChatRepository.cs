using ChatChallenge.Data;

namespace ChatChallenge.Repositories
{
    public interface IChatRepository
    {
        List<ChatMessage> GetChatMessages(Guid id);
        List<Chat> GetUserChats(string id);
        Chat GetChat(Guid id);
        ChatUser GetUser(string id);
        List<ChatUser> GetChatUsers(Guid id);
        List<ChatUser> GetAllUsers();
        void CreateChat(Chat chat);
        void UpdateChat(Chat chat);
        void CreateMessage(ChatMessage message);
    }
}
