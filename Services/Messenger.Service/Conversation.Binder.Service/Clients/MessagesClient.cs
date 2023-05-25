using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Common.Clients;

namespace Conversation.Binder.Service.Clients
{
    public class MessagesClient
    {
        private readonly HttpClient httpClient;

        public MessagesClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<MessageDto>> GetAsync()
        {
            var messages = await httpClient.GetFromJsonAsync<IReadOnlyCollection<MessageDto>>("/messages");

            return messages;
        }
    }
}