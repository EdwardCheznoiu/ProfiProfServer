using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Common.Clients;

namespace Conversation.Binder.Service.Clients
{
    public class ConversationsClient
    {
        private readonly HttpClient httpClient;

        public ConversationsClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<ConversationDto>> GetAsync()
        {
            var conversations = await httpClient.GetFromJsonAsync<IReadOnlyCollection<ConversationDto>>("/conversations");

            return conversations;
        }
    }
}