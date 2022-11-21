using ChatChallenge.Services;
using ChatChallenge.Bot;
using Microsoft.AspNetCore.SignalR;

namespace ChatChallenge.Servers.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;
        private readonly IBot _bot;
        public ChatHub(IChatService chatService, IBot bot)
        {
            _chatService = chatService;
            _bot = bot;
        }
        public async Task SendMessage(Guid chatId, string userId, string message)
        {
            var botResponse = _bot.AnalizeMessage(message);
            if (botResponse.isValidCommand)
            {
                if(botResponse.command == "stock" && botResponse.data is not null)
                {
                    var botCommandExecuted = _bot.ExecuteStockCommand(botResponse.data);
                    await Clients.Group(chatId.ToString()).SendAsync("ReceiveBotMessage", botCommandExecuted.response);
                }
            }
            else
            {
                var response = _chatService.SendMessage(userId, chatId, message);
                await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", response.Item1);
            }
        }

        public async Task AddToGroup(string chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);

            //await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
        }
    }
}