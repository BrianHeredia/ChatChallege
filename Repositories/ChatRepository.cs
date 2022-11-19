using ChatChallenge.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatChallenge.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly DbContextOptions<ApplicationDbContext> options;
        public ChatRepository(DbContextOptions<ApplicationDbContext> options)
        {
            options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public void CreateChat(Chat chat)
        {
            using (var context = new ApplicationDbContext(options))
            {
                context.Chats.Add(chat);

                context.SaveChanges();
            }
        }

        public void CreateMessage(ChatMessage message)
        {
            using (var context = new ApplicationDbContext(options))
            {
                context.ChatMessages.Add(message);

                context.SaveChanges();
            }
        }

        public void CreateUser(ChatUser user)
        {
            using (var context = new ApplicationDbContext(options))
            {
                context.ChatUsers.Add(user);

                context.SaveChanges();
            }
        }

        public List<ChatUser> GetAllUser(Guid id)
        {
            using (var context = new ApplicationDbContext(options))
            {
                return context.ChatUsers.ToList();
            }
        }

        public List<ChatMessage> GetChatMessages(Guid id)
        {
            using (var context = new ApplicationDbContext(options))
            {
                return context.ChatMessages.Where(x => x.ChatId == id).ToList();
            }
        }

        public List<ChatUser> GetChatUsers(Guid id)
        {
            using (var context = new ApplicationDbContext(options))
            {
                var chat = context.Chats.Include(x => x.JoinedChatUsers).Where(x => x.ChatId == id).FirstOrDefault();
                if (chat != null)
                {
                    return chat.JoinedChatUsers.ToList();
                }
                return new List<ChatUser>();
            }
        }

        public List<Chat> GetUserChats(Guid id)
        {
            using(var context = new ApplicationDbContext(options))
            {
                var user = context.ChatUsers.Include(x => x.JoinedChatsRooms).Where(x => x.ChatUserId == id).FirstOrDefault();
                if(user != null)
                {
                    return user.JoinedChatsRooms.ToList();
                }
                return new List<Chat>();
            }
        }
    }
}
