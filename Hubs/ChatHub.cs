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
            string _message = string.Empty;
            bool isBotMessage = false;
            if (botResponse.isValidCommand)//Bot Command
            {
                if(botResponse.command == "stock" && botResponse.data is not null)
                {
                    var botCommandExecuted = _bot.ExecuteStockCommand(botResponse.data);
                    _message = botCommandExecuted.response;
                    isBotMessage = true;
                }
            }
            else//Regular Message
            {
                _message = message;
            }
            var response = _chatService.SendMessage(userId, chatId, _message, isBotMessage);
            await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", response._ChatMessage);
        }

        public async Task AddToGroup(string chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);

            //await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
        }
    }
}