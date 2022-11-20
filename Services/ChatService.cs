using ChatChallenge.Data;
using ChatChallenge.Repositories;
using Microsoft.AspNetCore.Components.Authorization;

namespace ChatChallenge.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        public ChatService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public List<ChatUser> GetAllUsers()
        {
            return _chatRepository.GetAllUsers();
        }

        public Chat GetChatWithMessages(Guid id)
        {
            return _chatRepository.GetChat(id);
        }

        public List<Chat> GetUserChats(string userId)
        {
            return _chatRepository.GetUserChats(userId);
        }

        public (Chat?, bool, string?) JoinChat(string userId, Guid chatId)
        {
            try
            {
                var chat = _chatRepository.GetChat(chatId);
                chat.JoinedChatUsers.Add(_chatRepository.GetUser(userId));
                _chatRepository.UpdateChat(chat);

                return (chat, true, null);
            }
            catch (Exception ex)
            {
                return (null, false, ex.Message);
            }

            throw new NotImplementedException();
        }

        public (ChatMessage?, bool, string?) SendMessage(string userId, Guid chatId, string message)
        {
            try
            {
                var Message = new ChatMessage()
                {
                    ChatId = chatId,
                    ChatUserId = userId,
                    Message = message,
                    CreatedOn = DateTime.Now,
                    ChatMessageId = Guid.NewGuid()                    
                };

                _chatRepository.CreateMessage(Message);

                return (Message, true, null);
            }
            catch (Exception ex)
            {
                return (null, false, ex.Message);
            }
        }

        public (Chat?, bool, string?) StartChat(string creatorId, string invitedId)
        {
            try
            {
                var chat = new Chat()
                {
                    ChatId = Guid.NewGuid()
                };
                chat.JoinedChatUsers.Add(_chatRepository.GetUser(creatorId));
                chat.JoinedChatUsers.Add(_chatRepository.GetUser(invitedId));

                _chatRepository.CreateChat(chat);

                return (chat, true, null);
            }
            catch (Exception ex)
            {
                return (null, false, ex.Message);
            }
        }
    }
}
