using ChatChallenge.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatChallenge.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly DbContextOptions<ApplicationDbContext> options;
        public ChatRepository(DbContextOptions<ApplicationDbContext> _options)
        {
            options = _options ?? throw new ArgumentNullException(nameof(options));
        }

        public void CreateChat(Chat chat)
        {
            using (var context = new ApplicationDbContext(options))
            {
                context.Entry<Chat>(chat).State = EntityState.Added;
                foreach (var user in chat.JoinedChatUsers)
                {
                    context.Entry<ChatUser>(user).State = EntityState.Modified;
                }
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

        public List<ChatUser> GetAllUsers()
        {
            using (var context = new ApplicationDbContext(options))
            {
                return context.Users.ToList();
            }
        }

        public Chat GetChat(Guid id)
        {
            using (var context = new ApplicationDbContext(options))
            {
                return context.Chats.Include(x => x.ChatMessages).Include(x => x.JoinedChatUsers).First(x => x.ChatId == id);
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

        public List<Chat> GetUserChats(string id)
        {
            using (var context = new ApplicationDbContext(options))
            {
                return context.Chats.Include(x => x.JoinedChatUsers).Where(x => x.JoinedChatUsers.Any(y => y.Id == id)).ToList();
            }
        }

        public ChatUser GetUser(string id)
        {
            using (var context = new ApplicationDbContext(options))
            {
                return context.ChatUsers.First(x => x.Id == id);
            }
        }

        public void UpdateChat(Chat chat)
        {
            using (var context = new ApplicationDbContext(options))
            {
                context.Entry<Chat>(chat).State = EntityState.Modified;

                context.SaveChanges();
            }
        }
    }
}
