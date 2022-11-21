using CsvHelper;
using System.Globalization;
using ChatChallenge.Data;

namespace ChatChallenge.Bot
{
    public class Bot : IBot
    {
        private readonly IEnumerable<string> commands = new string[] { "stock" };
        private readonly IHttpClientFactory httpClientFactory;
        public Bot(IHttpClientFactory _httpClientFactory)
        {
            httpClientFactory = _httpClientFactory;
        }

        public (bool isValidCommand, string? command, string? data) AnalizeMessage(string message)
        {
            var isValidCommand = commands.Any(x => message.Contains($"/{x}="));
            var command = commands.FirstOrDefault(x => message.Contains($"/{x}="));
            string? data = null;
            if(command is not null && isValidCommand)
            {
                data = message.Substring(7);
            }
            return (isValidCommand, command, data);
        }

        public (bool isSuccesful, string response) ExecuteStockCommand(string data)
        {
            try
            {
                using var httpClient = httpClientFactory.CreateClient("StockAPI");

                var request = new HttpRequestMessage(HttpMethod.Get, $"?s={data}&f=sd2t2ohlcv&h&e=csv");

                var response = httpClient.SendAsync(request).Result;

                using var s = response.Content.ReadAsStreamAsync().Result;

                using var sr = new StreamReader(s);

                using var csv = new CsvReader(sr, CultureInfo.CurrentCulture);

                var stock = csv.GetRecords<Stock>().FirstOrDefault();
                
                if(stock is not null)
                {
                    return (true, $"{stock.Symbol} quote is ${stock.Close.ToString("0.##")} per share.");
                }
                else
                {
                    throw new Exception("Information not available.");
                }

            }
            catch (Exception ex)
            {
                return (false, "There was an error, please try again later.");
            }


        }
    }
}
