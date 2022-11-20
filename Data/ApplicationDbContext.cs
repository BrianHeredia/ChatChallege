using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatChallenge.Data
{
    public partial class ApplicationDbContext : IdentityDbContext<ChatUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<ChatUser> ChatUsers { get; set; }
        public virtual DbSet<ChatMessage> ChatMessages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Chat>(entity =>
            {
                entity.ToTable("Chat", "dbo");

                entity.Property(e => e.ChatId).ValueGeneratedNever();
            });

            modelBuilder.Entity<ChatMessage>(entity =>
            {
                entity.ToTable("ChatMessage", "dbo");

                entity.Property(e => e.ChatMessageId).ValueGeneratedNever();

                entity.Property(e => e.Message)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.ChatUser)
                    .WithMany(p => p.ChatMessages)
                    .HasForeignKey(d => d.ChatUserId)
                    .HasConstraintName("FK_ChatUser_ChatMessage");

                entity.HasOne(d => d.Chat)
                    .WithMany(p => p.ChatMessages)
                    .HasForeignKey(d => d.ChatId)
                    .HasConstraintName("FK_Chat_ChatMessage");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

    public class Chat
    {
        public Chat()
        {
            ChatMessages = new HashSet<ChatMessage>();
            JoinedChatUsers = new HashSet<ChatUser>();
        }
        public Guid ChatId { get; set; }
        public virtual ICollection<ChatMessage> ChatMessages { get; set; }
        public virtual ICollection<ChatUser> JoinedChatUsers { get; set; }
    }

    public class ChatUser: IdentityUser
    {
        public ChatUser()
        {
            ChatMessages = new HashSet<ChatMessage>();
            JoinedChatsRooms = new HashSet<Chat>();
        }
        public virtual ICollection<ChatMessage> ChatMessages { get; set; }
        public virtual ICollection<Chat> JoinedChatsRooms { get; set; }
    }

    public class ChatMessage
    {
        public Guid ChatMessageId { get; set; }
        public string ChatUserId { get; set; }
        public Guid ChatId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Message { get; set; }
        public ChatUser ChatUser { get; set; }
        public Chat Chat { get; set; }

    }
}