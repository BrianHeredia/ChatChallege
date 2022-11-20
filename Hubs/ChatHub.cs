using ChatChallenge.Services;
using Microsoft.AspNetCore.SignalR;

namespace ChatChallenge.Servers.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;
        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }
        public async Task SendMessage(Guid chatId, string userId, string message)
        {
            var response = _chatService.SendMessage(userId,chatId,message);
            await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage",response.Item1);
        }
    }
}