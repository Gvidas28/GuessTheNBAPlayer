using GuessTheNBAPlayer.Model.Entities.Exceptions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace GuessTheNBAPlayer.Model.Services
{
    public class ClientService : IClientService
    {
        public Response SendRequest<Response>(string url) where Response : class
        {
            var client = new RestClient(url);
            var request = new RestRequest("", Method.Get);

            var res = client.Execute(request);
            if (res?.StatusCode is not HttpStatusCode.OK)
                throw new CommunicationException($"Failed to communicate with the balldontlie API: Status - {res?.StatusCode}, Content - {res?.Content}");

            return JsonConvert.DeserializeObject<Response>(res.Content);
        }
    }
}