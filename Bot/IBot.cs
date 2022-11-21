namespace ChatChallenge.Bot
{
    public interface IBot
    {
        (bool isValidCommand,string? command, string? data) AnalizeMessage(string message);
        (bool isSuccesful, string response) ExecuteStockCommand(string data);
    }
}
