using OpenAI;
using OpenAI.Chat;

namespace TravelPlanner.API.Services
{
    public class AiService
    {
        private readonly ChatClient _chatClient;

        public AiService(IConfiguration configuration)
        {
            var apiKey = configuration["OpenAI:ApiKey"]!;
            var client = new OpenAIClient(apiKey);
            _chatClient = client.GetChatClient("gpt-3.5-turbo");
        }

        public async Task<string> GenerateItinerary(string destination, string country, int days, string preferences)
        {
            var prompt = $@"Creează un itinerariu detaliat de călătorie pentru {destination}, {country}.
Durata: {days} zile.
Preferințe: {preferences}.

Pentru fiecare zi include:
- Activități de dimineață, prânz și seară
- Recomandări de restaurante
- Estimare costuri
- Tips utile

Răspunde în limba română, formatat frumos.";

            var messages = new List<ChatMessage>
            {
                new SystemChatMessage("Ești un expert în planificarea călătoriilor. Creezi itinerarii detaliate și utile."),
                new UserChatMessage(prompt)
            };

            ChatCompletion completion = await _chatClient.CompleteChatAsync(messages);
            return completion.Content[0].Text;
        }
    }
}